using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;

namespace Context.Database
{
    public partial class Category
    {
        public IEnumerable<Course> GetCourses()
        {
            return this.Courses.Where(c => c.Status != (int)CourseStatus.DELETED);
        }
    }
}
