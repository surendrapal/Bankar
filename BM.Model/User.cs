using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }
        [Required(ErrorMessage = "User Name is required.")]
        [Column("UserName")]
        [Display(Name = "User Name")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Column("PasswordHash")]
        [Display(Name = "Password")]
        public override string PasswordHash { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Column("FirstName")]
        [Display(Name = " First Name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        
        [Column("LastName")]
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Column("Email")]
        [Display(Name = "Email")]
        [MaxLength(50)]
        public string Email { get; set; }
        
       // DateTime createdDate = DateTime.Now;
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate{ get; set; }// { get { return createdDate; } set { this.createdDate = value; } }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("CompanyId")]
        public virtual ICollection<Company> Companies { get; set; }
    }
}