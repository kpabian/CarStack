using CarStack.Models;
using CarStack.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CarStack.Controllers;

public sealed class ManufacturerController(
    IManufacturerService manufacturerService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var manufacturers = await manufacturerService.GetAll();
        return View(manufacturers);
    }

    [HttpGet]
    public async Task<IActionResult> Read(int id)
    {
        var manufacturer = await manufacturerService.GetById(id);
        if (manufacturer is null) return NotFound();
        return View(manufacturer);
    }
}
