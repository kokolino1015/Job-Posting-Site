using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Data.Entities.Account;
using System.ComponentModel.DataAnnotations;

namespace Job_Posting_Site.Models.AdViewModel
{
    public class AdFormModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Category { get; set; }
        [Required]
        public ApplicationUser Owner { get; set; }
    }
}
