using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctAPITry.Models.ModelClasses
{
    public class Student
    {
        public int id { get; set; }
        [Required]
        public Nullable<int> roll { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0,500)]
        public Nullable<int> TotalMarks { get; set; }
    }
}