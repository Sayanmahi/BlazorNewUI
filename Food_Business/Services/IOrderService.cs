using BlazorFastFood.DTO;
using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Food_Business.Services
{
    public interface IOrderService
    {
        Task<List<GetAllOrderDTO>> GetAllOrders();
        Task<List<GetAllOrderDTO>> GetOrdersNotDelivered();
        Task<bool> ValidateisDelieredOrder(int id);
        Task<bool> PlaceOrder(List<Order> orders);
        Task<List<MyOrder>> MyOrders(int id);
        Task<List<MyOrder>> ShowMyUndeliveredOrders(int id);
        Task<bool> OrderIsPreparing(int id);

    }
}
