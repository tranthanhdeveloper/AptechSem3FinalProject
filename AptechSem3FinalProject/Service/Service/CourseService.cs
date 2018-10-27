using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class CourseService : Service<Course>, ICourseService
    {
        public CourseService(IUow uow, IRepository<Course> repository) : base(uow, repository)
        {
        }

        public IEnumerable<Course> GetLastedCourse()
        {
            try
            {
                return GetAll();
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }
    }
}
