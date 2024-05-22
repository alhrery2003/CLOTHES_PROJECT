using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHES_PROJECT.Models
{
    public class WORKS_ON
    {
        [ForeignKey("Employee")]
        public int ESSN { get; set; }
        [ForeignKey("Project")]
        public int PNO { get; set; }
        [Required(ErrorMessage = "Working Hours Is Required")]
        [Range(30, 500, ErrorMessage = "Working Hours per month must be Between 30 and 500")]
        public int Hours { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Project? Project { get; set; }

    }
}
