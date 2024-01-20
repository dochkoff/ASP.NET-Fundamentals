using System;
namespace T05_ASP.NETCoreIntroduction.Models
{
    static class Database
    {
        private static List<Student> _students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "Pavel Dochkov",
                Email = "pavel@dochkov.com"
            },

            new Student
            {
                Id = 2,
                Name = "An Dochkova",
                Email = "an@dochkova.com"
            },
        };

        public static Student GetStudent(int id)
        {
            return _students
                .FirstOrDefault(s => s.Id == id);
        }

        public static bool UpdateStudent(Student student)
        {
            var existingStudent = _students
                .FirstOrDefault(s => s.Id == student.Id);
            bool result = false;

            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;

                result = true;
            }

            return result;
        }
    }
}

