using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenterApp.Core.Models;
using CenterApp.Infrasturcture.Data;
using CenterAppWeb.ViewModel;
using System.IO;
using Newtonsoft.Json;

namespace CenterAppWeb.Controllers
{
    public class StuffController : Controller
    {
        private readonly CenterDBContext _context;
        private readonly IWebHostEnvironment _hosting;


        public StuffController (CenterDBContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        // GET: Stuff
        [HttpGet]
        public async Task<IActionResult> Index()

        {
            StuffSearchIndexVM stuffSearchIndexVM = new StuffSearchIndexVM();
            stuffSearchIndexVM.Stuff = await _context.Stuff.ToListAsync();
            return View(stuffSearchIndexVM);
        }
        [HttpPost]
        public async Task<IActionResult> Index(StuffSearchIndexVM stuffSearchIndexVM)
        {
            stuffSearchIndexVM.Stuff = await _context.Stuff.ToListAsync();
            if (stuffSearchIndexVM.SearchByID is not null)
            {
                stuffSearchIndexVM.Stuff = stuffSearchIndexVM.Stuff.Where(x => x.Stuff_Id == stuffSearchIndexVM.SearchByID).ToList();
            }
            if (!String.IsNullOrEmpty(stuffSearchIndexVM.SearchByName))
                stuffSearchIndexVM.Stuff = stuffSearchIndexVM.Stuff.Where(x => x.Stuff_Name.ToLower()
                .Contains(stuffSearchIndexVM.SearchByName.ToLower())).ToList();
            if (!String.IsNullOrEmpty(stuffSearchIndexVM.SearchByPhone))
                stuffSearchIndexVM.Stuff = stuffSearchIndexVM.Stuff.Where(x => x.Stuff_Phone.ToLower()
               .Contains(stuffSearchIndexVM.SearchByPhone.ToLower())).ToList();
            
            return View(stuffSearchIndexVM);
        }

        // GET: Stuff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stuff == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuff
                .FirstOrDefaultAsync(m => m.Stuff_Id == id);
            if (stuff == null)
            {
                return NotFound();
            }

            return View(stuff);
        }

        // GET: Stuff/Create
        public IActionResult Create()
        {
            return View(new StuffFileVM());
        }

        // POST: stuff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StuffFileVM stuffFileVM)
        {

            if (ModelState.IsValid)
            {

                string fileimage = String.Empty;
                if (stuffFileVM.file != null)
                {

                    string images = Path.Combine(_hosting.WebRootPath, "images");
                    fileimage = Guid.NewGuid().ToString() + "_" + stuffFileVM.file.FileName;
                    string fullpathimage = Path.Combine(images, fileimage);
                    //string extension = Path.GetExtension(fullpathimage);

                    using (var stream = new FileStream(fullpathimage, FileMode.Create))
                    {
                        stuffFileVM.file.CopyTo(stream);// save image in folder  images path
                    }

                    stuffFileVM.Stuff.Stuff_Image = fileimage;
                    _context.Stuff.Add(stuffFileVM.Stuff);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Stuff Is Added SuccessFull ...";
                }
                else
                {
                    ViewBag.Message = "Error !.................";
                    return View(stuffFileVM);
                }

            }
            foreach (var item in ModelState.Values)
            {
                foreach (var item2 in item.Errors)
                {
                    ModelState.AddModelError(string.Empty, item2.ErrorMessage);

                }

            }

            ViewBag.Message = "Stuff Is Added SuccessFull ...";
            return View(stuffFileVM);
        }

        public CenterDBContext Get_context()
        {
            return _context;
        }


        // GET: Stuff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Stuff stuff = new Stuff();
            stuff = await _context.Stuff.FindAsync(id);
            return View(stuff);
        }

        // POST: Stuff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Stuff stuff)
        {
            if (id != stuff.Stuff_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Stuff.Update(stuff);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Stuff Is updated SuccessFull .... ";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StuffExists(stuff.Stuff_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Message = "Error Please Add Stuff updates Again";
            return View(stuff);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stuff == null)
            {
                return Problem("Entity set 'CenterDBContext.Stuff'  is null.");
            }
            var stuff = await _context.Stuff.FindAsync(id);
            if (stuff != null)
            {
                _context.Stuff.Remove(stuff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StuffExists(int id)
        {
            return (_context.Stuff?.Any(e => e.Stuff_Id == id)).GetValueOrDefault();
        }

    }
}
