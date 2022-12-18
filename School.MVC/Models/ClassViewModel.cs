using School.MVC.DAL.Models;

namespace School.MVC.Models
{
    public class ClassViewModel
    {
        public List<Class> Classes { get; set; }
        public List<string> ClassTypes { get; set; }
    }
}
