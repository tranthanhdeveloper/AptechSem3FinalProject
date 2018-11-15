using AutoMapper;
using Context.Database;
using Web.Areas.Instructors.Models;

namespace Web.Areas.Instructor.Models
{
    public class CourseAuthorProfile : Profile
    {
        public CourseAuthorProfile()
        {
            CreateMap<Course, CourseItemViewModel>();
            CreateMap<Lecture, ModuleViewModel >();
            CreateMap<Video, LessonViewModel>();
            CreateMap<CreateCourseViewModel, Course>();
        }
    }
}