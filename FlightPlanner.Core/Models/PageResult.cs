using System;

namespace FlightPlanner.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public Array Items { get; set; }

        public PageResult(Array _items)
        {
            Page = 0;
            Items = _items;
            TotalItems = _items.Length;
        }
    }
}