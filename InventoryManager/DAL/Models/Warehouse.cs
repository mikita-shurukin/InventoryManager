using System.ComponentModel.DataAnnotations;

namespace InventoryManager.DAL.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string StorageLocation { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string ContactPerson { get; set; }

        public virtual IEnumerable<InventoryItem> InventoryItems { get; set; }
    }
}
