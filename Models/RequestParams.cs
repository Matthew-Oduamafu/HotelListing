﻿namespace HotelListing.Models
{
    public class RequestParams
    {
        private const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}