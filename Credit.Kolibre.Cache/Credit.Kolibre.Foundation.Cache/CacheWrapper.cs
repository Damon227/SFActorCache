﻿// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : Credit.Kolibre.Foundation.Cache
// File             : CacheWrapper.cs
// Created          : 2017-05-18  17:02
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Credit.Kolibre.Foundation.Sys;

namespace Credit.Kolibre.Foundation.Cache
{
    /// <summary>
    ///     用来封装 Cache ，设置有效期。
    /// </summary>
    public class CacheWrapper
    {
        public CacheWrapper(CacheType cacheType)
        {
            CacheType = cacheType;
            InitializationTime = DateTimeOffset.UtcNow.ToChinaStandardTime();
        }

        private CacheWrapper()
        {
        }

        /// <summary>
        ///     缓存的值。
        /// </summary>
        public string Cache { get; set; }

        /// <summary>
        ///     缓存值的类型。"Normal", "Hash", "List".
        /// </summary>
        public CacheType CacheType { get; set; }

        /// <summary>
        ///     初始化时间。
        /// </summary>
        public DateTimeOffset InitializationTime { get; set; }

        /// <summary>
        ///     绝对过期时间。
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <summary>
        ///     滑动过期时长，如果设置了绝对过期时间，滑动过期时长不会延伸超出绝对过期时间。
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }

        /// <summary>
        ///     滑动过期时间点。
        /// </summary>
        public DateTimeOffset SlidingExpirationTime { get; set; }

        public static CacheWrapper BuildCacheWrapper(string cache, CacheType cacheType, KolibreCacheOptions options)
        {
            if (options == null)
            {
                return new CacheWrapper
                {
                    Cache = cache,
                    CacheType = cacheType,
                    InitializationTime = DateTimeOffset.UtcNow.ToChinaStandardTime(),
                    AbsoluteExpiration = DateTimeOffset.MaxValue
                };
            }

            return new CacheWrapper
            {
                Cache = cache,
                CacheType = cacheType,
                InitializationTime = DateTimeOffset.UtcNow.ToChinaStandardTime(),
                AbsoluteExpiration = options.AbsoluteExpiration,
                SlidingExpiration = options.SlidingExpiration,
                SlidingExpirationTime = options.SlidingExpiration.HasValue ? DateTimeOffset.UtcNow.ToChinaStandardTime().Add(options.SlidingExpiration.Value) : DateTimeOffset.UtcNow.ToChinaStandardTime()
            };
        }


        /// <summary>
        ///     刷新缓存
        /// </summary>
        public (bool needRestore, CacheWrapper cacheWrapper) Refresh()
        {
            if (SlidingExpiration.HasValue && SlidingExpirationTime > DateTimeOffset.UtcNow.ToChinaStandardTime())
            {
                SlidingExpirationTime = DateTimeOffset.UtcNow.ToChinaStandardTime() + SlidingExpiration.Value;

                if (AbsoluteExpiration.HasValue && SlidingExpirationTime >= AbsoluteExpiration.Value)
                {
                    SlidingExpirationTime = AbsoluteExpiration.Value;
                }

                return (true, this);
            }

            return (false, this);
        }

        public CacheWrapper Refresh(KolibreCacheOptions options)
        {
            AbsoluteExpiration = options.AbsoluteExpiration;
            SlidingExpiration = options.SlidingExpiration;
            SlidingExpirationTime = options.SlidingExpiration.HasValue ? DateTimeOffset.UtcNow.ToChinaStandardTime().Add(options.SlidingExpiration.Value) : DateTimeOffset.UtcNow.ToChinaStandardTime();
            return this;
        }

