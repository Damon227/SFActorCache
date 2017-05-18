// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : CacheActor.Interfaces
// File             : CacheService.cs
// Created          : 2017-05-18  15:47
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Credit.Kolibre.Foundation.Cache;
using Credit.Kolibre.Foundation.Sys;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace CacheActor.Interfaces
{
    public class CacheService : ICacheService
    {
        private static readonly Uri s_serviceUri = new Uri("fabric:/Credit.Kolibre.Cache/CacheActorService");
        private static readonly ActorId s_actorId = new ActorId("FD9101FB3503401E9AA3015B47D0E9D6");

        #region ICacheService Members

        /// <summary>
        ///     获取缓存。
        /// </summary>
        /// <param name="key">缓存的主键。</param>
        public async Task<T> GetCacheAsync<T>(string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            string response = await cacheActor.GetCacheAsync(key);
            return response.FromJson<T>();
        }

        /// <summary>
        ///     获取 Hash 主键+次键对应的缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="secondKey">次键，为空则返回主键对应的缓存。</param>
        public async Task<T> GetHashCacheAsync<T>(string firstKey, string secondKey)
        {
            if (firstKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(firstKey));
            }

            if (secondKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(secondKey));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            string response = await cacheActor.GetHashCacheAsync(firstKey, secondKey);
            return response.FromJson<T>();
        }

        /// <summary>
        ///     移除并返回列表中第一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        public async Task<T> LeftPopListCacheAsync<T>(string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            string response = await cacheActor.LeftPopListCacheAsync(key);
            return response.FromJson<T>();
        }

        /// <summary>
        ///     移除并返回列表中最后一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        public async Task<T> RightPopListCacheAsync<T>(string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            string response = await cacheActor.RightPopListCacheAsync(key);
            return response.FromJson<T>();
        }

        #endregion

        /// <summary>
        ///     设置缓存。
        /// </summary>
        /// <param name="key">缓存的主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task SetCacheAsync<T>(string key, T value, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.SetCacheAsync(key, value.ToJson(), options);
        }

        /// <summary>
        ///     刷新缓存。
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        public async Task RefreshAsync(string key, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.RefreshAsync(key, options);
        }

        /// <summary>
        ///     设置 Hash 缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="secondKey">次键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task SetHashCacheAsync<T>(string firstKey, string secondKey, T value, KolibreCacheOptions options)
        {
            if (firstKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(firstKey));
            }

            if (secondKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(secondKey));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.SetHashCacheAsync(firstKey, secondKey, value.ToJson(), options);
        }

        /// <summary>
        ///     刷新 Hash 缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="options">The options.</param>
        public async Task RefreshHashCacheAsync(string firstKey, KolibreCacheOptions options)
        {
            if (firstKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(firstKey));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.RefreshHashCacheAsync(firstKey, options);
        }

        /// <summary>
        ///     向列表首部添加元素，若 key 对应无列表缓存，则自动创建并添加一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task LeftPushListCacheAsync<T>(string key, T value, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.LeftPushListCacheAsync(key, value.ToJson(), options);
        }

        /// <summary>
        ///     向列表尾添加元素，若 key 对应无列表缓存，则自动创建并添加一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        public async Task RightPushListCacheAsync<T>(string key, T value, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.RightPushListCacheAsync(key, value.ToJson(), options);
        }

        /// <summary>
        ///     刷新列表缓存。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="options">The options.</param>
        public async Task RefreshListCacheAsync(string key, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(s_actorId, s_serviceUri);
            await cacheActor.RefreshListCacheAsync(key, options);
        }
    }
}