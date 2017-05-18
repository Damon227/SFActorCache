// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : CacheActor
// File             : CacheActor.cs
// Created          : 2017-05-09  20:20
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheActor.Interfaces;
using Credit.Kolibre.Foundation.Cache;
using Credit.Kolibre.Foundation.Sys;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;

namespace CacheActor
{
    /// <remarks>
    ///     This class represents an actor.
    ///     Every ActorID maps to an instance of this class.
    ///     The StatePersistence attribute determines persistence and replication of actor state:
    ///     - Persisted: State is written to disk and replicated.
    ///     - Volatile: State is kept in memory only and replicated.
    ///     - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class CacheActor : Actor, ICacheActor
    {
        /// <summary>
        ///     清理过期 Cache 的定时器。
        /// </summary>
        private IActorTimer _clearExpiredCacheTimer;

        /// <summary>
        ///     Initializes a new instance of CacheActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public CacheActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        #region ICacheActor Members

        /// <summary>
        ///     获取缓存。
        /// </summary>
        /// <param name="key">缓存的主键。</param>
        public async Task<string> GetCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper> result = await StateManager.TryGetStateAsync<CacheWrapper>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper cacheWrapper = result.Value;

            // 如果获取缓存时，给定的 key 对应的值类型跟当前类型不一致。
            if (cacheWrapper.CacheType != CacheType.Normal)
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Cache;
        }

