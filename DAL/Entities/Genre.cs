using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace DAL.Entities;

public class Genre
{
    public int Id { get; set; }
    
    [MaxLength(20)]
    public string Name { get; set; }
}