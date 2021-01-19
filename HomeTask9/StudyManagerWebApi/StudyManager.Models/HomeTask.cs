using System;

namespace StudyManager.Models
{
    public class HomeTask
    {
        public int HomeTaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TaskDate { get; set; }
        public int SerialNumber { get; set; }
        public int CourseID { get; set; }
    }
}
