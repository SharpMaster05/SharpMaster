using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTO;

public class BuildDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Adress { get; set; }
    public int? RegionId { get; set; }
}
