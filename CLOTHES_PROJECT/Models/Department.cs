using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHES_PROJECT.Models
{
    public class Department
    {
        [Key]
        public int DNUMBER { get; set; }
        [Required(ErrorMessage = "Department Name Is Required")]
        [RegularExpression("[a-zA-Z_ ]{3,20}", ErrorMessage = "First Name Must Be only Characters and between 3:20 Characters")]
        public string DName { get; set; }
        [Required(ErrorMessage = "Location Is Required")]
        [RegularExpression("[a-zA-Z_ ]{3,25}", ErrorMessage = "First Name Must Be only Characters and between 3:25 Characters")]
        public string DLocation { get; set; }

        [ForeignKey("Employee")]
        public int? MGRSSN { get; set; }
        [Required(ErrorMessage = "Manager start date Is Required")]
        [Range(typeof(DateTime), "1/1/2010", "12/31/9999", ErrorMessage = "Manager start date is at least 2010")]
        public DateTime MGRStartDate { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }

    }
}
