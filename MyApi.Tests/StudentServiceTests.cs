using Microsoft.EntityFrameworkCore;
using MyApi.Services;
using MyApi.DTO;

namespace MyApi.Tests;

public class StudentServiceTests: IClassFixture<StudentServiceFixture>
{    
    private readonly StudentServiceFixture _fixture;
    public StudentServiceTests(StudentServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetStudentByIdAsync_WhenStudentExists_ReturnsStudent()
    {
        await using var context = _fixture.CreateContext();
        var student = new Student
        {
            Name = "Rohit",
            Age = 39
        };
        context.Students.Add(student);
        await context.SaveChangesAsync();

        var service = new StudentService(context);

        var result = await service.GetStudentByIdAsync(student.Id);

        Assert.NotNull(result);
        Assert.Equal(student.Id, result.Id);
        Assert.Equal(student.Name, result.Name);
        Assert.Equal(student.Age, result.Age);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(100)]
    public async Task GetStudentByIdAsync_WhenStudentNotExists_ReturnsNull(int id)
    {
        await using var context = _fixture.CreateContext();

        var service = new StudentService(context);

        var result = await service.GetStudentByIdAsync(id);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllStudentsAsync_WhenStudentExist_ReturnListOfStudents()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        Student student2 = new()
        {
            Name = "Sachin",
            Age = 51
        };
        context.Students.Add(student1);
        context.Students.Add(student2);
        await context.SaveChangesAsync();

        var service = new StudentService(context);
        
        var result = await service.GetAllStudentsAsync();
        Assert.Equal(2, result.Count);
        Assert.Contains(result, s => s.Id == student1.Id && s.Name == student1.Name && s.Age == student1.Age);
        Assert.Contains(result, s => s.Id == student2.Id && s.Name == student2.Name && s.Age == student2.Age);
    }    
    [Fact]
    public async Task GetAllStudentsAsync_WhenNoStudentExist_ReturnEmptyList()
    {
        await using var context = _fixture.CreateContext();

        var service = new StudentService(context);
        
        var result = await service.GetAllStudentsAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task AddstudentAsync_WithAppropriateValue_ReturnStudent()
    {
        await using var context = _fixture.CreateContext();

        var service = new StudentService(context);

        CreateStudentDto student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        
        var student = await service.AddStudentAsync(student1);

        var result = context.Students;
        Assert.Contains(result, s => s.Name==student1.Name && s.Age == student1.Age && s.Id == student.Id); // Test DB Data
        Assert.Equal(student1.Name, student.Name); // Test API response 
        Assert.Equal(student1.Age, student.Age); // Test API response
    }

    [Fact]
    public async Task DeleteStudentById_WhenStudentExists_ReturnsTrue()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        var service = new StudentService(context);
        
        var result = await service.DeleteStudentByIdAsync(student1.Id);

        var student = context.Students;
        Assert.True(result);
        Assert.DoesNotContain(student, s => s.Id == student1.Id);
    }
        [Fact]
    public async Task DeleteStudentById_WhenStudentDoesNotExist_ReturnFalse()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        var service = new StudentService(context);
        
        var result = await service.DeleteStudentByIdAsync(999);

        var student = context.Students;
        Assert.False(result);
    }
    [Fact]
    public async Task UpdateStudentByIdAsync_WhenNameAndAgeProvided_ReturnTrue()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        UpdateStudentDto updateStudentDto = new()
        {
            Name = "Sachin",
            Age = 51
        };

        var service = new StudentService(context);

        var result = await service.UpdateStudentAsync(student1.Id, updateStudentDto);

        var student = context.Students;

        Assert.True(result);
        Assert.Contains(student, s => s.Id == student1.Id && s.Name == updateStudentDto.Name && s.Age == updateStudentDto.Age);
    }
    [Fact]
    public async Task UpdateStudentByIdAsync_WhenAgeProvided_ReturnTrue()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        UpdateStudentDto updateStudentDto = new()
        {
            Age = 51
        };

        var service = new StudentService(context);

        var result = await service.UpdateStudentAsync(student1.Id, updateStudentDto);

        var student = context.Students;

        Assert.True(result);
        Assert.Contains(student, s => s.Id == student1.Id && s.Name == student1.Name && s.Age == updateStudentDto.Age);
    }
    [Fact]
    public async Task UpdateStudentByIdAsync_WhenNameProvided_ReturnTrue()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        UpdateStudentDto updateStudentDto = new()
        {
            Name = "Sachin"
        };

        var service = new StudentService(context);

        var result = await service.UpdateStudentAsync(student1.Id, updateStudentDto);

        var student = context.Students;

        Assert.True(result);
        Assert.Contains(student, s => s.Id == student1.Id && s.Name == updateStudentDto.Name && s.Age == student1.Age);
    }
    [Fact]
    public async Task UpdateStudentByIdAsync_WithWrongId_ReturnFalse()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        UpdateStudentDto updateStudentDto = new()
        {
            Name = "Sachin"
        };

        var service = new StudentService(context);

        var result = await service.UpdateStudentAsync(999, updateStudentDto);

        var student = context.Students;

        Assert.False( result);
    }    
    [Fact]
    public async Task UpdateStudentByIdAsync_WithNullInNameAndAge_ReturnTrue()
    {
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        UpdateStudentDto updateStudentDto = new()
        {
            Name = null,
            Age = null
        };

        var service = new StudentService(context);

        var result = await service.UpdateStudentAsync(student1.Id, updateStudentDto);

        var student = context.Students;

        Assert.True(result);
        Assert.Contains(student, s => s.Id == student1.Id && s.Name == student1.Name && s.Age == student1.Age);
    }    
    [Theory]
    [InlineData("   ")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("\t")]
    public async Task UpdateStudentByIdAsync_WithWhitespaceInName_ReturnTrue(string? name)
    {    
        await using var context = _fixture.CreateContext();

        Student student1 = new()
        {
            Name = "Rohit",
            Age = 38
        };
        context.Students.Add(student1);
        await context.SaveChangesAsync();

        UpdateStudentDto updateStudentDto = new()
        {
            Name = name
        };

        var service = new StudentService(context);

        var result = await service.UpdateStudentAsync(student1.Id, updateStudentDto);

        var student = context.Students;

        Assert.True(result);
        Assert.Contains(student, s => s.Id == student1.Id && s.Name == student1.Name && s.Age == student1.Age);
    }
}