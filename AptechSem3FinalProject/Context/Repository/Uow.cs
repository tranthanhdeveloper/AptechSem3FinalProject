using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{
    public class Uow : IUow
    {
        private AptechSem3FinalProjectEntities _context;
        private IDbFactory _dbFactory;

        public Uow(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public AptechSem3FinalProjectEntities Context
            => _context ?? (_context = _dbFactory.Init());
        public void Save()
        {
            Context.SaveChanges();
        }
       
    }
}