        /// <summary>
        ///     设置缓存。
        /// </summary>
        /// <param name="key">缓存的主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task SetCacheAsync(string key, string value, KolibreCacheOptions options)
        {
            CacheWrapper cacheWrapper = CacheWrapper.BuildCacheWrapper(value, CacheType.Normal, options);
            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     刷新缓存。
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        public async Task RefreshAsync(string key, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper> result = await StateManager.TryGetStateAsync<CacheWrapper>(key);
            if (!result.HasValue)
            {
                return;
            }

            CacheWrapper cacheWrapper = result.Value;
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            await StateManager.SetStateAsync(key, cacheWrapper.Refresh(options));
        }

        /// <summary>
        ///     获取 Hash 主键+次键对应的缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="secondKey">次键，为空则返回主键对应的缓存。</param>
        public async Task<string> GetHashCacheAsync(string firstKey, string secondKey)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(firstKey);
            if (!state.HasValue)
            {
                return null;
            }

            CacheWrapper cacheWrapper = state.Value;

            // 如果获取缓存时，给定的 key 对应的值类型跟当前类型不一致。
            if (cacheWrapper.CacheType != CacheType.Hash)
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }
            // 若 first key 过期
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(firstKey);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(firstKey, refreshResult.cacheWrapper);
            }

            Dictionary<string, string> hashCacheWrappers;
            try
            {
                hashCacheWrappers = cacheWrapper.Cache.FromJson<Dictionary<string, string>>();
            }
            catch
            {
                throw new ArgumentException($"The cache value which cache key '{firstKey}' is not type of Dictionary<string,string>.");
            }

            if (!hashCacheWrappers.TryGetValue(secondKey, out string hashCache))
            {
                return null;
            }

            return hashCache;
        }


        /// <summary>
        ///     设置 Hash 缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="secondKey">次键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task SetHashCacheAsync(string firstKey, string secondKey, string value, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(firstKey);
            CacheWrapper cacheWrapper = state.HasValue ? state.Value : new CacheWrapper(CacheType.Hash);

            if (cacheWrapper.CacheType != CacheType.Hash)
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }

            // 若设置 Hash Cache 时，值不是 Dictionary<string,string> 类型，抛出异常
            Dictionary<string, string> hashCacheWrappers;
            try
            {
                hashCacheWrappers = cacheWrapper.Cache.FromJson<Dictionary<string, string>>() ?? new Dictionary<string, string>();
            }
            catch
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }

            hashCacheWrappers[secondKey] = value;
            CacheWrapper result = cacheWrapper.SetCache(hashCacheWrappers.ToJson(), CacheType.Hash, options);

            await StateManager.SetStateAsync(firstKey, result);
        }

        /// <summary>
        ///     刷新 Hash 缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="options">The options.</param>
        public async Task RefreshHashCacheAsync(string firstKey, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(firstKey);

            if (!state.HasValue)
            {
                return;
            }

            CacheWrapper cacheWrapper = state.Value;
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(firstKey);
                return;
            }

            await StateManager.SetStateAsync(firstKey, cacheWrapper.Refresh(options));
        }

        /// <summary>
        ///     移除并返回列表中第一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        public async Task<string> LeftPopListCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(key);

            if (!state.HasValue)
            {
                return null;
            }

            CacheWrapper cacheWrapper = state.Value;

            // 如果获取缓存时，给定的 key 对应的值类型跟当前类型不一致。
            if (cacheWrapper.CacheType != CacheType.List)
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            List<string> caches;
            try
            {
                caches = cacheWrapper.Cache.FromJson<List<string>>();
            }
            catch
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }

            if (caches.Count == 0)
            {
                return null;
            }

            string result = caches.First();
            caches.RemoveAt(0);
            cacheWrapper.Cache = caches.ToJson();

            await StateManager.SetStateAsync(key, cacheWrapper);

            return result;
        }

        /// <summary>
        ///     移除并返回列表中最后一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        public async Task<string> RightPopListCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(key);

            if (!state.HasValue)
            {
                return null;
            }

            CacheWrapper cacheWrapper = state.Value;

            // 如果获取缓存时，给定的 key 对应的值类型跟当前类型不一致。
            if (cacheWrapper.CacheType != CacheType.List)
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            List<string> caches;
            try
            {
                caches = cacheWrapper.Cache.FromJson<List<string>>();
            }
            catch
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }

            if (caches == null || caches.Count == 0)
            {
                return null;
            }

            string result = caches.FindLast(t => true);
            caches.RemoveAt(caches.Count - 1);
            cacheWrapper.Cache = caches.ToJson();

            await StateManager.SetStateAsync(key, cacheWrapper);

            return result;
        }

        /// <summary>
        ///     向列表首部添加元素，若 key 对应无列表缓存，则自动创建并添加一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">The options.</param>
        public async Task LeftPushListCacheAsync(string key, string value, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(key);

            CacheWrapper cacheWrapper = new CacheWrapper(CacheType.List);
            if (state.HasValue)
            {
                if (cacheWrapper.CacheType != CacheType.List)
                {
                    throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
                }

                if (!state.Value.IsExpired())
                {
                    cacheWrapper = state.Value;
                }
            }

            List<string> caches;
            try
            {
                caches = cacheWrapper.Cache.FromJson<List<string>>() ?? new List<string>();
            }
            catch
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }

            caches.Insert(0, value);
            CacheWrapper result = cacheWrapper.SetCache(caches.ToJson(), CacheType.List, options);

            await StateManager.SetStateAsync(key, result);
        }

        /// <summary>
        ///     向列表尾添加元素，若 key 对应无列表缓存，则自动创建并添加一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task RightPushListCacheAsync(string key, string value, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(key);

            CacheWrapper cacheWrapper = new CacheWrapper(CacheType.List);
            if (state.HasValue)
            {
                if (cacheWrapper.CacheType != CacheType.List)
                {
                    throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
                }

                if (!state.Value.IsExpired())
                {
                    cacheWrapper = state.Value;
                }
            }

            List<string> caches;
            try
            {
                caches = cacheWrapper.Cache.FromJson<List<string>>() ?? new List<string>();
            }
            catch
            {
                throw new InvalidOperationException($"Operation against a key holding the wrong type '{cacheWrapper.CacheType}'.");
            }

            caches.Insert(caches.Count, value);
            CacheWrapper result = cacheWrapper.SetCache(caches.ToJson(), CacheType.List, options);

            await StateManager.SetStateAsync(key, result);
        }

        /// <summary>
        ///     刷新列表缓存。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="options">The options.</param>
        public async Task RefreshListCacheAsync(string key, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper> state = await StateManager.TryGetStateAsync<CacheWrapper>(key);
            if (!state.HasValue)
            {
                return;
            }

            CacheWrapper cacheWrapper = state.Value;
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            await StateManager.SetStateAsync(key, cacheWrapper.Refresh(options));
        }

        #endregion


        /// <summary>
        ///     This method is called whenever an actor is activated.
        ///     An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            _clearExpiredCacheTimer = RegisterTimer(
                ClearExpiredCacheAsync,
                null,
                TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(5)
            );

            return base.OnActivateAsync();
        }

        /// <summary>
        ///     Override this method to release any resources. This method is called when actor is deactivated (garbage collected by Actor Runtime).
        ///     Actor operations like state changes should not be called from this method.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task">Task</see> that represents outstanding OnDeactivateAsync operation.</returns>
        protected override Task OnDeactivateAsync()
        {
            UnregisterTimer(_clearExpiredCacheTimer);
            return base.OnDeactivateAsync();
        }

        /// <summary>
        ///     清理失效 Cache。
        /// </summary>
        private async Task ClearExpiredCacheAsync(object obj)
        {
            IEnumerable<string> stateNames = await StateManager.GetStateNamesAsync();

            foreach (string stateName in stateNames)
            {
                CacheWrapper state = await StateManager.GetStateAsync<CacheWrapper>(stateName);
                if (state.IsExpired())
                {
                    await StateManager.RemoveStateAsync(stateName);
                }
            }
        }
    }
}