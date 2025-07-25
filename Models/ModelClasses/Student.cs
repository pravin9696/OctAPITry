using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctAPITry.Models.ModelClasses
{
    public class Student
    {
        public int id { get; set; }
        public Nullable<int> roll { get; set; }
        public string Name { get; set; }
        public Nullable<int> TotalMarks { get; set; }
    }
}