using System;
using System.Collections.Generic;
using System.Linq;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class PaymentService : Service<Context.Database.Payment>, IPaymentService
    {
        public PaymentService(IUow uow, IRepository<Context.Database.Payment> repository) : base(uow, repository)
        {
        }        
    }
}