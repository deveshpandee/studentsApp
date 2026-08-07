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
    public IActionResult GetAllStudents()
    {
        return Ok(_studentService.GetAllStudents());
    }

    [HttpPost]
    public IActionResult AddStudent([FromBody] CreateStudentDto dto)
    {
        Student student = _studentService.AddStudent(dto);

        return CreatedAtAction(nameof(GetStudentById), new {id = student.Id}, student);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        Student? student = _studentService.GetStudentById(id);

        if (student == null) return NotFound($"no student found with id: {id}");
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudentById(int id)
    {
        bool isDeleted = _studentService.DeleteStudentById(id);
        if(!isDeleted) 
            return NotFound($"Cannot find student with id {id}");
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateStudent([FromBody] UpdateStudentDto dto, int id)
    {
        bool isUpdated = _studentService.UpdateStudent(id, dto);
        if(!isUpdated) return NotFound();

        return NoContent();
    }
}