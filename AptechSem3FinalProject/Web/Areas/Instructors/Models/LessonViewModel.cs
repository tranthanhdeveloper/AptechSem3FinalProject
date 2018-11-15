using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Instructors.Models
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageMain { get; set; }
        public string Path { get; set; }
        public int Status { get; set; }
        public int LectureId { get; set; }
        public int CourseId { get; set; }
        public byte IsPreview { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TimeLimit { get; set; }
        public byte IsEnable { get; set; }
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
        public int ModuleId { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsPreview { get; set; }
        public bool IsEnable { get; set; }
    }
}