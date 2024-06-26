using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public class Build
{
    [Column("BuildId")]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Adress { get; set; }
    public int? RegionId { get; set; }

}
