using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Genre
{
    public int Id { get; set; }
    
    [MaxLength(20)]
    public string Name { get; set; }
}