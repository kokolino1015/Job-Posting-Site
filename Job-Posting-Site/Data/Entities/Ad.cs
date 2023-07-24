using Job_Posting_Site.Data.Entities.Account;

namespace Job_Posting_Site.Data.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public List<ApplicationUser> Candidates { get; set; } = new List<ApplicationUser>();
        public ApplicationUser Owner { get; set; }
        public bool isDeleted { get; set; }
    }
}
