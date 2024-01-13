using CarStack.Models;
using CarStack.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarStack.Controllers;

[Authorize(Roles = "User,Admin")]
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
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View(new Manufacturer());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Manufacturer manufacturer)
    {
        if (!ModelState.IsValid)
            return View(manufacturer);

        await manufacturerService.Create(manufacturer);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var manufacturer = await manufacturerService.GetById(id);
        if (manufacturer is null) return NotFound();
        return View(manufacturer);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Manufacturer manufacturer)
    {
        if (!ModelState.IsValid)
            return View(manufacturer);

        await manufacturerService.Update(manufacturer);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await manufacturerService.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Read(int id)
    {
        var manufacturer = await manufacturerService.GetById(id);
        if (manufacturer is null) return NotFound();
        return View(manufacturer);
    }
}
