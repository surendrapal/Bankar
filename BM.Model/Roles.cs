using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Models
{
    public class Role : IdentityRole
    {

        public Role() : base() { }

        public Role(string name, string description)
            : base(name)
        {

            this.Description = description;

        }

        public virtual string Description { get; set; }

    }

}
