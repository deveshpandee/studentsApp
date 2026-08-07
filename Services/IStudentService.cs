using MyApi.DTO;

namespace MyApi.Services;

public interface IStudentService
{
    Task<Student> AddStudentAsync(CreateStudentDto student);
    Task<bool> DeleteStudentByIdAsync(int id);
    Task<bool> UpdateStudentAsync(int id, UpdateStudentDto student);
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);

}