using System.Collections.Generic;
using Context.Database;

namespace Service.Service
{
    public interface ICourseService : IService<Course>
    {
        IEnumerable<Course> GetLastedCourse();
        IEnumerable<Course> GetByCreatedUser(int createdUser);
        IEnumerable<Course> GetPublished();
        IEnumerable<Course> GetInteractiveCourses();
        bool ValidateCourseAccessible(int userId, int id);
        bool ValidateCourseEditable(int userId, int id);
        bool ValidateCourseDeletable(int userId, int id);
        bool ValidatePublishState(int id);

    }
}