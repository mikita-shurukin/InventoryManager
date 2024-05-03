using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }
        public virtual ItemGroup Group { get; set; }
        public string Name { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
    }
}
