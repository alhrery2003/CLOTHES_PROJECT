using CLOTHES_PROJECT.Entities;
using CLOTHES_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLOTHES_PROJECT.Controllers
{
    public class DependentController : Controller
    {
        Clothes_Context db = new Clothes_Context();
        public IActionResult Index()
        {
            List<Dependent> DEPLST = db.Dependents.ToList();
            if(DEPLST.Count != 0) 
            {
                return View(DEPLST);
            }
            else 
            {
                return View("EmptyDependents");
            }
        }

        public IActionResult DependentDetails(int id)
        {
            Dependent D = db.Dependents.SingleOrDefault(x => x.ESSN == id);
            if(D != null) 
            {
                Employee E = db.Employees.SingleOrDefault(x => x.SSN == D.ESSN);
                ViewData["DependentEmployee"] = E;
                return View(D);
            }
            else
            {
                return View("EmptyDependent");
            }

        }

        public IActionResult NewDependent() 
        {
            var DependentEmps = db.Employees.Where(x => x.Dependent == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
            if(DependentEmps.Count != 0)
            {
                ViewData["DEPEMPS"] = DependentEmps;
                return View();
            }
            else { return View("NoEmployeesLeft"); }

        }

        public IActionResult EditDependent(int Id) 
        {
            Dependent D = db.Dependents.SingleOrDefault(x => x.ESSN == Id);
            Employee E = db.Employees.SingleOrDefault(x => x.SSN == D.ESSN);
            ViewData["DependentEmployee"] = E;
            return View(D);
        }

        public IActionResult DeleteDependent(int Id)
        {
            Dependent d = db.Dependents.SingleOrDefault(x => x.ESSN == Id);
            db.Dependents.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult SaveNewDependent(Dependent d) 
        {
            if(ModelState.IsValid==true) 
            {
                db.Dependents.Add(d);
                db.SaveChanges();
                return View();
            }
            else 
            {
                var DependentEmps = db.Employees.Where(x => x.Dependent == null).Select(w => new { ID = w.SSN, Name = (w.Fname + " " + w.Lname) }).ToList();
                ViewData["DEPEMPS"] = DependentEmps;

                return View("NewDependent",d);
            }
        }
        public IActionResult SaveEditDependent(Dependent New_D,[FromRoute]int Id) 
        {
            Dependent Old_D = db.Dependents.SingleOrDefault(x => x.ESSN == Id);

            if (ModelState.IsValid == true)
            {
                db.Dependents.Remove(Old_D);
                db.Dependents.Add(New_D);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Employee E = db.Employees.SingleOrDefault(x => x.SSN == Id);
                ViewData["DependentEmployee"] = E;

                return View("EditDependent", New_D);
            }

        }
    }
}
