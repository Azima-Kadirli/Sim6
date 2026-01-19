using Sim6.Models.Common;

namespace Sim6.Models
{
    public class Project:BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
