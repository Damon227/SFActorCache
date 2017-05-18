// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : SmartCacheActor.Interfaces
// File             : IKolibreSmartCacheService.cs
// Created          : 2017-05-18  16:22
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

namespace SmartCacheActor.Interfaces
{
    /// <summary>
    ///     Service Fabric Actor Caches 基础接口。
    /// </summary>
    public interface IKolibreSmartCacheService<TSource> where TSource : class
    {
        /// <summary>
        ///     设置单个实例缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        /// <returns></returns>
        Task SetSmartCacheAsync(string partitionId, string key, TSource value, KolibreCacheOptions options);

        /// <summary>
        ///     设置列表实例缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="values">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        Task SetSmartCacheAsync(string partitionId, string key, List<TSource> values, KolibreCacheOptions options);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        Task<TSource> GetSmartCacheAsync(string partitionId, string key);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        Task<List<TSource>> GetSmartCachesAsync(string partitionId, string key);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="orderBy">排序。</param>
        Task<TSource> GetSmartCachesAsync(string partitionId, string key, Expression<Func<TSource, bool>> orderBy);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="top">返回结果数量。</param>
        Task<List<TSource>> GetTopSmartCachesAsync(string partitionId, string key, int top);

        /// <summary>
        ///     查询缓存键对应的缓存值的数量。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        Task<int> GetCountsAsync(string partitionId, string key);

        /// <summary>
        ///     移除缓存键。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        Task RemoveSmartCacheAsync(string partitionId, string key);

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="value">被移除的缓存值。</param>
        Task RemoveSmartCacheAsync(string partitionId, string key, TSource value);

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="values">被移除的缓存值。</param>
        Task RemoveSmartCachesAsync(string partitionId, string key, List<TSource> values);

        /// <summary>
        ///     刷新缓存有效期。
        /// </summary>
        /// <param name="partitionId">缓存分区Id，不同Id对应不同区。</param>
        /// <param name="key">缓存键。</param>
        /// <param name="options">缓存有效期配置</param>
        Task RefreshAsync(string partitionId, string key, KolibreCacheOptions options);
    }
}