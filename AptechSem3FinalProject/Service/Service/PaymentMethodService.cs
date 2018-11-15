using System.Collections.Generic;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class PaymentMethodService : Service<PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IUow uow, IRepository<PaymentMethod> repository) : base(uow, repository)
        {
        }
    }
}