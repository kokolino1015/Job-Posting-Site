using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Data;
using System.Security.Claims;
using Job_Posting_Site.Data.Entities;

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
            return context.Users.FirstOrDefault(x => x.Id == userId);
        }
        public Role FindRole(ClaimsPrincipal user)
        {
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = context.Users.Where(x => x.Id == userId).Select(x => new ApplicationUser() { 
                Role = x.Role,
            }).FirstOrDefault();
            if(role != null) {
                return context.Roles.Where(x => x.Id == role.Role.Id).Select(x => new Role()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).FirstOrDefault();
            }
            return null;
            
        }
        public string OwnerName(ApplicationUser user)
        {
            return user.FirstName + " " + user.LastName;
        }

    }
}