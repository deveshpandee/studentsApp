using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.DTO;

namespace MyApi.Services;

public class StudentService: IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Student> AddStudentAsync(CreateStudentDto createStudentDto)
    {
        Student student = new Student
        {
            Name = createStudentDto.Name,
            Age = createStudentDto.Age
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<bool> UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto)
    {
        Student? student = await _context.Students.FindAsync(id);
        if(student == null) return false;
        if(!string.IsNullOrWhiteSpace(updateStudentDto.Name))
            student.Name = updateStudentDto.Name;

        if(updateStudentDto.Age.HasValue)
            student.Age = updateStudentDto.Age.Value;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteStudentByIdAsync(int id)
    {
        Student? student = await _context.Students.FindAsync(id);
        if(student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<Student>> GetAllStudentsAsync()
    {

        return await _context.Students.ToListAsync();
    }
    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }
}