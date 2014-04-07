using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Models
{
    public class Company
    {
        public Company()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Column("Name")]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mailing Name is required.")]
        [Column("MailingName")]
        [Display(Name = "Mailing Name")]
        [MaxLength(100)]
        public string MailingName { get; set; }

        [Required(ErrorMessage = "Address 1 is required.")]
        [Column("Address1")]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Column("Address2")]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Column("Address3")]
        [Display(Name = "Address 3")]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public Guid CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual Location City { get; set; }

        [Required(ErrorMessage = "Pin Code is required.")]
        [Display(Name = "Pin Code")]
        [MaxLength(10)]
        [DataType(DataType.PostalCode)]
        public string PinCode { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Display(Name = "Mobile")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Display(Name = "Fax")]
        [MaxLength(10)]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Column("Logo")]
        [Display(Name = "Logo")]
        public byte[] Logo { get; set; }

        [Column("LogoType")]
        [ScaffoldColumn(false)]
        public string LogoType { get; set; }

        [NotMapped]
        public string ShowLogo
        {
            get
            {
                if (Logo != null)
                {
                    string base64 = Convert.ToBase64String(Logo);
                    return string.Format("data:{0};base64,{1}", LogoType, base64);
                }
                else
                    return string.Empty;
            }
        }


        [DataType(DataType.DateTime)]
        [Display(Name = "FY Start")]
        public DateTime FYStart { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Book Begining From")]
        public DateTime BookBeginingFrom { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<User> Users { get; set; }

    }
}
