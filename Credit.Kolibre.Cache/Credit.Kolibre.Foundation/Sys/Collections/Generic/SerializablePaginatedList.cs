// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : SerializablePaginatedList.cs
// Created          : 2017-04-12  2:17 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Credit.Kolibre.Foundation.Sys.Collections.Generic
{
    public class SerializablePaginatedList<T>
    {
        public SerializablePaginatedList()
        {
        }

        public SerializablePaginatedList(int pageIndex, int pageSize, int totalCount, IEnumerable<T> source)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            HasNextPage = PageIndex < TotalPageCount - 1;
            Data = new List<T>(source);
        }

        public List<T> Data { get; set; }

        public bool HasNextPage { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPageCount { get; set; }
    }
}