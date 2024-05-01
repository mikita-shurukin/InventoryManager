using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.DAL.Models
{
    public class InventoryItem
    {
        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        [Required]
        public string UnitOfMeasurement { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
