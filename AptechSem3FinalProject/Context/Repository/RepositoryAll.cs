using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context.Database;

namespace Context.Repository
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface IVideoRepository : IRepository<Video>
    {

    }

    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface IPaymentMethodRepository : IRepository<PaymentMethod>
    {

    }

    public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface IPaymentRepository : IRepository<Payment>
    {

    }

    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface IOrderRepository : IRepository<Order>
    {

    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface ILectureRepository : IRepository<Lecture>
    {

    }

    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        public LectureRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface ICourseRepository : IRepository<Course>
    {

    }

    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(IDbFactory context) : base(context)
        {

        }
    }

    public interface IAccountRepository : IRepository<Account>
    {

    }

    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory context) : base(context)
        {

        }
    }
}
