using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data
{
    public class DataContext : DbContext
    {
        private string _connection;

        public DataContext( DbContextOptions<DataContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connection = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasKey(c => new { c.StudentId, c.TeacherId });

            //list of students to seed
            List<Student> students = new List<Student>()
            {
                new Student(){ Id=1, Name="Kaja"},
                new Student(){ Id=2, Name="John"}

            };

            //single teacher to seed
            Teacher teacher = new Teacher() { Id = 1, Name = "Nigel" };

            //list of course to map student => teacher
            List<Course> courses = new List<Course>()
            {
                new Course() {  Subject ="C# for Kaja", StudentId=1, TeacherId=1 },
                new Course() {  Subject ="C# for John", StudentId=2, TeacherId=1 },
            };
                
            
            //seed data
            modelBuilder.Entity<Teacher>().HasData(teacher);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Course>().HasData(courses);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connection);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
