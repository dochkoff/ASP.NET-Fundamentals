using System;
using T05_ASP.NETCoreIntroduction.Contracts;
using T05_ASP.NETCoreIntroduction.Models;

namespace T05_ASP.NETCoreIntroduction.Services
{
    public class StudentService : IStudentService
    {
        public StudentService()
        {
        }

        public Student GetStudent(int id)
        {
            return Database.GetStudent(id);
        }

        public bool UpdateStudent(Student student)
        {
            return Database.UpdateStudent(student);
        }
    }
}

