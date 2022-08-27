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
    public class ExamController : Controller
    {
        private readonly CenterDBContext _context;

        public ExamController(CenterDBContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> IndexExam()
        {

            //ExamGradeSearch examGradeSearch = new ExamGradeSearch();
            //examGradeSearch.Exam = _context.Exams.Include(x => x.Matrials).Include(x => x.Stage).Include(x => x.Group).ToList();
            ExamScheduleVM examScheduleVM = new ExamScheduleVM();
            examScheduleVM.Exams = _context.Exams.Include(x => x.Matrials).Include(x => x.Group).ToList();

            return View(examScheduleVM);

        }
        [HttpPost]
        public async Task<IActionResult> IndexExam(ExamScheduleVM examScheduleVM)
        {
            examScheduleVM.Exams = _context.Exams.Include(x => x.Matrials).Include(x => x.Group).ToList();


            if (!String.IsNullOrEmpty(examScheduleVM.SearchByExam))
                examScheduleVM.Exams = examScheduleVM.Exams.Where(x => x.Exam_Title.ToLower()
                    .Contains(examScheduleVM.SearchByExam.ToLower())).ToList();
            //if (!DateTime.Is not null(examScheduleVM.SearchByDate))
            //{ examScheduleVM.SearchByDate = DateTime.Parse(examScheduleVM.Exams
            //    .Where(x => x.Exam_Start_Date == examScheduleVM.SearchByDate).ToString()); }
            //{
            //examGradeSearch.Time = examGradeSearch.Time == examGradeSearch.SearchByDate.t;
            //}
            //if (!DateTime.Is not null(examScheduleVM.SearchByDate))
            //examScheduleVM.Exams = examScheduleVM.Exams.Where(x=>x.Exam_Start_Date.ToString()
            //  .Contains(examScheduleVM.SearchByDate.ToString())).ToList();
            if (!String.IsNullOrEmpty(examScheduleVM.SearchBySubject))
                examScheduleVM.Exams = examScheduleVM.Exams.Where(x => x.Matrials.Matrial_Name.ToLower()
                    .Contains(examScheduleVM.SearchBySubject.ToLower())).ToList();

            return View(examScheduleVM);
        }

        public async Task<IActionResult> Details(string Exam_Title)
        {
            if (Exam_Title == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Exam_Title == Exam_Title);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }
        public async Task<IActionResult> CreateExam()
        {
            ViewBag.Matrial_Name = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name");
            ViewBag.Stage_Name = new SelectList(_context.Stages.ToList(), "Stage_Id", "Stage_Name");
            ViewBag.Group_Name = new SelectList(_context.Group.ToList(), "Group_Id", "Group_Name");
            return View();
        }


        // POST: ExamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExam(Exam model)
        {
            ViewData["Matrial_Name"] = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name");
            ViewData["Stage_Name"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Group_Name"] = new SelectList(_context.Group, "Group_Id", "Group_Name");

            if (ModelState.IsValid)
            {
                _context.Exams.Add(model);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Exam Is Added SuccessFull  ";
                return RedirectToAction(nameof(IndexExam));

            }
            else
            {
                ViewBag.Message = "Error !........................ ";
                return View(model);
            }


        }

        // GET: ExamController/Edit/5
        public async Task<IActionResult> Edit(string Exam_Title)
        {
            ExamScheduleVM examScheduleVM = new ExamScheduleVM();
            examScheduleVM.Exam = await _context.Exams.FindAsync(Exam_Title);
            return View(examScheduleVM);
        }

        // POST: ExamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Exam_Title, ExamScheduleVM examScheduleVM)
        {
            if (Exam_Title != examScheduleVM.Exam.Exam_Title)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Exams.Update(examScheduleVM.Exam);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Exam Is updated SuccessFull .... ";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ExamExists(examScheduleVM.Exam.Exam_Title))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return View(examScheduleVM);
                }
                ViewBag.Message = "Error Please Add Exam updates Again";
            }
            return View(examScheduleVM);
        }
        private bool ExamExists(string Exam_Title)
        {
            return (_context.Exams?.Any(e => e.Exam_Title == Exam_Title)).GetValueOrDefault();
        }
        //// GET: ExamController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ExamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string Exam_Title)
        {
            ExamScheduleVM examScheduleVM = new ExamScheduleVM();
            if (examScheduleVM == null)
            {
                return Problem("ExamSchedule is null.");
            }
            var exam = await _context.Exams.FindAsync(Exam_Title);
            if (exam != null)
            {
                _context.Exams.Remove(examScheduleVM.Exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexExam));
        }
        [HttpGet]
        public async Task<IActionResult> IndexGrade()
        {

            //ExamGradeSearch examGradeSearch = new ExamGradeSearch();
            //examGradeSearch.Exam = _context.Exams.Include(x => x.Matrials).Include(x => x.Stage).Include(x => x.Group).ToList();
            ExamScheduleVM examScheduleVM = new ExamScheduleVM();
            examScheduleVM.Grades = await _context.Grade.ToListAsync();

            return View(examScheduleVM);

        }
        [HttpPost]
        public async Task<IActionResult> IndexGrade(ExamScheduleVM examScheduleVM)
        {
            examScheduleVM.Grades = await _context.Grade.ToListAsync();


            if (!String.IsNullOrEmpty(examScheduleVM.SearchByGrade_Name))
                examScheduleVM.Grades = examScheduleVM.Grades.Where(x => x.Grade_Name.ToLower()
                    .Contains(examScheduleVM.SearchByGrade_Name.ToLower())).ToList();
            if (examScheduleVM.SearchByGrade_Point is not null)
            {
                examScheduleVM.Grades = examScheduleVM.Grades.Where(x => x.Grade_Point == examScheduleVM.SearchByGrade_Point).ToList();
            }

            return View(examScheduleVM);
        }

        public async Task<IActionResult> DetailsGrade(int Grade_Id)
        {
            if (Grade_Id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Grade_Id == Grade_Id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }
        public async Task<IActionResult> CreateGrade()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGrade(Grade model)
        {
            if (ModelState.IsValid)
            {
                _context.Grade.Add(model);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Grade Is Added SuccessFull  ";
                 return RedirectToAction(nameof(IndexGrade));
            }
            else
            {
                ViewBag.Message = "Error !........................ ";
                return View(model);
            }

        }

        // GET: ExamController/Edit/5
        public async Task<IActionResult> EditGrade(int Grade_Id)
        {
            ExamScheduleVM examScheduleVM = new ExamScheduleVM();
            examScheduleVM.Grade = await _context.Grade.FindAsync(Grade_Id);
            return View(examScheduleVM);
        }

        // POST: ExamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGrade(int Grade_Id, ExamScheduleVM examScheduleVM)
        {
            if (Grade_Id != examScheduleVM.Grade.Grade_Id)
            {
                NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Grade.Update(examScheduleVM.Grade);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Grade Is updated SuccessFull .... ";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(examScheduleVM.Grade.Grade_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(examScheduleVM);
            }
            ViewBag.Message = "Error Please Add Grade updates Again";
            return View(examScheduleVM);
        }


        private bool GradeExists(int? Grade_Id)
        {
            return (_context.Grade?.Any(g => g.Grade_Id == Grade_Id)).GetValueOrDefault();
        }

        // POST: ExamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGrade(int Grade_Id)
        {
            ExamScheduleVM examScheduleVM = new ExamScheduleVM();
            if (examScheduleVM == null)
            {
                return Problem("GradeSchedule is null.");
            }
            var grade = await _context.Grade.FindAsync(Grade_Id);
            if (grade != null)
            {
                _context.Grade.Remove(examScheduleVM.Grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexGrade));
        }
    }
}
