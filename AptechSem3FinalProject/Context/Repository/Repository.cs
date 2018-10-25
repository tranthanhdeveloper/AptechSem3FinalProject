using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{
    public class Repository<U> where U : class
    {
        internal AptechSem3FinalProjectEntities _context;
        internal DbSet<U> _dbSet;

        public Repository(AptechSem3FinalProjectEntities context)
        {
            this._context = context;
            this._dbSet = context.Set<U>();
        }

        public virtual void Insert(U u)
        {
            _dbSet.Add(u);
        }

        public virtual U Add(U u)
        {
            _dbSet.Add(u);
            _context.SaveChanges();
            return u;
        }

        public virtual void Update(U u)
        {
            
            _context.Entry(u).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            U entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(U u)
        {
            if (_context.Entry(u).State == EntityState.Detached)
            {
                _dbSet.Attach(u);
            }
            _dbSet.Remove(u);
        }

        public virtual U GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<U> GetAll(
            Expression<Func<U, bool>> filter = null,
            Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<U> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
