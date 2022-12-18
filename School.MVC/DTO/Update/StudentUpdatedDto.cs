namespace School.MVC.DTO.Update
{
    public class StudentUpdatedDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Sex { get; set; }
        public string Address { get; set; }
        public string MotherFullName { get; set; }
        public string FutherFullName { get; set; }
        public string AdditionalInfo { get; set; }
        public int ClassId { get; set; }
    }
}
