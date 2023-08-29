using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models.CustomValidations;


public class Shirt
{
    //[Range(0, 1000)]
    public int Id { get; set; }
    public string? Color { get; set; }
    public string? Brand { get; set; }
    public string? Gender { get; set; }
    public double Price  { get; set; }
    [CorrectSizing]
    public int? Size { get; set; }
}
