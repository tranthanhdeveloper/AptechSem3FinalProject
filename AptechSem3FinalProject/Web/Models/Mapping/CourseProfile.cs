using AutoMapper;
using Context.Database;

namespace Web.Models.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseItemViewModel, Course>();
            CreateMap<CourseOutlineViewModel, Lecture>();
            CreateMap<CourseLessonViewModel, Video>();
        }
    }
}