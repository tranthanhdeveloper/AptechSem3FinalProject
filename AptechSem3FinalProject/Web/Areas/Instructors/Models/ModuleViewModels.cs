using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Instructors.Models
{
    public class ModuleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public List<LessonViewModel> LessonViewModels { get; set; }
    }

    public class CreateModuleViewModule
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(100)]
        public string  Title { get; set; }
    }

    public class EditModuleViewModule
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public bool IsEnable { get; set; }
    }
}