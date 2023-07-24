using Job_Posting_Site.Data.Entities.Account;

namespace Job_Posting_Site.Models.CategoryViewModel
{
    public class CategoryFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser Owner { get; set; }
        public bool IsDeleted { get; set; }
    }
}
