using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class HomeViewModel
    {
        public List<CourseItemViewModel> InteractiveCourses { get; set; }
        public List<CourseItemViewModel> LastedCourses { get; set; }
    }
}