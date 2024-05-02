using AutoMapper;
using InventoryManager.DAL.Models;
using InventoryManager.Models.Requests;

namespace InventoryManager.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig() {
            CreateMap<CreateItemRequest, Item>();
        }
    }
}
