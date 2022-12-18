using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace School.MVC.Models
{
    public class ScheduleUpdatedViewModel
    {
        [Required(ErrorMessage = "Required date and time")]
        public DateTime DateTime { get; set; }


        [Required(ErrorMessage = "Required class")]
        public string Class { get; set; }


        [Required(ErrorMessage = "Required subject")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "Required teacher")]
        public string Teacher { get; set; }


        [ValidateNever]
        public List<string> Classes { get; set; }

        [ValidateNever]
        public List<string> Subjects { get; set; }

        [ValidateNever]
        public List<string> Teachers { get; set; }
    }
}
