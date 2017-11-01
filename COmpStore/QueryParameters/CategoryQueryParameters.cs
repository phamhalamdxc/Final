using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.QueryParameters
{
    public class CategoryQueryParameters
    {
        private const int maxPageCount = 100;
        public int Page { get; set; } = 1;

        private int _pageCount = 100;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > maxPageCount) ? maxPageCount : value; }
        }

        public bool HasSearch { get { return !String.IsNullOrEmpty(Search); } }
        public string Search { get; set; }

        //public string OrderBy { get; set; } = "CategoryName";
        // public bool Descending
        //{
        //    get
        //    {
        //        if (!String.IsNullOrEmpty(OrderBy))
        //        {
        //            return OrderBy.Split(' ').Last().ToLower().StartsWith("desc");
        //        }
        //        return false;
        //    }
        //}
    }
}
