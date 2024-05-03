using AutoMapper;
using InventoryManager.DAL.Models;
using InventoryManager.Models.Requests.Item;
using InventoryManager.Models.Requests.ItemGroup;
using InventoryManager.Models.Requests.Warehouse;

namespace InventoryManager.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig() {
            CreateMap<CreateItemRequest, Item>();
            CreateMap<UpdateItemRequest, Item>();

            CreateMap<CreateItemGroupRequest, ItemGroup>();

            CreateMap<CreateWarehouseRequest, Warehouse>();
        }
    }
}
