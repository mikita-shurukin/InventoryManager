using AutoMapper;
using InventoryManager.DAL.Models;
using InventoryManager.Models.Requests.Item;
using InventoryManager.Models.Requests.ItemGroup;

namespace InventoryManager.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig() {
            CreateMap<CreateItemRequest, Item>();

            CreateMap<CreateItemGroupRequest, ItemGroup>();
        }
    }
}
