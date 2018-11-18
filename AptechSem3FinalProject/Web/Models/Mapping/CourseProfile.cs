using AutoMapper;
using Context.Database;

namespace Web.Models.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseItemViewModel>().ForMember(course => course.Author, destination => destination.MapFrom(c => c.User));
            CreateMap<CourseOutlineViewModel, Lecture>();
            CreateMap<CourseModuleViewModel, Lecture>();
            CreateMap<CourseLessonViewModel, Video>();
            CreateMap<ApplyAuthorViewModel, User>();
        }
    }
}