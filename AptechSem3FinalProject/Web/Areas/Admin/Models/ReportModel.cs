﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Web.Areas.Admin.Models
{
    public class ReportModel
    {
        public int CountCourse { get; set; }
        public int CountOrder { get; set; }
        public DateTime CreatedDate { get; set; }

        public long TotalMoney { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

    }
}