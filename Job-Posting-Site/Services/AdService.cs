using Job_Posting_Site.Data;
using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Models.AdViewModel;
using System.Diagnostics.Metrics;
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
                Category = context.Categories.Where(x => x.Id == model.Category).FirstOrDefault(),
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
        public Category GetCategoryById(int id)
        {
            return context.Categories.Where(x => x.Id == id).FirstOrDefault();
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
                Category = ad.Category.Id,
                Owner = ad.Owner,
                Candidates = ad.Candidates
            }).FirstOrDefault();
        }
        public bool checkifUserInCandidateList(AdFormModel model, ApplicationUser user)
        {
            var available = true;
            foreach (ApplicationUser u in model.Candidates)
            {
                if (u.Id == user.Id || user.Role.Name == "employ")
                {
                    available = false;
                }
            }
            return available;
        }   
        public void Update(AdFormModel model)
        {
            Ad ad = context.Ads.Find(model.Id);
            ad.Description = model.Description;
            ad.Category= context.Categories.Where(x => x.Id == model.Category).FirstOrDefault();
            this.context.SaveChanges();
        }
        public void Delete(int id)
        {
            this.context.Ads.Remove(this.context.Ads.Find(id));
            this.context.SaveChanges();
        }
        public void ApplyUser(ApplicationUser user, AdFormModel model)
        {
            var ad = context.Ads.Where(x => x.Id == model.Id).FirstOrDefault();
            ad.Candidates.Add(user);
            this.context.SaveChanges();
        }
        public void UnapplyUser(ApplicationUser user, AdFormModel model)
        {
            var ad = context.Ads.Where(x => x.Id == model.Id).FirstOrDefault();
            ad.Candidates.Remove(user);
            this.context.SaveChanges();
        }
        public List<AdFormModel> GetFirst10Ads()
        {
            return context.Ads.Take(10).Select(x => new AdFormModel()
            {
                Id = x.Id,
                Candidates = x.Candidates,
                Category = x.Category.Id,
                Description = x.Description,
                Owner = x.Owner
            }).ToList();
        }
    }
}
