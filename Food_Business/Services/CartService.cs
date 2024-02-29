using BlazorFastFood.DTO;
using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Business.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext db;
        public CartService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> AddToCart(Cart cart)
        {
            var itemPrice = await db.Items.FindAsync(cart.ItemId);
            cart.Price = cart.Qty * itemPrice.Price;
            var d= await db.Carts.AddAsync(cart);
            await db.SaveChangesAsync();
            return (true);
        }


        public async Task<bool> DeleteItem(int id)
        {
            var d = await db.Carts.FindAsync(id);
            if(d!= null)
            {
                db.Carts.Remove(d); 
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);
        }

        public async Task<bool> EditItem(Cart cart)
        {
            var d = await db.Carts.FindAsync(cart.Id);
            if(d!=null)
            {
                var dd = await db.Items.FirstOrDefaultAsync(x => x.Id == d.ItemId);
                d.Qty = cart.Qty;
                d.Price = cart.Qty * dd.Price;
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);

        }

        public async Task<List<MyOrder>> ShowMyCart(int uid)
        {
            var d = await db.Carts.Where(x => x.UserId == uid).ToListAsync();
            List<MyOrder> list = new List<MyOrder>();   
            foreach(var i in d)
            {
                var itemName = await db.Items.FindAsync(i.ItemId);
                var dto = new MyOrder()
                {
                    Id = i.ItemId,
                    Qty = i.Qty,
                    Price = i.Price,
                    ImageUrl=itemName.ImageUrl,
                    ItemName = itemName.ProdName
                };
                list.Add(dto);
            }
            return(list);
        }
    }
}
