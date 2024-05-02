namespace InventoryManager.Models.Requests
{
    public class CreateItemRequest
    {
        public int GroupId { get; set; }
        public decimal Price { get; set; }
    }
}
