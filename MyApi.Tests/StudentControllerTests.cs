using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApi.Controllers;
using MyApi.DTO;
using MyApi.Services;

namespace MyApi.Tests;

public class StudentControllerTests
{
    [Fact]
    public async Task GetStudentById_WhenStudentExists_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        var student = new Student
        {
            Id = 2,
            Name = "Rohit",
            Age = 39
        };

        mockService
            .Setup(s => s.GetStudentByIdAsync(student.Id))
            .ReturnsAsync(student);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.GetStudentById(student.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<Student>(okResult.Value);

        Assert.Equal(student.Id, returnedStudent.Id);
        Assert.Equal(student.Name, returnedStudent.Name);
        Assert.Equal(student.Age, returnedStudent.Age);

        mockService.Verify(
            s => s.GetStudentByIdAsync(student.Id),
            Times.Once
        );
    }


    [Fact]
    public async Task GetStudentById_WhenStudentDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        mockService
            .Setup(s => s.GetStudentByIdAsync(99))
            .ReturnsAsync((Student?)null);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.GetStudentById(99);

        // Assert
        var notFoundResult =
            Assert.IsType<NotFoundObjectResult>(result);

        mockService.Verify(
            s => s.GetStudentByIdAsync(99),
            Times.Once
        );
    }


    [Fact]
    public async Task GetAllStudents_ReturnsOkWithStudents()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        var students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "Rohit",
                Age = 39
            },
            new Student
            {
                Id = 2,
                Name = "Sachin",
                Age = 51
            }
        };

        mockService
            .Setup(s => s.GetAllStudentsAsync())
            .ReturnsAsync(students);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.GetAllStudents();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudents =
            Assert.IsType<List<Student>>(okResult.Value);

        Assert.Equal(2, returnedStudents.Count);

        Assert.Equal("Rohit", returnedStudents[0].Name);
        Assert.Equal("Sachin", returnedStudents[1].Name);

        mockService.Verify(
            s => s.GetAllStudentsAsync(),
            Times.Once
        );
    }


    [Fact]
    public async Task AddStudent_WithValidFields_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        var dto = new CreateStudentDto
        {
            Name = "Rohit",
            Age = 39
        };

        var student = new Student
        {
            Id = 42,
            Name = "Rohit",
            Age = 39
        };

        mockService
            .Setup(s => s.AddStudentAsync(
                It.Is<CreateStudentDto>(d =>
                    d.Name == dto.Name &&
                    d.Age == dto.Age)))
            .ReturnsAsync(student);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.AddStudent(dto);

        // Assert
        var createdResult =
            Assert.IsType<CreatedAtActionResult>(result);

        Assert.Equal(
            StatusCodes.Status201Created,
            createdResult.StatusCode
        );

        Assert.Equal(
            nameof(StudentController.GetStudentById),
            createdResult.ActionName
        );

        Assert.Equal(
            student.Id,
            createdResult.RouteValues!["id"]
        );

        var returnedStudent =
            Assert.IsType<Student>(createdResult.Value);

        Assert.Equal(student.Id, returnedStudent.Id);
        Assert.Equal(student.Name, returnedStudent.Name);
        Assert.Equal(student.Age, returnedStudent.Age);

        mockService.Verify(
            s => s.AddStudentAsync(
                It.Is<CreateStudentDto>(d =>
                    d.Name == dto.Name &&
                    d.Age == dto.Age)),
            Times.Once
        );
    }


    [Fact]
    public async Task DeleteStudentById_WhenStudentExists_ReturnsNoContent()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        mockService
            .Setup(s => s.DeleteStudentByIdAsync(2))
            .ReturnsAsync(true);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.DeleteStudentById(2);

        // Assert
        Assert.IsType<NoContentResult>(result);

        mockService.Verify(
            s => s.DeleteStudentByIdAsync(2),
            Times.Once
        );
    }


    [Fact]
    public async Task DeleteStudentById_WhenStudentDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        mockService
            .Setup(s => s.DeleteStudentByIdAsync(999))
            .ReturnsAsync(false);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.DeleteStudentById(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        mockService.Verify(
            s => s.DeleteStudentByIdAsync(999),
            Times.Once
        );
    }


    [Fact]
    public async Task UpdateStudent_WhenUpdateSucceeds_ReturnsNoContent()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        var dto = new UpdateStudentDto
        {
            Name = "Sachin",
            Age = 51
        };

        mockService
            .Setup(s => s.UpdateStudentAsync(2, dto))
            .ReturnsAsync(true);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.UpdateStudent(dto, 2);

        // Assert
        Assert.IsType<NoContentResult>(result);

        mockService.Verify(
            s => s.UpdateStudentAsync(2, dto),
            Times.Once
        );
    }


    [Fact]
    public async Task UpdateStudent_WhenStudentDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IStudentService>();

        var dto = new UpdateStudentDto
        {
            Name = "Sachin",
            Age = 51
        };

        mockService
            .Setup(s => s.UpdateStudentAsync(999, dto))
            .ReturnsAsync(false);

        var controller = new StudentController(mockService.Object);

        // Act
        var result = await controller.UpdateStudent(dto, 999);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        mockService.Verify(
            s => s.UpdateStudentAsync(999, dto),
            Times.Once
        );
    }
}