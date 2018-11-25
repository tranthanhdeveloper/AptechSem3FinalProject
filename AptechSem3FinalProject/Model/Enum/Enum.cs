using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public enum RoleEnum
    {
        User = 1,
        Author = 2,
        Admin = 3
    }

    public enum CourseStatus
    {
        CREATED = 1,
        DELETED = 2,
        PUBLISHED = 3
    }

    public enum PaymentType
    {
        FREE = 1
    }

    public enum EntityStatus
    {
        Visible = 0,
        Invisible = 1
    }
}
