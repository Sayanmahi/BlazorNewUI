using BlazorFastFood.DTO;
using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Food_Business.Services
{
    public interface ICartService
    {
        Task<List<MyOrder>> ShowMyCart(int uid);
        Task<bool> AddToCart(Cart cart);
        Task<bool> EditItem(Cart cart);
        Task<bool> DeleteItem(int id);



    }
}
