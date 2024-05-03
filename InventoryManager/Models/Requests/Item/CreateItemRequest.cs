namespace InventoryManager.Models.Requests.Item
{
    public class CreateItemRequest
    {
        public int GroupId { get; set; }
        public decimal Price { get; set; }
    }
}
