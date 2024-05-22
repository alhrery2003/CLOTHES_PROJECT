using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHES_PROJECT.Models
{
    public class Employee
    {
        [Key]
        public int SSN { get; set; }
        [ForeignKey("Employee")]
        public int? SUPERSSN { get; set; }
        [Required(ErrorMessage = "First Name Is Required")]
        [RegularExpression("[a-zA-Z]{3,20}",ErrorMessage ="First Name Must Be only Characters and between 3:20 Characters")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "Last Name Must Be only Characters and between 3:20 Characters")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "Address Is Required")]
        [RegularExpression("[a-zA-Z_ ]{3,30}", ErrorMessage = "Address Name Must Be only Characters and between 3:30 Characters")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Sex Is Required")]
        public char Sex { get; set; }
        [Required(ErrorMessage = "Salary Is Required")]
        [Range(2000,10000,ErrorMessage ="Salary Must be Between 2000 and 10000")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Birth Date Is Required")]
        [Range(typeof(DateTime), "1/1/1990", "12/31/2006",ErrorMessage ="Birth Date of Employee Must be between 1990 and 2006")]
        public DateTime Bdate { get; set; }

        [ForeignKey("Department")]
        public int DNUM { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Dependent? Dependent { get; set; }
        public virtual ICollection<WORKS_ON>? WORKS_ONs { get; set; }

    }
}
