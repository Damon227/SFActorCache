// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : SmartCacheActor.Interfaces
// File             : ISmartCacheActor.cs
// Created          : 2017-05-18  16:19
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
using Microsoft.ServiceFabric.Actors;

namespace SmartCacheActor.Interfaces
{
    /// <summary>
    ///     This interface defines the methods exposed by an actor.
    ///     Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface ISmartCacheActor : IActor
    {
        /// <summary>
        ///     刷新缓存有效期。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="options">缓存有效期配置</param>
        Task RefreshAsync(string key, KolibreCacheOptions options);

        /// <summary>
        ///     设置单个实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        Task SetSmartCacheAsync(string key, Student value, KolibreCacheOptions options);

        /// <summary>
        ///     设置列表实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        Task SetSmartCachesAsync(string key, List<Student> values, KolibreCacheOptions options);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task<Student> GetSmartCacheAsync(string key);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task<List<Student>> GetSmartCachesAsync(string key);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="orderBy">排序。</param>
        Task<Student> GetOrderlySmartCacheAsync(string key, Expression<Func<Student, bool>> orderBy);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="top">返回结果数量。</param>
        Task<List<Student>> GetTopSmartCachesAsync(string key, int top);

        /// <summary>
        ///     查询缓存键对应的缓存值的数量。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task<int> GetCountsAsync(string key);

        /// <summary>
        ///     移除缓存键。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task RemoveSmartCacheAsync(string key);

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">被移除的缓存值。</param>
        Task RemoveSmartCacheValueAsync(string key, Student value);

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">被移除的缓存值。</param>
        Task RemoveSmartCacheValuesAsync(string key, List<Student> values);

        /// <summary>
        ///     设置单个实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        Task SetTeacherSmartCacheAsync(string key, Teacher value, KolibreCacheOptions options);

        /// <summary>
        ///     设置列表实例缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">缓存值。</param>
        /// <param name="options">缓存有效期配置</param>
        Task SetTeacherSmartCachesAsync(string key, List<Teacher> values, KolibreCacheOptions options);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task<Teacher> GetTeacherSmartCacheAsync(string key);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task<List<Teacher>> GetTeacherSmartCachesAsync(string key);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="orderBy">排序。</param>
        Task<Teacher> GetTeacherOrderlySmartCacheAsync(string key, Expression<Func<Teacher, bool>> orderBy);

        /// <summary>
        ///     查询缓存。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="top">返回结果数量。</param>
        Task<List<Teacher>> GetTeacherTopSmartCachesAsync(string key, int top);

        /// <summary>
        ///     查询缓存键对应的缓存值的数量。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task<int> GetTeacherCountsAsync(string key);

        /// <summary>
        ///     移除缓存键。
        /// </summary>
        /// <param name="key">缓存键。</param>
        Task RemoveTeacherSmartCacheAsync(string key);

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="value">被移除的缓存值。</param>
        Task RemoveTeacherSmartCacheValueAsync(string key, Teacher value);

        /// <summary>
        ///     移除缓存键中指定的缓存值。
        /// </summary>
        /// <param name="key">缓存键。</param>
        /// <param name="values">被移除的缓存值。</param>
        Task RemoveTeacherSmartCacheValuesAsync(string key, List<Teacher> values);
    }
}