using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{

    public interface IService
    {

    }
    public interface IService<U> : IService where U : class
    {
        void Insert(U u);
        U Add(U u);
        void Update(U u);
        void Delete(object id);
        void Delete(U u);
        U GetById(object id);
        IEnumerable<U> GetAll(Expression<Func<U, bool>> filter = null, Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null, string includeProperties = "");
        void Save();
    }
}
