using System;
using T05_ASP.NETCoreIntroduction.Models;

namespace T05_ASP.NETCoreIntroduction.Contracts
{
    public interface IStudentService
    {
        Student GetStudent(int Id);

        bool UpdateStudent(Student student);
    }
}

