using symphony_limited.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace symphony_limited.Models
{
    public class symphony_limited_Model
    {
        public List<Course> ListCourse { get; set; }
        public List<Academy> ListAcademy { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Contact> ListContact { get; set; }
        public List<Result> ListResult { get; set; }
        public List<Student> ListStudent { get; set; }
        public List<Teacher> ListTeacher { get; set; }
        public List<Login> ListLogin { get; set; }

        public Student Student { get; set; }
    }
}