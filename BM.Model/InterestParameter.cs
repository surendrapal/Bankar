using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Column("Rate")]
        [Display(Name = "Rate")]
        public int? Rate { get; set; }

        [Column("ApplicableFrom")]
        [Display(Name = "Applicable From")]
        [DataType(DataType.DateTime)]
        public DateTime? ApplicableFrom { get; set; }

        [Display(Name = "Applicable To")]
        [DataType(DataType.DateTime)]
        public DateTime? ApplicableTo { get; set; }

        public Guid LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }

        //[Display(Name = "Interest Style")]
        //public virtual InterestStyle InterestStyles { get; set; }

        //[Display(Name = "Interest Balance")]
        //public virtual InterestBalance InterestBalance { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }

    public enum InterestStyle
    {
        [Description("30 Days Month")]
        DaysMonth30=1,
        [Description("365 Day Year")]
        DayYear365=2,
        [Description("Calendar Month")]
        CalendarMonth=3,
        [Description("Calendar Year")]
        CalendarYear=4
    }

    public enum InterestBalance
    {
        [Description("All Balances")]
        AllBalance=1,
        [Description("Credit Balance Only")]
        CreditBalance=2,
        [Description("Debit Balance Only")]
        DebitBalance=3
    }
}
