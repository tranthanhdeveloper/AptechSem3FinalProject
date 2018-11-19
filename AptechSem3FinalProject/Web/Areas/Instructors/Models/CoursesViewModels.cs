using Context.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Instructors.Models
{
    public class CourseItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public byte Status { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Prerequisites { get; set; }
        public List<ModuleItemViewModel> ModuleItemViewModels { get; set; }
    }

    public class CreateCourseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public string Comment { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public long Price { get; set; }
        public byte Status { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Prerequisites { get; set; }

    }
    public class EditCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public string Comment { get; set; }

        [Required]
        public long Price { get; set; }
        public byte Status { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Prerequisites { get; set; }
    }

    public class CourseSearchViewModel
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }

}