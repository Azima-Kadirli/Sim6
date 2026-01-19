using System.ComponentModel.DataAnnotations;

namespace Sim6.ViewModel.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }
    }
}
