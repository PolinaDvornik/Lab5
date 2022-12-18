namespace School.MVC.DTO.Creation
{
    public class ScheduleCreatedDto
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public string StartLessonTime { get; set; }
        public string EndLessonTime { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}
