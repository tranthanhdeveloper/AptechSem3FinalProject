using Context.Database;
using Context.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    class PayerService : Service<Payer>, IPayerService
    {
        public PayerService(IUow uow, IRepository<Payer> repository) : base(uow, repository)
        {
        }
    }
}
