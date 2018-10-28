using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context.Database;

namespace Web.Models
{
    public class CourseListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
    }

    public class ShowCoursesViewModel
    {
        public List<CourseListItemViewModel> PopularCourses { get; set; }
        public List<CourseListItemViewModel> LastedCourses { get; set; }
    }

    public class CourseDetailViewModel
    {
        public  CourseListItemViewModel CourseListItemViewModel { get; set; }
        public virtual User Author { get; set; }
        public virtual List<CourseOutlineViewModel> CourseOutline { get; set; }
    }

    public class CourseOutlineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CoursePlayViewModel
    {

    }
}