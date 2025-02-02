﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures.MetaData
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }

    public class PagedList<T> : List<T>
    {
        public MetaData metaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            metaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source,int totalCount, int pageNumber, int pageSize)
        {
            var count = totalCount;
            var items = source.ToList();
               /* .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();*/
            

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }

}
