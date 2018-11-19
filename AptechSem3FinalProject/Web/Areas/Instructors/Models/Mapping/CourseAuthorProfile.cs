using AutoMapper;
using Context.Database;
using Web.Areas.Instructors.Models;

namespace Web.Areas.Instructor.Models
{
    public class CourseAuthorProfile : Profile
    {
        public CourseAuthorProfile()
        {
            CreateMap<Course, CourseItemViewModel>().ForMember(source => source.ModuleItemViewModels, destination => destination.MapFrom(course => course.Lectures));
            CreateMap<Lecture, ModuleItemViewModel>().ForMember(source => source.LessonItemViewModels, destination => destination.MapFrom(module => module.Videos));
            CreateMap<Video, LessonItemViewModel>();
            CreateMap<CreateCourseViewModel, Course>();
            CreateMap<Course, EditCourseViewModel>();
        }
    }
}