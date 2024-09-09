using System.Runtime.CompilerServices;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.EndPoints
{
    public static class LearningEndpoints
    {
        public static void ConfigureLearningEndpoints(this WebApplication app)
        {
            var learning = app.MapGroup("learning");
            learning.MapGet("/", GetCourses);
        }

        private static async Task<IResult> GetCourses(IRepository repository)
        {
            var courses = await repository.GetCoursesAsync();

            List<Object> results = new List<object>();

            courses.ToList().ForEach(c => { results.Add(new { Title=c.Subject, Teacher=c.Teacher.Name, Student=c.Student.Name}); });
            return TypedResults.Ok(results);
        }
    }
}
