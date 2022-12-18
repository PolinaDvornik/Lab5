namespace School.MVC.DAL.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int StudentsCount { get; set; }
        public int CreationYear { get; set; }
        public int TeacherId { get; set; }
        public int ClassTypeId { get; set; }
        public Teacher ClassroomTeacher { get; set; } // TODO: навигационное свойство
        public ClassType ClassType { get; set; }
    }
}
