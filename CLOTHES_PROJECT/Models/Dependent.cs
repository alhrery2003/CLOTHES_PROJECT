using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHES_PROJECT.Models
{
    public class Dependent
    {
        [ForeignKey("Employee")]
        public int ESSN { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [RegularExpression("[a-zA-Z_ ]{3,25}", ErrorMessage = "Name Must Be only Characters and between 3:25 Characters")]
        public string Dependent_Name { get; set; }
        [Required(ErrorMessage = "Sex Is Required")]
        public char Sex { get; set; }
        [Required(ErrorMessage = "Birth Date Is Required")]
        [Range(typeof(DateTime), "1/1/1950", "12/31/9999", ErrorMessage = "Birth Date of Dependent is at least 1950")]
        public DateTime Bdate { get; set; }
        public string? Relationship { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
