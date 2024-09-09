namespace workshop.wwwapi.Models
{
    public class Course
    {

        public int StudentId { get; set; }
        public int TeacherId { get; set; }

        public string Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }

    }
}
