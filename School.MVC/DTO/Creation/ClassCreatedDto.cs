namespace School.MVC.DTO.Creation
//TODO: модели для переноса данных между слоями
{
    
    public class ClassCreatedDto
    {
        public string Number { get; set; }
        public int StudentsCount { get; set; }
        public int CreationYear { get; set; }
        public int TeacherId { get; set; }
        public int ClassTypeId { get; set; }
    }
}
