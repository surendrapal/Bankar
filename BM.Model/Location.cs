using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Models
{
    public class Location
    {
        public Location()
        {
            CreatedDate = DateTime.Now;
            // this.location = new HashSet<Location>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [Column("Name")]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

       // [Required(ErrorMessage = "Parent Location is required.")]
        [Display(Name = "Parent")]
        [Column("ParentId")]
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Location ParentLocation { get; set; }
        public virtual List<Location> ChildLocations { get; set; }

       
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }

    public enum LocationType
    {
        Country=1,
        State=2,
        City=3
    }
}
