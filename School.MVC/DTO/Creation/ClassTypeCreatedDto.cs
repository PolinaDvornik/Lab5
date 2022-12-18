using System.ComponentModel.DataAnnotations;

namespace School.MVC.DTO.Creation
{
    public class ClassTypeCreatedDto
    {
        [Required(ErrorMessage = "Required name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Required description")]
        public string Description { get; set; }
    }
}
