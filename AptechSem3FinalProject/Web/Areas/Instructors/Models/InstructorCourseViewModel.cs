using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Instructors.Models
{
    public class CourseItemViewModel
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
        //public List<ModuleViewModel> ModuleViewModels { get; set; }
        //public CreateLessonViewModel CreateLessonViewModel { get; set; }
    }

}