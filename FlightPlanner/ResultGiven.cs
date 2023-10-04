namespace FlightPlanner
{
    public class ResultGiven
    {
        public int Page { get; set; }
        public int totalItems { get; set; }
        public Array Items { get; set; }

        public ResultGiven(Array _items)
        {
            Page = 0;
            Items = _items;
            totalItems = _items.Length;
        }
    }
}
