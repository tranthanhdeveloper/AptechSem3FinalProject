using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Context.Repository;

namespace Service.Service
{
    public class Service<U> : IService<U> where U : class 
    {
        private IUow _uow;
        private IRepository<U> _repository;

        public virtual string NameForFinding
        {
            get { return string.Empty; }
        }

        public Service(IUow uow, IRepository<U> repository)
        {
            this._uow = uow;
            this._repository = repository;
        }
        public void Insert(U u)
        {
            _repository.Insert(u);
        }

        public U Add(U u)
        {
            return _repository.Add(u);
        }

        public void Update(U u)
        {
            _repository.Update(u);
        }

        public void Delete(object id)
        {
            _repository.Delete(id);
        }

        public void Delete(U u)
        {
            _repository.Delete(u);
        }

        public U GetById(object id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<U> GetAll(Expression<Func<U, bool>> filter = null, Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null, string includeProperties = "")
        {
            return _repository.GetAll(filter, orderBy, includeProperties);
        }

        public void Save()
        {
            _uow.Save();
        }
    }
}
