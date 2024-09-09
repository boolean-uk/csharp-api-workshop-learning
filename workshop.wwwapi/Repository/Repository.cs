using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _db.Courses.Include(c => c.Teacher).Include(c => c.Student).ToListAsync();
        }
    }
}
