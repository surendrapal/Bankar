using System.ComponentModel.DataAnnotations;
using System;

namespace BM.Web.ViewModels
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage="Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public string ParentName { get; set; }
    }
}
