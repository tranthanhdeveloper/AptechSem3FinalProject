using System;
using System.Collections.Generic;
using System.Linq;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class CommentServices : Service<Comment>, ICommentService
    {
        public CommentServices(IUow uow, IRepository<Comment> repository) : base(uow, repository)
        {
           
        }

        public IEnumerable<Comment> GetByCourse(int id)
        {
            return GetAll(c => c.CourseId == id);
        }
    }
}