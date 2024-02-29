using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFastFood.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;
        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var d= await db.Categories.FirstOrDefaultAsync(x=> x.Id==id);
            return (d);
        }
        public async Task<bool> AddCategory(Category category)
        {
            var d= await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditCategory(Category category)
        {
            var d= await db.Categories.FirstOrDefaultAsync(x=> x.Id==category.Id);
            if(d!=null)
            {
                d.Name = category.Name;
                d.ImageUrl=category.ImageUrl;
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);

        }

        public async Task<List<Category>> GetAllCategories()
        {
            var d = await db.Categories.ToListAsync();
            return d;
        }
    }
}
