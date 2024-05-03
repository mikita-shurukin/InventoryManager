namespace InventoryManager.Models.Requests.Item
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}
