using System.ComponentModel.DataAnnotations;

namespace School.MVC.DTO.Update
{
    public class SubjectUpdatedDto
    {
        [Required(ErrorMessage = "Required id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Required name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Required description")]
        public string Description { get; set; }
    }
}
