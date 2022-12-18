using School.MVC.DAL.Models;

namespace School.MVC.Models
{
    public class StudentViewModel
    {
        public List<Student> Students { get; set; }
        public List<string> Classes { get; set; }
        public List<string> ClassTypes { get; set; }
        public List<string> Subjects { get; set; }
    }
}
