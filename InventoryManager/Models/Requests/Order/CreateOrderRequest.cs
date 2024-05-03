namespace InventoryManager.Models.Requests.Order
{
    public class CreateOrderRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int WarehouseId { get; set; }
        public string Comment { get; set; }
    }
}
