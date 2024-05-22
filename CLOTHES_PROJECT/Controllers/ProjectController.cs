using CLOTHES_PROJECT.Entities;
using CLOTHES_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLOTHES_PROJECT.Controllers
{
    public class ProjectController : Controller
    {
        Clothes_Context db = new Clothes_Context();
        public IActionResult Index()
        {
            List<Project> Pros = db.Projects.ToList();
            if(Pros.Count!=0) 
            {
                return View(Pros);
            }
            else
            {
                return View("EmptyProject");
            }
        }

        public IActionResult ProjectEmployees(int id)
        {
            var EmployeesWithHours = db.Projects.Where(x => x.PNUMBER == id).SelectMany(e => e.WORKS_ONs.Select(w => new { Employee = w.Employee, WorkingHours = w.Hours })).ToList();
            if (EmployeesWithHours.Count != 0) 
            {
                Project p = db.Projects.SingleOrDefault(x => x.PNUMBER == id);
                ViewData["ProjectEmps"] = EmployeesWithHours;
                return View(p);
            }
            else
            {
                return View("EmptyProjectEmployees");
            }
        }

        public IActionResult NewProject() 
        {
            List<Department> deptlst =db.Departments.ToList();
            if(deptlst.Count != 0)
            {
                ViewData["DEPTS"] = deptlst;
                return View();
            }
            else
            {
                return View("NoDepartmentYet");
            }

        }
        public IActionResult DetailsProject(int id) 
        {
            Project P = db.Projects.SingleOrDefault(x=>x.PNUMBER == id);

            if (P != null) 
            {
                Department d = db.Departments.SingleOrDefault(x => x.DNUMBER == P.DNUM);
                ViewData["Department"] = d;
                return View(P);
            }
            else 
            {
                return View("ErorrProject",P);
            }
        }

        public IActionResult EditProject(int id)
        {
            Project p = db.Projects.SingleOrDefault(x => x.PNUMBER == id);
            List<Department> deptlst = db.Departments.ToList();
            ViewData["DEPTS"] = deptlst;
            return View(p);

        }

        public IActionResult DeleteProject(int id) 
        {
            Project p = db.Projects.SingleOrDefault(x => x.PNUMBER == id);
            int EmployeesInProject = db.WORKS_ON.Where(x=>x.PNO == id).Select(w=>w.Employee).ToList().Count;
            if (EmployeesInProject != 0) 
            {
                return View("EmployeesInProjectError");
            }
            else
            {
                db.Projects.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public IActionResult SaveNewProject(Project p)
        {
            if (ModelState.IsValid == true)
            {
                db.Projects.Add(p);
                db.SaveChanges();
                return View();
            }
            else
            {
                List<Department> deptlst = db.Departments.ToList();
                ViewData["DEPTS"] = deptlst;

                return View("NewProject",p);
            }

        }

        public IActionResult SaveEditProject(Project New_P , [FromRoute] int id) 
        {
            Project Old_P = db.Projects.SingleOrDefault(x => x.PNUMBER == id);
            if (ModelState.IsValid == true) 
            {
                Old_P.Pname = New_P.Pname;
                Old_P.Plocation = New_P.Plocation;
                Old_P.DNUM = New_P.DNUM;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                List<Department> deptlst = db.Departments.ToList();
                ViewData["DEPTS"] = deptlst;

                return View("EditProject", Old_P);
            }
        }
    }
}
