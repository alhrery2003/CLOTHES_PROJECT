using CLOTHES_PROJECT.Entities;
using CLOTHES_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLOTHES_PROJECT.Controllers
{
    public class DepartmentController : Controller
    {
        Clothes_Context db = new Clothes_Context();
        public IActionResult Index()
        {
            List<Department> DptLst = db.Departments.ToList();
            if (DptLst.Count != 0) 
            {
                return View(DptLst);
            }
            else
            {
                return View("EmptyDepartment");
            }
        }

        public IActionResult DetailsDepartment(int id) 
        {
            Department D = db.Departments.FirstOrDefault(x => x.DNUMBER == id);
            Employee E = db.Employees.FirstOrDefault(x => x.SSN == D.MGRSSN);
            ViewData["Manager"] = E;
            if(E != null) 
            {
                return View(D);
            }
            else 
            {
                return View("DepartmentErorr",D);
            }
        }

        public IActionResult FirstNewDepartment() 
        {
            return View();
        }
        public IActionResult NewDepartment() 
        {
            var Managers = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
            ViewData["MANGS"]=Managers;
            return View();
        }

        public IActionResult EditDepartment(int id) 
        {
            Department D = db.Departments.FirstOrDefault(x => x.DNUMBER == id);
            var Managers = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
            ViewData["MANGS"] = Managers;
            return View(D);
        }

        public IActionResult DeleteDepartment(int id) 
        {
            Department d = db.Departments.FirstOrDefault(x => x.DNUMBER == id);
            int EmployeesInDepartment = db.Employees.Where(x => x.DNUM == id).ToList().Count;
            int ProjectsInDepartment = db.Projects.Where(x => x.DNUM == id).ToList().Count;
            if (EmployeesInDepartment != 0)
            {
                return View("EmployeesInDepartmentError");
            }
            else if (ProjectsInDepartment != 0)
            {
                return View("ProjectsInDepartmentError");
            }
            else
            {
                db.Departments.Remove(d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult SaveFirstNewDepartment(Department d)
        {
            if (ModelState.ContainsKey(nameof(Department.MGRSSN))) // i did this because the ModelState doesn't validate MGRSSN 
            {                                                      // if its value is NULL because its type is integer but in the
                                                                   // database MGRSSN does allow the value NULL , and because 
                                                                   // of that i had to exclude it from validation
                ModelState.Remove(nameof(Department.MGRSSN));
            }

            if (ModelState.IsValid == true)
            {
                db.Departments.Add(d);
                db.SaveChanges();
                return View("SaveNewDepartment");
            }
            else
            {

                return View("FirstNewDepartment", d);
            }
        }

        public IActionResult SaveNewDepartment(Department d) 
        {
            if (ModelState.ContainsKey(nameof(Department.MGRSSN))) // i did this because the ModelState doesn't validate MGRSSN 
            {                                                      // if its value is NULL because its type is integer but in the
                                                                   // database MGRSSN does allow the value NULL , and because 
                                                                   // of that i had to exclude it from validation
                ModelState.Remove(nameof(Department.MGRSSN));
            }

            if (ModelState.IsValid == true)
            {
                db.Departments.Add(d);
                db.SaveChanges();
                return View();
            }
            else 
            {
                var Managers = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
                ViewData["MANGS"] = Managers;

                return View("NewDepartment",d);
            }
        }
        public IActionResult SaveEditDepartment(Department New_d,[FromRoute]int id) 
        {
            Department Old_d = db.Departments.FirstOrDefault(x => x.DNUMBER == id);

            if (ModelState.ContainsKey(nameof(Department.MGRSSN))) // i did this because the ModelState doesn't validate MGRSSN 
            {                                                      // if its value is NULL because its type is integer but in the
                                                                   // database MGRSSN does allow the value NULL , and because 
                                                                   // of that i had to exclude it from validation
                ModelState.Remove(nameof(Department.MGRSSN));
            }

            if (ModelState.IsValid == true) 
            {
                Old_d.DName = New_d.DName;
                Old_d.MGRSSN = New_d.MGRSSN;
                Old_d.MGRStartDate = New_d.MGRStartDate;
                Old_d.DLocation = New_d.DLocation;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var Managers = db.Employees.Where(x => x.SUPERSSN == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
                ViewData["MANGS"] = Managers;

                return View("EditDepartment",Old_d);
            }
        }
    }
}
