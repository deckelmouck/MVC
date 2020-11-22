using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Data
{
    public class RepositoryClass
    {
        public static List<Student> GetStudents()
        {
            return new List<Student>{ new Student() { StudentId = 1, Name = "Jai",RollNo="12" },
                 new Student() { StudentId = 2, Name = "James" ,RollNo="13"},
                 new Student() { StudentId = 3, Name = "John",RollNo="14" }
                 };
        }
        public static List<Faculty> GetFaculty()
        {
            return new List<Faculty> {
                new Faculty () {  FacultyId = 1, Name = "Joseph john"},
                new Faculty () {  FacultyId = 2, Name = "Sam"},
                new Faculty () {  FacultyId = 3, Name = "Stephen miller" },

                 };
        }
    }
}
