using System.Collections.Generic;
using System.Linq;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class LectureService : Service<Lecture>, ILectureService
    {
        public LectureService(IUow uow, IRepository<Lecture> repository) : base(uow, repository)
        {
        }

        public Lecture Get(int id)
        {
            return GetAll(lecture => lecture.Id == id).First();
        }

        public IEnumerable<Lecture> GetByCourseId(int courseId)
        {
            return GetAll(lecture => lecture.CourseId == courseId,
                lectures => lectures.OrderByDescending(lecture => lecture.Id));
        }

        public bool IsEditable(int userId, int id)
        {
            return Get(id).Course.UserId == userId;
        }
    }
}