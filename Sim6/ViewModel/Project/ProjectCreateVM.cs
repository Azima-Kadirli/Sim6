using System.ComponentModel.DataAnnotations;

namespace Sim6.ViewModel.Project
{
    public class ProjectCreateVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        
    }
}
