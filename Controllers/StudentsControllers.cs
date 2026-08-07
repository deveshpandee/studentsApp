using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTO;
using MyApi.Services;

namespace MyApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StudentController: ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        return Ok(await _studentService.GetAllStudentsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] CreateStudentDto dto)
    {
        Student student = await _studentService.AddStudentAsync(dto);

        return CreatedAtAction(nameof(GetStudentById), new {id = student.Id}, student);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        Student? student = await _studentService.GetStudentByIdAsync(id);

        if (student == null) return NotFound($"no student found with id: {id}");
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentById(int id)
    {
        bool isDeleted = await _studentService.DeleteStudentByIdAsync(id);
        if(!isDeleted) 
            return NotFound($"Cannot find student with id {id}");
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto dto, int id)
    {
        bool isUpdated = await _studentService.UpdateStudentAsync(id, dto);
        if(!isUpdated) return NotFound();

        return NoContent();
    }
}