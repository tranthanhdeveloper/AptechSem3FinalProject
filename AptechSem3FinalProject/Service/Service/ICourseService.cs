using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Service.Service
{
    public interface ICourseService : IService<Course>
    {
        IEnumerable<Course> GetLastedCourse();
    }
}
