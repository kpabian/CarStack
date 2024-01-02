using CarStack.Models;
using CarStack.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarStack.Controllers;

public sealed class CarController(
    ICarService carService,
    IManufacturerService manufacturerService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cars = await carService.GetAll();
        return View(cars);
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(CarViewModel model)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        return RedirectToAction("Index");
    }

    [HttpGet("/Car/Read/{id:int}")]
    public async Task<IActionResult> Read(int id)
    {
        return View();
    }
}
