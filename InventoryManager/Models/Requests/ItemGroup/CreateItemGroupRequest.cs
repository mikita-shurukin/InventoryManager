using System.ComponentModel.DataAnnotations;

namespace InventoryManager.Models.Requests.ItemGroup
{
    public class CreateItemGroupRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
