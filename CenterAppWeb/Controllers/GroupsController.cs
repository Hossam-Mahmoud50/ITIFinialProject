using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenterApp.Core.Models;
using CenterApp.Infrasturcture.Data;

namespace CenterAppWeb.Controllers
{
    public class GroupsController : Controller
    {
        private readonly CenterDBContext _context;

        public GroupsController(CenterDBContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var centerDBContext = _context.Group.Include(x => x.Teacher);
            return View(await centerDBContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(m => m.Group_Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create(int? id)
        {
            if (id is not null)
                ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Name", id);
            else
                ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Name", @group.Teacher_Id);
            return View(@group);
        }
        // GET: Groups/Create
        public IActionResult CreateStudent(int? id)
        {
            var student = _context.Students.Include(x => x.Stage.Level).FirstOrDefault(x => x.Student_Id == id);
            ViewBag.Matrial = _context.LevelMatrial.Include(x => x.Matrial)
                .Where(x => x.Level_Id == student.Stage.Level_Id).Select(x => new
                {
                    Matrial_Id = x.Matrial_Id,
                    Matrial_Name = x.Matrial.Matrial_Name
                });
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(int id, StudentGroup studentGroup)
        {
            if (ModelState.IsValid)
            {
                studentGroup.Student_Id = id;
                _context.Add(studentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentGroup);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Name", @group.Teacher_Id);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Group @group)
        {
            if (id != @group.Group_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Group_Id))
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
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Name", @group.Teacher_Id);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(m => m.Group_Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Group == null)
            {
                return Problem("Entity set 'CenterDBContext.Group'  is null.");
            }
            var @group = await _context.Group.FindAsync(id);
            if (@group != null)
            {
                _context.Group.Remove(@group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return (_context.Group?.Any(e => e.Group_Id == id)).GetValueOrDefault();
        }
        public JsonResult GetTeachers(int Matrial_Id)
        {
            var data = _context.TeacherMatrial.Include(x => x.Teacher).Where(x => x.Matrial_Id == Matrial_Id).Select(x => new
            {
                Teacher_Id = x.Teacher_Id,
                Teacher_Name = x.Teacher.Teacher_Name
            }).ToList();
            return Json(data);
        }
        public JsonResult GetGroups(int teacher_Id)
        {
            var data = _context.Group.Where(x => x.Teacher_Id == teacher_Id).Select(x => new
            {
                Group_Id = x.Group_Id,
                Group_Name = x.Group_Name
            }).ToList();
            return Json(data);
        }
    }
}
