using System;
using System.Collections.Generic;
using System.Linq;
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
                return GetAll(null, courses => courses.OrderByDescending(course => course.Id)).Take(5);
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }
    }
}