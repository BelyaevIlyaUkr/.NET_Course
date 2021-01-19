using System;

namespace StudyManager.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PassingScore { get; set; }
    }
}
