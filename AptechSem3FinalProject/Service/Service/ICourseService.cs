using System.Collections.Generic;
using Context.Database;

namespace Service.Service
{
    public interface ICourseService : IService<Course>
    {
        IEnumerable<Course> GetLastedCourse();
        IEnumerable<Course> GetByCreatedUser(int createdUser);
        bool ValidateCourseAccessible(int userId, int id);
    }
}