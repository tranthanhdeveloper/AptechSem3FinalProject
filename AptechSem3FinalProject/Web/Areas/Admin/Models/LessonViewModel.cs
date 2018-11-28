using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin.Models
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
    public class EditLessonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsPreview { get; set; }
        public bool IsEnable { get; set; }

        public HttpPostedFileBase uploadVideo { get; set; }
    }
}