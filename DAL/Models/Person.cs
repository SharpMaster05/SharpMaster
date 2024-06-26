﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

[Table("People")]
public class Person
{
    [Column("PersonId")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Resume { get; set; }
    public int? BuildId { get; set; }
    public string ImagePath { get; set; }

}
