using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Instructors.Models
{
    public class LessonItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageMain { get; set; }
        public string Path { get; set; }
        public byte Status { get; set; }
        public bool IsPreview { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TimeLimit { get; set; }
        public bool IsEnable { get; set; }
        public int CourseId { get; set; }
        public int LectureId { get; set; }
    }
    public class CreateLessonViewModel
    {
        [Required]
        public int ModuleId { get; set; }
        [Required]
        public string Title { get; set; }       
        public bool IsPreview { get; set; }
    }

    public class EditLessonViewModel
    {
        [Required]
        public string Title { get; set; }
        public bool IsPreview { get; set; }
        public bool IsEnable { get; set; }


    }
}