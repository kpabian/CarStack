namespace CarStack.Models;

public sealed class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public List<Car> Cars { get; set; } = [];
}