        /// <summary>
        ///     判断缓存是否过期。
        /// </summary>
        public bool IsExpired()
        {
            if (SlidingExpiration.HasValue)
            {
                if (SlidingExpirationTime <= DateTimeOffset.UtcNow.ToChinaStandardTime())
                {
                    return true;
                }

                if (AbsoluteExpiration.HasValue && SlidingExpirationTime >= AbsoluteExpiration.Value)
                {
                    return true;
                }
            }

            if (AbsoluteExpiration.HasValue && AbsoluteExpiration.Value <= DateTimeOffset.UtcNow.ToChinaStandardTime())
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    ///     用来封装 Caches ，设置有效期。
    /// </summary>
    public class CacheWrapper<TSource> where TSource : class
    {
        public CacheWrapper(CacheType cacheType)
        {
            CacheType = cacheType;
            InitializationTime = DateTimeOffset.UtcNow.ToChinaStandardTime();
        }

        private CacheWrapper()
        {
        }

        /// <summary>
        ///     缓存的值。
        /// </summary>
        public List<TSource> Caches { get; set; }

        /// <summary>
        ///     缓存值的类型。
        /// </summary>
        public CacheType CacheType { get; set; }

        /// <summary>
        ///     初始化时间。
        /// </summary>
        public DateTimeOffset InitializationTime { get; set; }

        /// <summary>
        ///     绝对过期时间。
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <summary>
        ///     滑动过期时长，如果设置了绝对过期时间，滑动过期时长不会延伸超出绝对过期时间。
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }

        /// <summary>
        ///     滑动过期时间点。
        /// </summary>
        public DateTimeOffset SlidingExpirationTime { get; set; }

        public static CacheWrapper<TSource> BuildCacheWrapper(List<TSource> caches, CacheType cacheType, KolibreCacheOptions options)
        {
            if (options == null)
            {
                return new CacheWrapper<TSource>
                {
                    Caches = caches,
                    CacheType = cacheType,
                    InitializationTime = DateTimeOffset.UtcNow.ToChinaStandardTime(),
                    AbsoluteExpiration = DateTimeOffset.MaxValue
                };
            }

            return new CacheWrapper<TSource>
            {
                Caches = caches,
                CacheType = cacheType,
                InitializationTime = DateTimeOffset.UtcNow.ToChinaStandardTime(),
                AbsoluteExpiration = options.AbsoluteExpiration,
                SlidingExpiration = options.SlidingExpiration,
                SlidingExpirationTime = options.SlidingExpiration.HasValue ? DateTimeOffset.UtcNow.ToChinaStandardTime().Add(options.SlidingExpiration.Value) : DateTimeOffset.UtcNow.ToChinaStandardTime()
            };
        }


        /// <summary>
        ///     刷新缓存
        /// </summary>
        public (bool needRestore, CacheWrapper<TSource> cacheWrapper) Refresh()
        {
            if (SlidingExpiration.HasValue && SlidingExpirationTime > DateTimeOffset.UtcNow.ToChinaStandardTime())
            {
                SlidingExpirationTime = DateTimeOffset.UtcNow.ToChinaStandardTime() + SlidingExpiration.Value;

                if (AbsoluteExpiration.HasValue && SlidingExpirationTime >= AbsoluteExpiration.Value)
                {
                    SlidingExpirationTime = AbsoluteExpiration.Value;
                }

                return (true, this);
            }

            return (false, this);
        }

        public CacheWrapper<TSource> Refresh(KolibreCacheOptions options)
        {
            AbsoluteExpiration = options.AbsoluteExpiration;
            SlidingExpiration = options.SlidingExpiration;
            SlidingExpirationTime = options.SlidingExpiration.HasValue ? DateTimeOffset.UtcNow.ToChinaStandardTime().Add(options.SlidingExpiration.Value) : DateTimeOffset.UtcNow.ToChinaStandardTime();
            return this;
        }

        /// <summary>
        ///     判断缓存是否过期。
        /// </summary>
        public bool IsExpired()
        {
            if (SlidingExpiration.HasValue)
            {
                if (SlidingExpirationTime <= DateTimeOffset.UtcNow.ToChinaStandardTime())
                {
                    return true;
                }

                if (AbsoluteExpiration.HasValue && SlidingExpirationTime >= AbsoluteExpiration.Value)
                {
                    return true;
                }
            }

            if (AbsoluteExpiration.HasValue && AbsoluteExpiration.Value <= DateTimeOffset.UtcNow.ToChinaStandardTime())
            {
                return true;
            }

            return false;
        }
    }


    public static class CacheWrapperExtensions
    {
        public static CacheWrapper SetCache(this CacheWrapper cacheWrapper, string cache, CacheType cacheType, KolibreCacheOptions options)
        {
            if (cacheWrapper == null)
            {
                throw new ArgumentNullException(nameof(cacheWrapper));
            }

            cacheWrapper.Cache = cache;
            cacheWrapper.CacheType = cacheType;

            if (!cacheWrapper.AbsoluteExpiration.HasValue)
            {
                cacheWrapper.AbsoluteExpiration = options.AbsoluteExpiration.HasValue ? options.AbsoluteExpiration.Value : DateTimeOffset.MaxValue;
            }
            if (!cacheWrapper.SlidingExpiration.HasValue)
            {
                cacheWrapper.SlidingExpiration = options.SlidingExpiration;
            }
            if (cacheWrapper.SlidingExpiration.HasValue)
            {
                cacheWrapper.SlidingExpirationTime = DateTimeOffset.UtcNow.ToChinaStandardTime().Add(cacheWrapper.SlidingExpiration.Value);
            }

            return cacheWrapper;
        }

        public static CacheWrapper<TSource> SetCache<TSource>(this CacheWrapper<TSource> cacheWrapper, List<TSource> caches, CacheType cacheType, KolibreCacheOptions options) where TSource : class
        {
            if (cacheWrapper == null)
            {
                throw new ArgumentNullException(nameof(cacheWrapper));
            }

            cacheWrapper.Caches = caches;
            cacheWrapper.CacheType = cacheType;

            if (!cacheWrapper.AbsoluteExpiration.HasValue)
            {
                cacheWrapper.AbsoluteExpiration = options.AbsoluteExpiration.HasValue ? options.AbsoluteExpiration.Value : DateTimeOffset.MaxValue;
            }
            if (!cacheWrapper.SlidingExpiration.HasValue)
            {
                cacheWrapper.SlidingExpiration = options.SlidingExpiration;
            }
            if (cacheWrapper.SlidingExpiration.HasValue)
            {
                cacheWrapper.SlidingExpirationTime = DateTimeOffset.UtcNow.ToChinaStandardTime().Add(cacheWrapper.SlidingExpiration.Value);
            }

            return cacheWrapper;
        }
    }
}