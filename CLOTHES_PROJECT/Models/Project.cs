using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHES_PROJECT.Models
{
    public class Project
    {
        [Key]
        public int PNUMBER { get; set; }
        [Required(ErrorMessage = "Project Name Is Required")]
        [RegularExpression("[a-zA-Z_ ]{3,20}", ErrorMessage = "First Name Must Be only Characters and between 3:20 Characters")]
        public string Pname { get; set; }
        [Required(ErrorMessage = "Location Is Required")]
        [RegularExpression("[a-zA-Z_ ]{3,25}", ErrorMessage = "First Name Must Be only Characters and between 3:25 Characters")]
        public string Plocation { get; set; }
        [ForeignKey("Department")]
        public int DNUM { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<WORKS_ON>? WORKS_ONs { get; set; } /*= new HashSet<WORKS_ON>();*/

    }
}
