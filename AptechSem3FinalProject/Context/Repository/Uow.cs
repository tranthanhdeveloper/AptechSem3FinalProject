using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{
    public class Uow<V> : IDisposable where V : class 
    {
        private AptechSem3FinalProjectEntities _context = new AptechSem3FinalProjectEntities();
        private Repository<V> _repository;
        private bool _disposed = false;

        public Repository<V> Repository
            => this._repository ?? new Repository<V>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
        

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
