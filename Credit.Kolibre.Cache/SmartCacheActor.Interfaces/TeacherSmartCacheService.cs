// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : SmartCacheActor.Interfaces
// File             : TeacherSmartCacheService.cs
// Created          : 2017-05-18  20:48
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Credit.Kolibre.Foundation.Cache;
using Credit.Kolibre.Foundation.Sys;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace SmartCacheActor.Interfaces
{
    public class TeacherSmartCacheService : ITeacherSmartCacheService
    {
        private static readonly Uri s_serviceUri = new Uri("fabric:/Credit.Kolibre.Cache/SmartCacheActorService");
        private static readonly string s_defaultPartitionId = "6587B0F6D4294AB3A54C015C1AFF2D26";

        #region IKolibreSmartCacheService<Teacher> Members

        /// <summary>
        ///     设置单个实例缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        /// <returns></returns>
        public async Task SetSmartCacheAsync(string partitionId, string key, Teacher value, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            await actor.SetTeacherSmartCacheAsync(key, value, options);
        }

        /// <summary>
        ///     设置列表实例缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="values">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task SetSmartCacheAsync(string partitionId, string key, List<Teacher> values, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (values == null || values.Count == 0)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            await actor.SetTeacherSmartCachesAsync(key, values, options);
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        public async Task<Teacher> GetSmartCacheAsync(string partitionId, string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            return await actor.GetTeacherSmartCacheAsync(key);
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        public async Task<List<Teacher>> GetSmartCachesAsync(string partitionId, string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            return await actor.GetTeacherSmartCachesAsync(key);
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="orderBy">排序。</param>
        public async Task<Teacher> GetSmartCachesAsync(string partitionId, string key, Expression<Func<Teacher, bool>> orderBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="top">返回结果数量。</param>
        public async Task<List<Teacher>> GetTopSmartCachesAsync(string partitionId, string key, int top)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            return await actor.GetTeacherTopSmartCachesAsync(key, top);
        }

        /// <summary>
        ///     查询缓存键对应的缓存值的数量。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        public async Task<int> GetCountsAsync(string partitionId, string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            return await actor.GetCountsAsync(key);
        }

        /// <summary>
        ///     移除缓存键。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        public async Task RemoveSmartCacheAsync(string partitionId, string key)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            await actor.RemoveSmartCacheAsync(key);
        }

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="value">被移除的缓存值。</param>
        public async Task RemoveSmartCacheAsync(string partitionId, string key, Teacher value)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            await actor.RemoveTeacherSmartCacheValueAsync(key, value);
        }

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="values">被移除的缓存值。</param>
        public async Task RemoveSmartCachesAsync(string partitionId, string key, List<Teacher> values)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (values == null || values.Count == 0)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            await actor.RemoveTeacherSmartCacheValuesAsync(key, values);
        }

        /// <summary>
        ///     刷新缓存有效期。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="options">缓存有效期配置</param>
        public async Task RefreshAsync(string partitionId, string key, KolibreCacheOptions options)
        {
            if (key.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (partitionId.IsNullOrEmpty())
            {
                partitionId = s_defaultPartitionId;
            }

            ISmartCacheActor actor = ActorProxy.Create<ISmartCacheActor>(new ActorId(partitionId), s_serviceUri);
            await actor.RefreshAsync(key, options);
        }

        #endregion
    }
}