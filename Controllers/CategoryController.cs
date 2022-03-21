using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.Data;

namespace BulkyBook.Controllers;

public class CategoryController : Controller {

    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(){
        IEnumerable<Category> obj = _context.Categories;
        return View(obj);
    }

    [HttpGet]
    public IActionResult Create(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category item){
        if(ModelState.IsValid){
            _context.Categories.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id){

        if( id == null || id == 0){
            return NotFound();
        }

        var categoryEdit = await _context.Categories.FindAsync(id);
        if( categoryEdit == null){
            return NotFound();
        }
        return View(categoryEdit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category item){
        if(ModelState.IsValid){
            _context.Categories.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id){

        if( id == null || id == 0){
            return NotFound();
        }

        var categoryEdit = await _context.Categories.FindAsync(id);
        if( categoryEdit == null){
            return NotFound();
        }
        return View(categoryEdit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int? id){

        var categoryDelete = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(categoryDelete);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}