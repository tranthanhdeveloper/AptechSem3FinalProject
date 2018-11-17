using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Instructors.Models
{
    public class ModuleItemViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public byte Status { get; set; }
        public List<LessonItemViewModel> LessonItemViewModels { get; set; }
    }

    public class CreateModuleViewModel
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class EditModuleViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}