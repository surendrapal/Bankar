using System.Collections.Generic;
using System.Web.Mvc;

namespace BM.Web.ViewModels
{
    public class ManageRolesViewModel
    {
        public SelectList Roles { get; set; }
        public string[] RoleList { get; set; }
    }
}
