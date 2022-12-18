using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace School.MVC.Models
{
    public class TeacherUpdatedViewModel
    {
        [Required(ErrorMessage = "Required surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Required name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Required middle name")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "Required position")]
        public string Position { get; set; }


        [ValidateNever]
        public List<string> Positions { get; set; }
    }
}
