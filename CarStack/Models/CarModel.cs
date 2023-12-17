using CarStack.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CarStack.Models;

public class Car
{
    public int Id { get; set; }
    [Required]
    public string Model { get; set; }
    public int ManufacturerId { get; set; }
    [Display(Name ="Producent")]
    public virtual Manufacturer Manufacturer { get; set; }
    [Required]
    [Display(Name = "Pojemnoœæ")]
    public double? Capacity { get; set; }
    [Required]
    [Display(Name = "Moc")]
    public int? Power { get; set; }
    [Required]
    [Display(Name = "Silnik")]
    public string? Motor { get; set; }
    [Required]
    [Display(Name = "Numer rejestracyjny")]
    public string RegistrationNumber { get; set; }
    [Display(Name = "Priorytet")]
    [Required]
    public Priority Priority { get; set; }
    public int UserId { get; set; }
    [Display(Name = "W³aœciciel")]
    public virtual CarStackUser User { get; set; }
}

public enum Priority
{
    [Display(Name = "Niski")] Low = 1,
    [Display(Name = "Normalny")] Normal = 2,
    [Display(Name = "Wysoki")] High = 3,
    [Display(Name = "Pilny")] Urgent = 4
}
