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

        public IEnumerable<Course> GetByCreatedUser(int createdUser)
        {
            try
            {
                return GetAll(course => course.UserId == createdUser, courses => courses.OrderByDescending(course => course.Id));
            }
            catch (Exception ex)
            {
                return new List<Course>();
            }
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

        public bool ValidateCourseAccessible(int userId, int id)
        {
            var courseToBeCheck = GetById(id);
            if (courseToBeCheck.Price <=0 )
            {
                return true;
            }
            else
            {
                var userOrdered = courseToBeCheck.Orders;
                if (!userOrdered.Any() || userOrdered.Last().Payment.PaymentStatus != 1)
                {
                    return false;
                }
                 
            }

            return true;
        }
    }
}