using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Instructor.Models
{

    public class AuthorDashboardViewModel
    {
        public AuthorSummaryInfoViewModel AuthorSummaryInfoViewModel { get; set; }
        public List<AuthorCoursesListItemViewModel> AuthorCoursesViewModels { get; set; }

    }
    public class AuthorSummaryInfoViewModel
    {
        public int TotalCourses { get; set; }
        public int TotalUserEnrolled { get; set; }
        public int MyProperty { get; set; }
    }
    public class AuthorCoursesListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreateDate { get; set; }

        public string Comment { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public byte Status { get; set; }
        public int CategoryId { get; set; }
    }
    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Course title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide course description")]
        public string Description { get; set; }
        public string Comment { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Stock amount must be at least 1")]
        public long Price { get; set; }
        public int CategoryId { get; set; }
        public string Prerequisites { get; set; }
    }
    public class EditCourseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Course title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide course description")]
        public string Description { get; set; }
        public string Comment { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Stock amount must be at least 1")]
        public long Price { get; set; }
        public int CategoryId { get; set; }
        public string Prerequisites { get; set; }
        public bool Status { get; set; }


    }
    public class CreateCourseModuleViewModel
    {
        [Required(ErrorMessage = "Module title is required")]
        public string Title { get; set; }
        public List<CreateModuleLessonViewModel> CreateModuleLessonViewModels  { get; set; }

    }
    public class CreateModuleLessonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lesson title is required")]
        public string Title { get; set; }
    }
    public class EditCourseModuleViewModel
    {
        [Required(ErrorMessage = "Module title is required")]
        public string Title { get; set; }
        public List<EditModuleLessonViewModel> CreateModuleLessonViewModels { get; set; }

    }
    public class EditModuleLessonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lesson title is required")]
        public string Title { get; set; }
    }
    public class UserEnrollmentViewModel{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreateDate { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public byte Status { get; set; }
        public int CategoryId { get; set; }
        public int TotalEnroll { get; set; }
        //public CourseComment CourseCommentList { get; set; } this feature will comming as soon as
    }

    public class CourseSearchViewModel
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }

    public class CourseSearchResultViewModel
    {
        public List<AuthorCoursesListItemViewModel> AuthorCoursesViewModels { get; set; }
    }
}