namespace CarStack.Models;

public sealed class CarViewModel
{
    public Car Car { get; set; } = new Car();

    public IEnumerable<Manufacturer> Manufacturers { get; set; } = [];

}
