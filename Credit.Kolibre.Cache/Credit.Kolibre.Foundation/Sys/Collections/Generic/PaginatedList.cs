// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : PaginatedList.cs
// Created          : 2017-03-22  5:04 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Credit.Kolibre.Foundation.Sys.Collections.Generic
{
    /// <summary>
    ///     分页列表。
    /// </summary>
    /// <typeparam name="T">列表中元素的类型。</typeparam>
    [DataContract]
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public PaginatedList()
        {
        }

        /// <summary>
        ///     初始化一个 <see cref="PaginatedList{T}" /> 实例。
        /// </summary>
        /// <param name="pageIndex">指定的页码索引。</param>
        /// <param name="pageSize">指定的单页元素数量。</param>
        /// <param name="totalCount">指定的原数据元素总数量。</param>
        /// <param name="source">指定的元素序列。</param>
        public PaginatedList(int pageIndex, int pageSize, int totalCount, IEnumerable<T> source) : base(source)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        #region IPaginatedList<T> Members

        /// <summary>
        ///     指示是否在原数据中，该页数据是否还有下一页。
        /// </summary>
        [DataMember]
        public bool HasNextPage
        {
            get => PageIndex < TotalPageCount - 1;
            set => HasNextPage = value;
        }

        /// <summary>
        ///     该页数据的页码索引，第一页的页码索引为0。
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        ///     获取分页时指定的单页元素数量。
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        ///     获取分页时原数据的元素总数量。
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        /// <summary>
        ///     获取分页时原数据的元素总页数。
        /// </summary>
        [DataMember]
        public int TotalPageCount { get; set; }

        #endregion

        /// <summary>
        ///     将当前的 <see cref="PaginatedList{T}" /> 实例转换为另一个 <see cref="PaginatedList{T}" /> 实例。
        /// </summary>
        /// <typeparam name="TEntity">新的<see cref="PaginatedList{T}" /> 实例的元素类型。</typeparam>
        /// <param name="selector">应用于每个元素的转换函数。</param>
        /// <returns>转换后的 <see cref="PaginatedList{T}" /> 实例。</returns>
        public IPaginatedList<TEntity> ToPaginated<TEntity>(Func<T, TEntity> selector)
        {
            return new PaginatedList<TEntity>(PageIndex, PageSize, TotalCount, this.Select(selector));
        }
    }
}