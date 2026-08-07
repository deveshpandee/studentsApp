using MyApi.DTO;

namespace MyApi.Services;

public interface IStudentService
{
    Student AddStudent(CreateStudentDto student);
    bool DeleteStudentById(int id);
    bool UpdateStudent(int id, UpdateStudentDto student);
    List<Student> GetAllStudents();
    Student? GetStudentById(int id);

}