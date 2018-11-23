using System;
using System.Collections.Generic;
using System.Linq;
using Context.Database;
using Context.Repository;
using Model.Enum;

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
                return GetAll(course => course.UserId == createdUser && course.Status != (byte)CourseStatus.DELETED, courses => courses.OrderByDescending(course => course.Id));
            }
            catch (Exception)
            {
                return new List<Course>();
            }
        }

        public IEnumerable<Course> GetLastedCourse()
        {
            try
            {
                return GetAll(courses => courses.Status == (byte)CourseStatus.PUBLISHED, courses => courses.OrderByDescending(course => course.Id)).Take(4);
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public IEnumerable<Course> GetPublished()
        {
            return GetAll(courses => courses.Status == (byte)CourseStatus.PUBLISHED);
        }

        public bool ValidateCourseAccessible(int userId, int id)
        {
            var courseToBeCheck = GetById(id);
            if (courseToBeCheck.Price <= 0 )
            {
                return true;
            }
            else
            {
                var userOrdered = courseToBeCheck.Orders.Where(c => c.CourseId == id && c.UserId == userId);
                if (userOrdered.Count() == 0 || userOrdered.First().Payment.PaymentStatus != 1)
                {
                    return false;
                }                 
            }
            return true;
        }

        public bool ValidateCourseDeletable(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCourseEditable(int userId, int id)
        {
            try
            {
                return GetById(id).UserId == userId;
            }
            catch
            {
                return false;
            }
        }
    }
}