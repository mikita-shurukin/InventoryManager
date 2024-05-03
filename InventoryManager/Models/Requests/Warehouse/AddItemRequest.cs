namespace InventoryManager.Models.Requests.Warehouse
{
    public class AddItemRequest
    {
        public int WarehouseId { get; set; }
        public IEnumerable<ItemPosition> ItemPositions { get; set; }
    }
}
