using AutoMapper;
using Context.Database;

namespace Web.Models.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseListItemViewModel, Course>();
            CreateMap<CourseOutlineViewModel, Lecture>();
        }
    }
}