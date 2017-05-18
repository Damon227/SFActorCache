// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : SmartCacheActor
// File             : SmartCacheActor.cs
// Created          : 2017-05-18  16:19
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Credit.Kolibre.Foundation.Cache;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;
using SmartCacheActor.Interfaces;

// ***********************************************************************
// Solution         : Credit.Kolibre.Caches
// Project          : SmartCacheActor
// File             : SmartCacheActor.cs
// Created          : 2017-05-18  16:19
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

namespace SmartCacheActor
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
    internal class SmartCacheActor : Actor, ISmartCacheActor
    {
        /// <summary>
        ///     Initializes a new instance of SmartCacheActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public SmartCacheActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        #region ISmartCacheActor Members

        /// <summary>
        ///     刷新缓存有效期。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task RefreshAsync(string key, KolibreCacheOptions options)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;
            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            await StateManager.SetStateAsync(key, cacheWrapper.Refresh(options));
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task<Student> GetSmartCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Student> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches.FirstOrDefault();
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task<List<Student>> GetSmartCachesAsync(string key)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Student> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches;
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="orderBy">排序。</param>
        public async Task<Student> GetOrderlySmartCacheAsync(string key, Expression<Func<Student, bool>> orderBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="top">返回结果数量。</param>
        public async Task<List<Student>> GetTopSmartCachesAsync(string key, int top)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Student> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches.GetRange(0, top);
        }

        /// <summary>
        ///     查询缓存键对应的缓存值的数量。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task<int> GetCountsAsync(string key)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return 0;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return 0;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Student> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches.Count;
        }

        /// <summary>
        ///     移除缓存键。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task RemoveSmartCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return;
            }

            await StateManager.RemoveStateAsync(key);
        }

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">被移除的缓存值。</param>
        public async Task RemoveSmartCacheValueAsync(string key, Student value)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            cacheWrapper.Caches.Remove(value);
            if (cacheWrapper.Caches.Count == 0)
            {
                await StateManager.RemoveStateAsync(key);
            }

            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">被移除的缓存值。</param>
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public async Task RemoveSmartCacheValuesAsync(string key, List<Student> values)
        {
            ConditionalValue<CacheWrapper<Student>> result = await StateManager.TryGetStateAsync<CacheWrapper<Student>>(key);
            if (!result.HasValue)
            {
                return;
            }

            CacheWrapper<Student> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            values.Select(t => cacheWrapper.Caches.Remove(t));
            if (cacheWrapper.Caches.Count == 0)
            {
                await StateManager.RemoveStateAsync(key);
            }

            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     设置单个实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task SetSmartCacheAsync(string key, Student value, KolibreCacheOptions options)
        {
            CacheWrapper<Student> cacheWrapper = CacheWrapper<Student>.BuildCacheWrapper(new List<Student> { value }, CacheType.Object, options);
            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     设置列表实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task SetSmartCachesAsync(string key, List<Student> values, KolibreCacheOptions options)
        {
            CacheWrapper<Student> cacheWrapper = CacheWrapper<Student>.BuildCacheWrapper(values, CacheType.Object, options);
            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     设置单个实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task SetTeacherSmartCacheAsync(string key, Teacher value, KolibreCacheOptions options)
        {
            CacheWrapper<Teacher> cacheWrapper = CacheWrapper<Teacher>.BuildCacheWrapper(new List<Teacher> { value }, CacheType.Object, options);
            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     设置列表实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task SetTeacherSmartCachesAsync(string key, List<Teacher> values, KolibreCacheOptions options)
        {
            CacheWrapper<Teacher> cacheWrapper = CacheWrapper<Teacher>.BuildCacheWrapper(values, CacheType.Object, options);
            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task<Teacher> GetTeacherSmartCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper<Teacher> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Teacher> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches.FirstOrDefault();
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task<List<Teacher>> GetTeacherSmartCachesAsync(string key)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper<Teacher> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Teacher> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches;
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="orderBy">排序。</param>
        public async Task<Teacher> GetTeacherOrderlySmartCacheAsync(string key, Expression<Func<Teacher, bool>> orderBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="top">返回结果数量。</param>
        public async Task<List<Teacher>> GetTeacherTopSmartCachesAsync(string key, int top)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return null;
            }

            CacheWrapper<Teacher> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return null;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Teacher> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches.GetRange(0, top);
        }

        /// <summary>
        ///     查询缓存键对应的缓存值的数量。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task<int> GetTeacherCountsAsync(string key)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return 0;
            }

            CacheWrapper<Teacher> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return 0;
            }

            // 刷新滑动过期时间。
            (bool needRestore, CacheWrapper<Teacher> cacheWrapper) refreshResult = cacheWrapper.Refresh();
            if (refreshResult.needRestore)
            {
                await StateManager.SetStateAsync(key, refreshResult.cacheWrapper);
            }

            return refreshResult.cacheWrapper.Caches.Count;
        }

        /// <summary>
        ///     移除缓存键。
        /// </summary>
        /// <param name="key">缓存键。</param>
        public async Task RemoveTeacherSmartCacheAsync(string key)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return;
            }

            await StateManager.RemoveStateAsync(key);
        }

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">被移除的缓存值。</param>
        public async Task RemoveTeacherSmartCacheValueAsync(string key, Teacher value)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return;
            }

            CacheWrapper<Teacher> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            cacheWrapper.Caches.Remove(value);
            if (cacheWrapper.Caches.Count == 0)
            {
                await StateManager.RemoveStateAsync(key);
            }

            await StateManager.SetStateAsync(key, cacheWrapper);
        }

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">被移除的缓存值。</param>
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public async Task RemoveTeacherSmartCacheValuesAsync(string key, List<Teacher> values)
        {
            ConditionalValue<CacheWrapper<Teacher>> result = await StateManager.TryGetStateAsync<CacheWrapper<Teacher>>(key);
            if (!result.HasValue)
            {
                return;
            }

            CacheWrapper<Teacher> cacheWrapper = result.Value;

            if (cacheWrapper.IsExpired())
            {
                await StateManager.RemoveStateAsync(key);
                return;
            }

            values.Select(t => cacheWrapper.Caches.Remove(t));
            if (cacheWrapper.Caches.Count == 0)
            {
                await StateManager.RemoveStateAsync(key);
            }

            await StateManager.SetStateAsync(key, cacheWrapper);
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

            return StateManager.TryAddStateAsync("count", 0);
        }
    }
}