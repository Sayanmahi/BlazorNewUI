using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFastFood.Services
{
    public interface IItemService
    {
        Task<bool> AddItem(Item item);
        Task<bool> EditItem(Item item);
        Task<bool> InActiveItem(int id);
        Task<bool> ActivateItem(int id);
        Task<List<Item>> GetItemsBasedOnCategory(int CategoryId);
        Task<List<Item>> GetAllItems();
        Task<Item> GetItemById(int id);
        Task<bool> ChangeCategory(int itemId,int categoryId);
    }
}
