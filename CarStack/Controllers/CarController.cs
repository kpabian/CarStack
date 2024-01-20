using CarStack.Models;
using CarStack.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarStack.Controllers;

[Authorize(Roles = "Admin,User")]
public sealed class CarController(
    ICarService carService,
    IManufacturerService manufacturerService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var manufacturers = await manufacturerService.GetAll();
        var manufacturerIds = HttpContext.Request.Query
            .Select(item => int.TryParse(item.Key, out var parsedId) ? parsedId : -1)
            .Where(parsedId => parsedId != -1)
            .ToList();

        if (User.IsInRole("Admin"))
        {
            var cars = await carService.GetByFilter(
                car => manufacturerIds.Count == 0 || manufacturerIds.Contains(car.ManufacturerId)
            );
            return View((cars, manufacturers));
        }

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            return Unauthorized();

        var userCars = await carService.GetByFilter(
            manufacturerIds.Count == 0 
                ? car => car.UserId == userId 
                : car => car.UserId == userId && manufacturerIds.Contains(car.ManufacturerId)
        );

        return View((userCars, manufacturers));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var manufacturers = await manufacturerService.GetAll();
        if (manufacturers is null) return NotFound();
        return View(new CarViewModel { Manufacturers = manufacturers });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarViewModel model)
    {
        try
        {
            var newCar = model.Car;
            newCar.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            await carService.Create(newCar);
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            var manufacturers = await manufacturerService.GetAll();
            if (manufacturers == null) return NotFound();
            return View(new CarViewModel { Car = model.Car, Manufacturers = manufacturers });
        }
    }

    [HttpGet("/Car/Update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var car = await carService.GetById(id);
        var manufacturers = await manufacturerService.GetAll();
        if (car is null || manufacturers is null) return NotFound();
        return View(new CarViewModel { Car = car, Manufacturers = manufacturers });
    }

    [HttpPost]
    public async Task<IActionResult> Update(CarViewModel model)
    {
        try
        {
            var car = model.Car;
            car.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            await carService.Update(car);
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            var manufacturers = await manufacturerService.GetAll();
            if (manufacturers == null) return NotFound();
            return View(new CarViewModel { Car = model.Car, Manufacturers = manufacturers });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await carService.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet("/Car/Read/{id:int}")]
    public async Task<IActionResult> Read(int id)
    {
        var car = await carService.GetById(id);
        if (car is null) return NotFound();
        if (User.IsInRole("Admin"))
            return View(car);
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            return Unauthorized();
        if (car.UserId != userId)
            return Unauthorized();
        return View(car);
    }
}
