using System.ComponentModel.DataAnnotations;

namespace MyApi.DTO;

public class UpdateStudentDto
{
    [MinLength(2)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Name cannot contains only whitespace.")]
    public string? Name { get; set; }
    [Range(1,120)]
    public int? Age { get; set; }
}