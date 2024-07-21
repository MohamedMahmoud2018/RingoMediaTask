using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Models;
using RingoMediaTask.Models.ViewModels;

namespace RingoMediaTask.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentController(AppDbContext  context)
        {
                _appDbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _appDbContext.Departments.ToListAsync();
            return View(departments);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_appDbContext.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Add(department);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_appDbContext.Departments, "DepartmentId", "DepartmentName", department.ParentDepartmentId);
            return View(department);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _appDbContext.Departments.FindAsync(id);
            if (department == null) return NotFound();

            ViewBag.Departments = new SelectList(_appDbContext.Departments, "DepartmentId", "DepartmentName", department.ParentDepartmentId);
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,DepartmentLogo,ParentDepartmentId")] Department department)
        {
            if (id != department.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(department);
                    await _appDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_appDbContext.Departments, "DepartmentId", "DepartmentName", department.ParentDepartmentId);
            return View(department);
        }


        public async Task<IActionResult> Hierarchy(int? id)
        {
            if (id == null) return NotFound();

            var department = await _appDbContext.Departments
                .Include(d => d.SubDepartments)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null) return NotFound();

            var parentDepartments = new List<Department>();
            var current = department;
            while (current.ParentDepartmentId != null)
            {
                if (current.ParentDepartmentId == current.DepartmentId) break;
                var parent = await _appDbContext.Departments.FindAsync(current.ParentDepartmentId);
                parentDepartments.Add(parent);
                current = parent;
            }

            var viewModel = new DepartmentHierarchyViewModel
            {
                Department = department,
                ParentDepartments = parentDepartments
            };

            return View(viewModel);
        }

        private bool DepartmentExists(int id)
        {
            return _appDbContext.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
