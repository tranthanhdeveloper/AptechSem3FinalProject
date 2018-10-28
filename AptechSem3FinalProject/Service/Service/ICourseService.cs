using System.Collections.Generic;
using Context.Database;

namespace Service.Service
{
    public interface ICourseService : IService<Course>
    {
        IEnumerable<Course> GetLastedCourse();
    }
}