using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Models
{
    public class InterestParameter
    {
        public InterestParameter()
        {
            CreatedDate = DateTime.Now;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Column("Rate")]
        [Display(Name = "Rate")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Applicable From is required.")]
        [Column("ApplicableFrom")]
        [Display(Name = "Applicable From")]
        [DataType(DataType.DateTime)]
        public DateTime ApplicableFrom { get; set; }

        [Required(ErrorMessage = "Applicable To is required.")]
        [Column("ApplicableTo")]
        [Display(Name = "Applicable To")]
        [DataType(DataType.DateTime)]
        public DateTime ApplicableTo { get; set; }

        public virtual ICollection<InterestStyle> InterestStyles { get; set; }
        public virtual ICollection<InterestBalance> InterestBalances { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }

    public enum InterestStyle
    {
        DaysMonth30 = "30 Days Month",
        DayYear365 = "365 Day Year",
        CalendarMonth = "Calendar Month",
        CalendarYear = "Calendar Year"
    }

    public enum InterestBalance
    {
        AllBalance = "All Balances",
        CreditBalance = "Credit Balance Only",
        DebitBalance = "Debit Balance Only"
    }
}
