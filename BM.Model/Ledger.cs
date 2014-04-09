using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Models
{
    public class Ledger
    {
        public Ledger()
        {
            CreatedDate = DateTime.Now;
            this.InterestParameters = new HashSet<InterestParameter>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Column("Name")]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }        

        [Display(Name = "Group")]
        [Column("GroupId")]
        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

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

        [Column("Photo")]
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }

        [Column("PhotoType")]
        [ScaffoldColumn(false)]
        public string PhotoType { get; set; }

        [NotMapped]
        public string ShowPhoto
        {
            get
            {
                if (Photo != null)
                {
                    string base64 = Convert.ToBase64String(Photo);
                    return string.Format("data:{0};base64,{1}", PhotoType, base64);
                }
                else
                    return string.Empty;
            }
        }

        [Display(Name = "Interest Parameter")]

        public virtual ICollection<InterestParameter> InterestParameters { get; set; }

        public void CreateInterestParameters(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                InterestParameters.Add(new InterestParameter());
            }
        }

        [Display(Name = "Opening Balance")]
        [DataType(DataType.Currency)]
        public decimal? OpeningBalance { get; set; }
        
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }
}
