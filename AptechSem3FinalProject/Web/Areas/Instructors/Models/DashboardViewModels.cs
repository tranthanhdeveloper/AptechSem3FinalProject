using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Instructors.Models
{
    public class DashboardViewModels
    {
        public AuthorSummaryInfoViewModel AuthorSummaryInfoViewModel { get; set; }
        public List<CourseItemViewModel> AuthorCoursesViewModels { get; set; }
    }

    public class AuthorSummaryInfoViewModel
    {
        public int TotalUserEnrolled { get; set; }
        public int TotalCourses { get; set; }

    }
}