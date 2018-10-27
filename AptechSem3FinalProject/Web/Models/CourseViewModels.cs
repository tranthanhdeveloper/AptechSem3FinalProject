using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context.Database;

namespace Web.Models
{
    public class CourseListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
    }

    public class CourseDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual List<Lecture> Lectures { get; set; }
    }

    public class CoursePlayViewModel
    {

    }
}