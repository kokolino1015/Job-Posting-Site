using System.ComponentModel.DataAnnotations;

namespace Job_Posting_Site.Models.CategoryViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
