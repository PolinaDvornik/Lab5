namespace School.MVC.DTO.Update
{
    public class ScheduleUpdatedDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public string StartLessonTime { get; set; }
        public string EndLessonTime { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}
