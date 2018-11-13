using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context.Database;

namespace Web.Models
{
    public class CourseItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
    }

    public class ShowCoursesViewModel
    {
        public List<CourseItemViewModel> PopularCourses { get; set; }
        public List<CourseItemViewModel> LastedCourses { get; set; }
    }

    public class LoadMoreCourseViewModel
    {
        public List<CourseItemViewModel> CourseItemViewModels  { get; set; }
    }
    public class CourseDetailViewModel
    {
        public CourseItemViewModel CourseListItemViewModel { get; set; }
        public virtual User Author { get; set; }
        public virtual List<CourseOutlineViewModel> CourseOutline { get; set; }
        public List<CourseItemViewModel> RelatedCourses { get; set; }
        public bool IsPaid { get; set; }
    }

    public class CourseOutlineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CourseLessonViewModel
    {       
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageMain { get; set; }
        public string Path { get; set; }
        public int Status { get; set; }
        public byte IsPreview { get; set; }

    }
    public class CourseModuleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CourseLessonViewModel> CourseLessonViewModels { get; set; }
    }

    public class CoursePlayViewModel
    {
        public CourseItemViewModel CourseItemViewModel { get; set; }
        public CourseLessonViewModel CurrentLesson { get; set; }
        public List<CourseModuleViewModel> CourseModuleViewModels{ get; set; }
    }
}