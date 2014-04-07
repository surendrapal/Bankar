using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Models
{
    public class Group
    {
        public Group()
        {
            CreatedDate = DateTime.Now;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Column("Name")]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("Description")]
        [Display(Name = "Description")]
        [MaxLength(50)]
        public string Description { get; set; }

        [Display(Name = "Parent")]
        [Column("ParentId")]
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Group ParentGroup { get; set; }
        public virtual List<Group> ChildGroups { get; set; }

        public virtual List<Ledger> Ledgers { get; set; }
        
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }
}
