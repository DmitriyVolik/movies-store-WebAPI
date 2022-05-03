using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Director
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string FullName { get; set; }
}