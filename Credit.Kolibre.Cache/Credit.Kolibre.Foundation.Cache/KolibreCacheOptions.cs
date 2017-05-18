// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : SmartCacheActor.Interfaces
// File             : KolibreSmartCacheOptions.cs
// Created          : 2017-05-18  16:32
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace Credit.Kolibre.Foundation.Cache
{
    public class KolibreCacheOptions
    {
        private TimeSpan? _absoluteExpirationRelativeToNow;
        private TimeSpan? _slidingExpiration;

        /// <summary>
        ///     Gets or sets an absolute expiration date for the cache entry.
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <summary>
        ///     Gets or sets an absolute expiration time, relative to now.
        /// </summary>
        public TimeSpan? AbsoluteExpirationRelativeToNow
        {
            get => _absoluteExpirationRelativeToNow;
            set
            {
                TimeSpan? nullable = value;
                TimeSpan zero = TimeSpan.Zero;
                if ((nullable.HasValue ? (nullable.GetValueOrDefault() <= zero ? 1 : 0) : 0) != 0)
                    throw new ArgumentOutOfRangeException(nameof(AbsoluteExpirationRelativeToNow), value, @"The relative expiration value must be positive.");
                _absoluteExpirationRelativeToNow = value;
            }
        }

        /// <summary>
        ///     Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
        ///     This will not extend the entry lifetime beyond the absolute expiration (if set).
        /// </summary>
        public TimeSpan? SlidingExpiration
        {
            get => _slidingExpiration;
            set
            {
                TimeSpan? nullable = value;
                TimeSpan zero = TimeSpan.Zero;
                if ((nullable.HasValue ? (nullable.GetValueOrDefault() <= zero ? 1 : 0) : 0) != 0)
                    throw new ArgumentOutOfRangeException(nameof(SlidingExpiration), value, @"The sliding expiration value must be positive.");
                _slidingExpiration = value;
            }
        }
    }
}