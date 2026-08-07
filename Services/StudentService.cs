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
    public Student AddStudent(CreateStudentDto createStudentDto)
    {
        Student student = new Student
        {
            Name = createStudentDto.Name,
            Age = createStudentDto.Age
        };
        _context.Students.Add(student);
        _context.SaveChanges();
        return student;
    }

    public bool UpdateStudent(int id, UpdateStudentDto updateStudentDto)
    {
        Student? student = _context.Students.FirstOrDefault(s => s.Id == id);
        if(student == null) return false;
        if(!string.IsNullOrWhiteSpace(updateStudentDto.Name))
            student.Name = updateStudentDto.Name;

        if(updateStudentDto.Age.HasValue)
            student.Age = updateStudentDto.Age.Value;
        _context.SaveChanges();
        return true;
    }
    public bool DeleteStudentById(int id)
    {
        Student? student = _context.Students.FirstOrDefault(s => s.Id == id);
        if(student == null) return false;

        _context.Students.Remove(student);
        _context.SaveChanges();
        return true;
    }
    public List<Student> GetAllStudents()
    {

        return _context.Students.ToList();
    }
    public Student? GetStudentById(int id)
    {
        return _context.Students.FirstOrDefault(s => s.Id == id);
    }
}