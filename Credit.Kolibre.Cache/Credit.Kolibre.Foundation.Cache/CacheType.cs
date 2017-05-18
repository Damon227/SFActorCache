// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : Credit.Kolibre.Foundation.Cache
// File             : CacheType.cs
// Created          : 2017-05-18  17:00
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

namespace Credit.Kolibre.Foundation.Cache
{
    public enum CacheType
    {
        /// <summary>
        ///     缓存正常字符串类型。
        /// </summary>
        Normal,

        /// <summary>
        ///     缓存 Hash 类型。
        /// </summary>
        Hash,

        /// <summary>
        ///     缓存 List 类型。
        /// </summary>
        List,

        /// <summary>
        ///     缓存 自定义实体。
        /// </summary>
        Object
    }
}