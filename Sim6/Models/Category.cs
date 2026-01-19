using Sim6.Models.Common;

namespace Sim6.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
