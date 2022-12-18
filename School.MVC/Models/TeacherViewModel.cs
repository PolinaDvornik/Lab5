using School.MVC.DAL.Models;

namespace School.MVC.Models
{
    public class TeacherViewModel
    {
        public List<Teacher> Teachers { get; set; }
        public List<string> Subjects { get; set; }
        public List<string> Classes { get; set; }
    }
}
