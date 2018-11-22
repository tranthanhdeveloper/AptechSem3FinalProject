using Context.Database;
using System.Collections.Generic;

namespace Service.Service
{
    public interface ICommentService : IService<Comment>
    {
        IEnumerable<Comment> GetByCourse(int id);
    }
}