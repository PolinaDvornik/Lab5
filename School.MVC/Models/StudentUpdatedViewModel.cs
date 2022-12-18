using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace School.MVC.Models
{
    public class StudentUpdatedViewModel
    {
        [Required(ErrorMessage = "Required surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Required name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Required middle name")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "Required birth date")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "Required sex")]
        public string Sex { get; set; }


        [Required(ErrorMessage = "Required address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Required mother full name")]
        public string MotherFullName { get; set; }


        [Required(ErrorMessage = "Required futher full name")]
        public string FutherFullName { get; set; }


        public string AdditionalInfo { get; set; }


        [Required(ErrorMessage = "Required class")]
        public string Class { get; set; }


        [ValidateNever]
        public List<string> Classes { get; set; }

        [ValidateNever]
        public List<string> Sexes { get; set; }
    }
}
