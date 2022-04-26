using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.Models.Enums;

namespace BLL.Models;

public class Genre
{
    [Key]
    public GenreEnum Id { get; set; }
    
    public string Name { get; set; }
}