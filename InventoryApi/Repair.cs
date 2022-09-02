namespace InventoryApi
{
    public class Repair
    {
        public int Id { get; set; }
        public string ContactNo { get; set; }
        public string Issue { get; set; }
        public int Status { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public double Fee { get; set; }

    }
}
