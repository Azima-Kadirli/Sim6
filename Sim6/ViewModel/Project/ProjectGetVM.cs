using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Sim6.ViewModel.Project
{
    public class ProjectGetVM
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
    }
}
