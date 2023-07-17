using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Data;
using System.Security.Claims;

namespace Job_Posting_Site.Services
{
    public class CommonService
    {
        private readonly ApplicationDbContext context;
        public CommonService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public ApplicationUser FindUser(ClaimsPrincipal user)
        {
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return (ApplicationUser)context.Users.FirstOrDefault(x => x.Id == userId);
        }
        public string OwnerName(ApplicationUser user)
        {
            return user.FirstName + " " + user.LastName;
        }

    }
}