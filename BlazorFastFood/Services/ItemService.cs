using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFastFood.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext db;
        public ItemService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> ActivateItem(int id)
        {
            var d = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (d != null)
            {
                d.IsActive = 1;
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);
        }

        public async Task<bool> AddItem(Item item)
        {
            item.IsActive = 1;
            var d= await db.Items.AddAsync(item);
            await db.SaveChangesAsync();
            return (true);
        }

        public async Task<List<Item>> AdminGetItemsBasedOnCategory(int CategoryId)
        {
            var d = await db.Items.Where(x => x.CategoryId == CategoryId).ToListAsync();
            return (d);
        }

        public async Task<bool> ChangeCategory(int itemId, int categoryId)
        {
            var d= await db.Items.FirstOrDefaultAsync(x => x.Id==itemId);
            if(d!=null)
            {
                d.CategoryId = categoryId;
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);

        }

        public async Task<bool> EditItem(Item item)
        {
            var d= await db.Items.FirstOrDefaultAsync(x=>x.Id==item.Id);
            if(d!=null)
            {
                d.ProdName = item.ProdName;
                d.Price= item.Price;
                d.Description = item.Description;
                d.CategoryId = item.CategoryId;
                d.ImageUrl = item.ImageUrl;
                d.IsActive = item.IsActive;
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);
        }

        public async Task<List<Item>> GetAllItems()
        {
            var d = await db.Items.ToListAsync();
            return d;
        }

        public async Task<Item> GetItemById(int id)
        {
            var d = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
            return d;
        }

        public async Task<List<Item>> GetItemsBasedOnCategory(int Categoryid)//show only active Item
        {
            var d= await db.Items.Where(x=> x.CategoryId==Categoryid && x.IsActive==1).ToListAsync();
            return d;
        }

        public async Task<bool> InActiveItem(int id)
        {
            var d= await db.Items.FirstOrDefaultAsync(x=> x.Id==id);
            if(d!=null)
            {
                d.IsActive = 0;
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);
        }

       
    }
}
