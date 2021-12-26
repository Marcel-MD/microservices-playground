using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos;

public class CreatePlatformDto
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; }
}