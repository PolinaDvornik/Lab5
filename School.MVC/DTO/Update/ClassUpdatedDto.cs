namespace School.MVC.DTO.Update
{
    public class ClassUpdatedDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int StudentsCount { get; set; }
        public int CreationYear { get; set; }
        public int TeacherId { get; set; }
        public int ClassTypeId { get; set; }
    }
}
