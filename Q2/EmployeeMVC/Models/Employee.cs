using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage = "Please enter valid employee number")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter valid employee name")]
        [StringLength(10, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string? Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter valid employee salary")]
        public decimal Salary { get; set; }

        public string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Salary: " + Salary;
        }
    }
}