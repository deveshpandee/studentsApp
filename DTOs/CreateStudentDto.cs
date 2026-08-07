using System.ComponentModel.DataAnnotations;

namespace MyApi.DTO;

public class CreateStudentDto
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;
    [Range(1, 120)]
    public int Age { get; set; }
}