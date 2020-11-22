using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;

namespace MVC.Models
{
    public class StudentFacultyViewModel
    {
        public List<Student> AllStudents { get; set; }
        public List<Faculty> AllFaculties { get; set; }
    }
}
