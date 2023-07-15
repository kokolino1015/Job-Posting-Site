using Job_Posting_Site.Data.Entities.Account;

namespace Job_Posting_Site.Data.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ApplicationUser> Candidates { get; set; }
    }
}
