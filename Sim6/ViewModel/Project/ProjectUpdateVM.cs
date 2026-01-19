using System.ComponentModel.DataAnnotations;

namespace Sim6.ViewModel.Project
{
    public class ProjectUpdateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
