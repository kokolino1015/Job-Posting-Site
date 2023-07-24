using Job_Posting_Site.Data;
using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Models.AdViewModel;
using Job_Posting_Site.Models.CategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job_Posting_Site.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext context;
        public CategoryService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Create(CategoryFormModel model)
        {
            Category category = new Category()
            {
                Name = model.Name,
                Owner = model.Owner,
                IsDeleted= false
            };
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void Update(CategoryFormModel model)
        {
            Category category = context.Categories.Find(model.Id);
            category.Name = model.Name;
            this.context.SaveChanges();
        }
        
        public bool CheckIfOwner(int id, ApplicationUser user)
        {
            Category category = context.Categories.Find(id);
            if (user == category.Owner)
            {
                return true;
            }
            return false;
        }
        public CategoryFormModel GetCategoryById(int id)
        {
            return context.Categories.Where(x => x.Id == id).Select(Category => new CategoryFormModel
            {
                Id = id,
                Name = Category.Name,
                Owner = Category.Owner
            }).FirstOrDefault();
        }
        public void Delete(int id)
        {
            var model = this.context.Categories.Find(id);
            model.IsDeleted = true;
            this.context.SaveChanges();
        }
        public List<Ad> GetAllIdByCategoryId(int id)
        {
            return context
                .Ads
                .Where(ad => ad.Category.Id == id && !ad.isDeleted)
                .ToList();
        }
        public List<Category> GetAllCategories()
        {
            return context.Categories.Where (x => !x.IsDeleted).ToList();
        }
    }
}
