namespace DAL.Models;

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Resume { get; set; }
    public int? BuildId { get; set; }
    public string ImagePath { get; set; }

}
