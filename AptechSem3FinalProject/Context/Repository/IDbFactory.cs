using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{

    public interface IDbFactory : IDisposable
    {
        AptechSem3FinalProjectEntities Init();
    }

}
