using CLOTHES_PROJECT.Entities;
using CLOTHES_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CLOTHES_PROJECT.Controllers
{
    public class EmployeeController : Controller
    {
        Clothes_Context db = new Clothes_Context();

        public IActionResult Index()
        {
            List<Employee> EmpLst = db.Employees.ToList();
            if(EmpLst.Count !=0)
            {
                return View(EmpLst);
            }
            else 
            {
                return View("EmptyEmployee");
            }
        }

        public IActionResult DetailsEmployee(int id) 
        {
            Employee E = db.Employees.FirstOrDefault(x => x.SSN == id);
            Employee EE = db.Employees.FirstOrDefault(x => x.SSN == E.SUPERSSN);
            Department D = db.Departments.FirstOrDefault(x => x.DNUMBER == E.DNUM);

            ViewData["SUPERVISOR"] = EE;
            ViewData["DEPARTMENT"] = D;

            if (EE != null) 
            {
                return View(E);
            }
            else 
            {
                return View("ErrorEmployee",E);
            }
        }

        public IActionResult EmployeeProjects(int id)
        {
            var ProjectsWithHours = db.Employees.Where(x => x.SSN == id).SelectMany(e=>e.WORKS_ONs.Select(w => new {Project = w.Project , WorkingHours =w.Hours })).ToList();
            if(ProjectsWithHours.Count != 0)
            {
                Employee e = db.Employees.SingleOrDefault(e => e.SSN == id);
                ViewData["EmpProjects"] = ProjectsWithHours;
                return View(e);
            }
            else
            {
                return View("EmptyEmployeeProjects");
            }
        }
        public IActionResult NewEmployee () 
        {
            List<Department> DeptLst = db.Departments.ToList();
            if(DeptLst.Count != 0)
            {
                ViewData["DepartmentList"] = DeptLst;
                var Supervisors = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
                ViewData["SUPS"] = Supervisors;
                return View();
            }
            else
            {
                return View("NoEmployeesYet");
            }

        }

        public IActionResult EditEmployee(int id) 
        {
            Employee e = db.Employees.SingleOrDefault(x => x.SSN == id);
            List<Department> DeptLst = db.Departments.ToList();
            ViewData["DepartmentList"] = DeptLst;
            var Supervisors = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
            ViewData["SUPS"] = Supervisors;
            return View(e);
        }

        public IActionResult SaveEmployee(Employee e) 
        {
            if (ModelState.ContainsKey(nameof(Employee.SUPERSSN))) // i did this because the ModelState doesn't validate SUPERSSN 
            {                                                      // if its value is NULL because its type is integer but in the
                                                                   // database SUPERSSN does allow the value NULL , and because 
                                                                   // of that i had to exclude it from validation
                ModelState.Remove(nameof(Employee.SUPERSSN));
            }

            if (ModelState.IsValid == true) 
            {
                db.Employees.Add(e);
                db.SaveChanges();
                return View();
            }
            else 
            {
                List<Department> DeptLst = db.Departments.ToList();
                ViewData["DepartmentList"] = DeptLst;
                var Supervisors = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
                ViewData["SUPS"] = Supervisors;

                return View("NewEmployee",e);
            }
        }

        public IActionResult SaveEditEmployee(Employee New_E ,[FromRoute] int id) 
        {
            Employee Old_E = db.Employees.SingleOrDefault(x => x.SSN == id);

            if (ModelState.ContainsKey(nameof(Employee.SUPERSSN))) // i did this because the ModelState doesn't validate SUPERSSN 
            {                                                      // if its value is NULL because its type is integer but in the
                                                                   // database SUPERSSN does allow the value NULL , and because 
                                                                   // of that i had to exclude it from validation
                ModelState.Remove(nameof(Employee.SUPERSSN));
            }
            if (ModelState.IsValid == true) 
            {
                Old_E.Fname = New_E.Fname;
                Old_E.Lname = New_E.Lname;
                Old_E.SUPERSSN = New_E.SUPERSSN;
                Old_E.Address = New_E.Address;
                Old_E.Sex = New_E.Sex;
                Old_E.Salary = New_E.Salary;
                Old_E.Bdate = New_E.Bdate;
                Old_E.DNUM = New_E.DNUM;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                List<Department> DeptLst = db.Departments.ToList();
                ViewData["DepartmentList"] = DeptLst;
                var Supervisors = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
                ViewData["SUPS"] = Supervisors;

                return View("EditEmployee",Old_E);
            }
        }

        public ActionResult DeleteEmployee(int id)
        {
            Employee e = db.Employees.SingleOrDefault(x => x.SSN == id);
            var DependentOfEmployee = db.Dependents.SingleOrDefault(x => x.ESSN == id);
            int IsEMployeeSupervisor = db.Employees.Where(x => x.SUPERSSN == id).ToList().Count;
            int IsEmployeeManager = db.Departments.Where(x => x.MGRSSN == id).ToList().Count;
            int ProjectsOfEmployee = db.WORKS_ON.Where(x=>x.ESSN==id).Select(w=>w.Project).ToList().Count;
            if (DependentOfEmployee != null)
            {
                return View("DependentOfEmployeeNotEmpty");
            }
            else if (IsEmployeeManager != 0)
            {
                return View("IsEmployeeManagerNotEmpty");
            }
            else if(IsEMployeeSupervisor != 0)
            {
                return View("IsEMployeeSupervisorNotEmpty");
            }
            else if(ProjectsOfEmployee != 0)
            {
                return View("ProjectsOfEmployeeNotEmpty");
            }
            else
            {
                db.Employees.Remove(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
