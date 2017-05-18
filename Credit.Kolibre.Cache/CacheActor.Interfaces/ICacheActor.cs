// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : CacheActor.Interfaces
// File             : ICacheActor.cs
// Created          : 2017-05-09  20:20
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Threading.Tasks;
using Credit.Kolibre.Foundation.Cache;
using Microsoft.ServiceFabric.Actors;

namespace CacheActor.Interfaces
{
    /// <summary>
    ///     This interface defines the methods exposed by an actor.
    ///     Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface ICacheActor : IActor
    {
        /// <summary>
        ///     获取缓存。
        /// </summary>
        /// <param name="key">缓存的主键。</param>
        Task<string> GetCacheAsync(string key);

        /// <summary>
        ///     设置缓存。
        /// </summary>
        /// <param name="key">缓存的主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        Task SetCacheAsync(string key, string value, KolibreCacheOptions options);

        /// <summary>
        ///     刷新缓存。
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        Task RefreshAsync(string key, KolibreCacheOptions options);

        /// <summary>
        ///     获取 Hash 主键+次键对应的缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="secondKey">次键，为空则返回主键对应的缓存。</param>
        Task<string> GetHashCacheAsync(string firstKey, string secondKey);

        /// <summary>
        ///     设置 Hash 缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="secondKey">次键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        Task SetHashCacheAsync(string firstKey, string secondKey, string value, KolibreCacheOptions options);

        /// <summary>
        ///     刷新 Hash 缓存。
        /// </summary>
        /// <param name="firstKey">主键。</param>
        /// <param name="options">The options.</param>
        Task RefreshHashCacheAsync(string firstKey, KolibreCacheOptions options);

        /// <summary>
        ///     移除并返回列表中第一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        Task<string> LeftPopListCacheAsync(string key);

        /// <summary>
        ///     移除并返回列表中最后一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        Task<string> RightPopListCacheAsync(string key);

        /// <summary>
        ///     向列表首部添加元素，若 key 对应无列表缓存，则自动创建并添加一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        Task LeftPushListCacheAsync(string key, string value, KolibreCacheOptions options);

        /// <summary>
        ///     向列表尾添加元素，若 key 对应无列表缓存，则自动创建并添加一个元素。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="value">要缓存的值。</param>
        /// <param name="options">缓存时间设置，为 NULL 表示永久。</param>
        Task RightPushListCacheAsync(string key, string value, KolibreCacheOptions options);

        /// <summary>
        ///     刷新列表缓存。
        /// </summary>
        /// <param name="key">主键。</param>
        /// <param name="options">The options.</param>
        Task RefreshListCacheAsync(string key, KolibreCacheOptions options);
    }
}