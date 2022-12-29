using CQRS.Models;

namespace CQRS.DataAccess
{
    public interface IDataAccess
    {
        List<Student> GetStudents();
        Student AddStudent(string firstName, string lastName, double age);
        Student GetStudentById(int id);
    }
}
