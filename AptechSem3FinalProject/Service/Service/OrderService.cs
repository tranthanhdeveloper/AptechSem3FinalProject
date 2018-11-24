using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;
using Context.Repository;

namespace Service.Service
{
    public class OrderService : Service<Order>, IOrderService
    {
        public OrderService(IUow uow, IRepository<Order> repository) : base(uow, repository)
        {
        }        

        public void AddOrder(Course course, int userId, int paymentId)
        {
            this.Insert(new Order { Course = course, CreatedDate = DateTime.Now, PaymentId = paymentId, UserId = userId });
        }

        public IEnumerable<Order> GetByUser(int userId)
        {
            return GetAll(o => o.UserId == userId);
        }

        public IEnumerable<Order> GetOrderByCourseAndUser(int courseId, int userId)
        {
            return GetAll(order => order.CourseId == courseId & order.UserId == userId);
        }
    }
}