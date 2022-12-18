using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace School.MVC.Models
{
    public class ClassUpdatedViewModel
    {
        [Required(ErrorMessage = "Required number")]
        public string Number { get; set; }


        [Required(ErrorMessage = "Required students count")]
        [Range(20, 40, ErrorMessage = "Students count should be in range from 20 till 40")]
        public int StudentsCount { get; set; }


        [Required(ErrorMessage = "Required creation year")]
        [Range(2000, 2023, ErrorMessage = "Creation year should be in range from 2000 till 2022")]
        public int CreationYear { get; set; }


        [Required(ErrorMessage = "Required teacher")]
        public string Teacher { get; set; }


        [Required(ErrorMessage = "Required class type")]
        public string ClassType { get; set; }


        [ValidateNever]
        public List<string> Teachers { get; set; }

        [ValidateNever]
        public List<string> ClassTypes { get; set; }
    }
}
