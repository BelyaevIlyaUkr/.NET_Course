using System;

namespace StudyManager.Models
{
    public class Grade
    {
        public int GradeID { get; set; }
        public DateTime GradeDate { get; set; }
        public bool IsComplete { get; set; }
        public int HomeTaskID { get; set; }
        public int StudentID { get; set; }
    }
}
