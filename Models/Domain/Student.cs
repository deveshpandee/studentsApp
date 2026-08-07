using System.ComponentModel.DataAnnotations;

public class Student
{
    public int Id { get; set; }
    
    public string Name{ get; set; } = String.Empty;
    public int Age { get; set; }
}