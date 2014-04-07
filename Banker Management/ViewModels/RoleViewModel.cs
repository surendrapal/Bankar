using System.ComponentModel.DataAnnotations;

namespace BM.Web.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage="RoleName is required.")]
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
