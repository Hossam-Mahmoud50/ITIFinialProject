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

namespace CenterAppWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly CenterDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StudentsController(CenterDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        //new
        [HttpGet]
        public async Task<IActionResult> Index()
        { //dsdgfsdf
            StudentsSearchIndexVM studentsSearch = new StudentsSearchIndexVM();
            studentsSearch.Students = await _context.Students.Include(x => x.Stage).ToListAsync();
            return View(studentsSearch);
        }

        [HttpPost]

        public async Task<IActionResult> Index(StudentsSearchIndexVM studentsSearch)
        {
            studentsSearch.Students = await _context.Students.ToListAsync();

            if (!String.IsNullOrEmpty(studentsSearch.SearchByName))
                studentsSearch.Students = _context.Students.Where(x => x.Student_Name.ToLower()
                .Contains(studentsSearch.SearchByName.ToLower())).ToList();


            //if (!String.IsNullOrEmpty(studentsSearch.SearchById))
            //    studentsSearch.Students = _context.Students.Where(x => x.Student_Id.ToLower()
            //      .Contains(studentsSearch.SearchByName.ToLower())).ToList();

            return View(studentsSearch);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Stage)
                .FirstOrDefaultAsync(m => m.Student_Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            ViewData["Material_id"] = new SelectList(_context.Stages, "Material_Id", "Material_Name");
            return View(new StudentStageMaterialVM());
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentStageMaterialVM studentstagematerialvm)
        {

            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            ViewData["Material_id"] = new SelectList(_context.Stages, "Material_Id", "Material_Name");
            if (ModelState.IsValid)
            {
                string fileimage = String.Empty;
                if (studentstagematerialvm.File != null)
                {
                    string images = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    fileimage = Guid.NewGuid().ToString() + "_" + studentstagematerialvm.File.FileName;
                    string fullpathimage = Path.Combine(images, fileimage);
                    using (var stream = new FileStream(fullpathimage, FileMode.Create))
                    {
                        studentstagematerialvm.File.CopyTo(stream);
                    }

                    studentstagematerialvm.Student.Student_Image = fileimage;
                }
                _context.Students.Add(studentstagematerialvm.Student);

                await _context.SaveChangesAsync();
                return View(studentstagematerialvm);
            }

            foreach (var item in ModelState.Values)
            {
                foreach (var item2 in item.Errors)
                {
                    ModelState.AddModelError(string.Empty, item2.ErrorMessage);
                }
            }
            // ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", student.Stage_id);
            //ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            return View(studentstagematerialvm);

        }


        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", student.Stage_id);
            //  ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Student_Id,Student_Name,Student_Address,Student_BirthOfDate,Gender,Student_RegisterDate,Student_Email,Student_Image,Student_StdPhone,Student_ParentPhone,Stage_id")] Student student)
        {
            if (id != student.Student_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Student_Id))
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
            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", student.Stage_id);
            // ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            return View(student);
        }

        // GET: Students/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Students == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .Include(s => s.Stage)
        //        .FirstOrDefaultAsync(m => m.Student_Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Students == null)
            {
                return Problem("Entity set 'CenterDBContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                var StudentA = await _context.StudentAttends.Where(x => x.Student_Id == student.Student_Id).ToListAsync();
                if (StudentA != null && StudentA.Count > 0)
                {
                    foreach (var item in StudentA)
                        _context.StudentAttends.Remove(item);
                }
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Student_Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult payForSupject(int id)
        {
            var groups = _context.StudentGroup.Include(x => x.Group).Where(x => x.Student_Id == id).Select(x => new
            {
                GroupName = x.Group.Group_Name,
                GroupId = x.Group_Id
            }).ToList(); 
            var students = _context.Students.Where(x => x.Student_Id == id).ToList();
            ViewBag.Groups = groups;
            ViewBag.Students = students;
            return View();
        }
        [HttpPost]
        public IActionResult payForSupject(PaymentVM model)
        {

            var groups = _context.StudentGroup.Include(x => x.Group).Where(x => x.Student_Id == model.StudentId).Select(x => new
            {
                GroupName = x.Group.Group_Name,
                GroupId = x.Group_Id
            }).ToList();
            var students = _context.Students.Where(x => x.Student_Id == model.StudentId).ToList();
            ViewBag.Groups = groups;
            ViewBag.Students = students;
            _context.StudentPayments.Add(new StudentPayments
            {
                Group_Id = model.GroupId,
                Price = model.Price,
                Student_Id = model.StudentId,
                IsPaid = model.IsPaid,
                Date = model.Date
            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetPaymentDetails(int studentId)
        {
            ViewBag.Materials = _context.Matrials.ToList();
            List<PaymentStatus> statusModels = _context.StudentPayments
                .Where(s => s.Student_Id == studentId).Include(d => d.Group).Select(c => new PaymentStatus
                {
                    Id = c.Id,
                    IsPaid = c.IsPaid,
                    GroupName = c.Group.Group_Name,
                    Price = c.Price,
                    Date = c.Date.ToString("MM")
                }).ToList();

            return View(statusModels);
        }


        public IActionResult DeletePayment(int id)
        {
            int stdId = 0;
            var payment = _context.StudentPayments.FirstOrDefault(d => d.Id == id);
            stdId = payment.Student_Id;
            _context.StudentPayments.Remove(payment);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetPaymentDetails), new { studentId = stdId });
        }

        [HttpGet]
        public IActionResult EditPayment(int id)
        {
            var groups = _context.StudentGroup.Include(x => x.Group).Where(x => x.Student_Id == id).Select(x => new
            {
                GroupName = x.Group.Group_Name,
                GroupId = x.Group_Id
            }).ToList();
            ViewBag.Groups = groups;
            StudentPayments studentP = _context.StudentPayments.FirstOrDefault(d => d.Id == id);
            return View(studentP);

        }
        [HttpPost]
        public IActionResult EditPayment(int id, StudentPayments payment)
        {
            var groups = _context.StudentGroup.Include(x => x.Group).Where(x => x.Student_Id == id).Select(x => new
            {
                GroupName = x.Group.Group_Name,
                GroupId = x.Group_Id
            }).ToList();
            ViewBag.Groups = groups;


            StudentPayments studentP = _context.StudentPayments.FirstOrDefault(d => d.Id == payment.Id);
            studentP.Group_Id = payment.Group_Id;
            studentP.Price = payment.Price;
            studentP.IsPaid = payment.IsPaid;
            studentP.Date = payment.Date;
            studentP.Student_Id = id;

            _context.SaveChanges();
            return RedirectToAction(nameof(GetPaymentDetails), new { studentId = studentP.Student_Id });

        }


        //--------------------------Attendance-------------------------

        [HttpGet]
        public IActionResult studentAttend(int id)
        {
            AttendVM attend = new AttendVM();
            attend.oneStudent = _context.Students.FirstOrDefault(x => x.Student_Id == id);
            ViewBag.Stages = _context.Stages.ToList();


            return View(attend);
        }
        [HttpPost]
        public IActionResult studentAttend(int id, StudentAttend model)
        {
            var stages = _context.Stages.ToList();
            var students = _context.Students.ToList();
            ViewBag.Stages = stages;
            ViewBag.Students = students;
            _context.StudentAttends.Add(new StudentAttend
            {
                Stage_Id = model.Stage_Id,
                Student_Id = id,
                IsAttend = model.IsAttend,
                AttendDate = model.AttendDate
            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetAttendDetails(int Id)
        {
            ViewBag.Stages = _context.Stages.ToList();
            var attends = _context.StudentAttends
                .Where(s => s.Student_Id == Id).Include(d => d.Stage).Include(x => x.Student).ToList();
            return View(attends);
        }


        public IActionResult DeleteAttend(int id)
        {
            int std_Id = 0;
            var attend = _context.StudentAttends.FirstOrDefault(d => d.Attend_Id == id);
            std_Id = attend.Attend_Id;
            _context.StudentAttends.Remove(attend);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetAttendDetails), new { studentId = std_Id });
        }

        [HttpGet]
        public IActionResult EditAttend(int id)
        {
            var stages = _context.Stages.ToList();
            var students = _context.Students.ToList();
            ViewBag.Stages = stages;
            ViewBag.Students = students;
            StudentAttend studentAttend = _context.StudentAttends.FirstOrDefault(d => d.Attend_Id == id);
            return View(studentAttend);

        }
        [HttpPost]
        public IActionResult EditAttend(StudentAttend attend)
        {
            var stages = _context.Stages.ToList();
            var students = _context.Students.ToList();
            ViewBag.Stages = stages;
            ViewBag.Students = students;
            StudentAttend studentAttend = _context.StudentAttends.FirstOrDefault(d => d.Attend_Id == attend.Attend_Id);
            studentAttend.Stage_Id = attend.Stage_Id;
            studentAttend.IsAttend = attend.IsAttend;
            studentAttend.AttendDate = attend.AttendDate;

            _context.SaveChanges();
            return RedirectToAction(nameof(GetAttendDetails), new { student_Id = studentAttend.Student_Id });

        }
    }

}

