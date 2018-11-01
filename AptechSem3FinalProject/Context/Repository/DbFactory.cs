using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{
    public class DbFactory : IDbFactory, IDisposable
    {
        private AptechSem3FinalProjectEntities _context;

        public AptechSem3FinalProjectEntities Init()
        {
            return _context ?? (_context = new AptechSem3FinalProjectEntities());
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
