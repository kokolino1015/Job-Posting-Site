using Job_Posting_Site.Data;
using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Models.AdViewModel;
using System.Security.Claims;

namespace Job_Posting_Site.Services
{
    public class AdService
    {
        private readonly ApplicationDbContext context;
        
        public AdService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public void Create(AdFormModel model)
        {
            Ad ad = new Ad()
            {
                Description = model.Description,
                Category = model.Category,
                Owner = model.Owner
            };
            context.Ads.Add(ad);
            context.SaveChanges();
        }
        public List<Category> GetCategories()
        {
            return context.Categories.Where(r => r.Id > -1).Select(x => new Category
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
        public bool CheckIfOwner(int id, ApplicationUser user)
        {
            Ad ad = context.Ads.Find(id);
            if (user != ad.Owner)
            {
                return false;
            }
            return true;
        }
        public AdFormModel GetAdById(int id)
        {
            return context.Ads.Where(x => x.Id == id).Select(ad => new AdFormModel
            {
                Id = id,
                Description = ad.Description,
                Category = ad.Category,
                Owner = ad.Owner
            }).FirstOrDefault();
        }
        public void Update(AdFormModel model)
        {
            Ad ad = context.Ads.Find(model.Id);
            ad.Description = model.Description;
            ad.Category= model.Category;
            this.context.SaveChanges();
        }

    }
}
