using System.Collections.Generic;
using Context.Database;

namespace Service.Service
{
    public interface ILectureService : IService<Lecture>
    {
        IEnumerable<Lecture> GetByCourseId(int courseId);
    }
}