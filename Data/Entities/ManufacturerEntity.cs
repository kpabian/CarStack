using CarStack.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("manufacturers")]
public class ManufacturerEntity
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string City { get; set; }
    [MaxLength(10)]
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public List<CarEntity> Cars { get; set; }
}
