using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarStack.Areas.Identity.Data;
using CarStack.Models;

namespace Data.Entities;

[Table("cars")]
public class CarEntity
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Model { get; set; }
    [Required]
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public double? Capacity { get; set; }
    public int? Power { get; set; }
    [MaxLength(50)]
    public string? Motor { get; set; }
    [Required]
    [MaxLength(50)]
    public string RegistrationNumber { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public int CarStackUserId { get; set; }
    [Required]
    public CarStackUser User { get; set; }

}



