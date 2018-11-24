using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Service.Service
{
    public interface IOrderService : IService<Order>
    {
        void AddOrder(Course course, int userId, int payment);
        IEnumerable<Order> GetOrderByCourseAndUser(int courseId, int userId);
        IEnumerable<Order> GetByUser(int userId);

    }
}