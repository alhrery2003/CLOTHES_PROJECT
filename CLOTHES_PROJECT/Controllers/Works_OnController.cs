using CLOTHES_PROJECT.Entities;
using CLOTHES_PROJECT.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace CLOTHES_PROJECT.Controllers
{
    public class Works_OnController : Controller
    {
        Clothes_Context db = new Clothes_Context();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DealWithEmployees() 
        {
            List<Employee> employees = db.Employees.ToList();
            if (employees.Count != 0)
            {
                return View();
            }
            else
            {
                return View("FreshEmployees");
            }
        }

        public IActionResult DealWithProjects() 
        {
            List<Project> projects = db.Projects.ToList();
            if (projects.Count != 0)
            {
                return View();
            }
            else
            {
                return View("FreshProjects");
            }

        }

        public IActionResult SearchEmployeeByID() 
        {
            return View();
        }

        public IActionResult SearchEmployeeByName()
        {
            return View();
        }

        public IActionResult SearchProjectByID()
        {
            return View();
        }

        public IActionResult SearchProjectByName()
        {
            return View();
        }

        public IActionResult FindEmployeeByID(Employee e)
        {
            if (e.SSN != 0)
            {
                Employee E1 = db.Employees.SingleOrDefault(x => x.SSN == e.SSN);

                if (E1 != null)
                {
                    var Test_w = db.WORKS_ON.Where(x => x.ESSN == E1.SSN).Select(p => p.Project).ToList();
                    if (Test_w.Count != 0)
                    {
                        var Prolst = db.Employees.Where(x => x.SSN == E1.SSN).SelectMany(e => e.WORKS_ONs.Select(w => new { Project = w.Project, WorkingHours = w.Hours })).ToList();
                        ViewData["PROS"] = Prolst;
                        ViewData["EMP"] = E1;
                        return View();
                    }
                    else
                    {
                        return View("EmptyEmpolyee",E1);
                    }

                }
                else
                {
                    return View("ErrorEmployeeIDSearch");
                }

            }
            else
            {
                return View("ErrorEmployeeIDSearch");
            }
        }

        public IActionResult FindEmployeeByName(Employee e)
        {
            if (e.Fname != null && e.Lname != null)
            {
                Employee E1 = db.Employees.SingleOrDefault(x => x.Fname == e.Fname && x.Lname == e.Lname);

                if (E1 != null)
                {
                    var Test_w = db.WORKS_ON.Where(x => x.ESSN == E1.SSN).Select(p => p.Project).ToList();
                    if (Test_w.Count != 0)
                    {
                        var Prolst = db.Employees.Where(x => x.SSN == E1.SSN).SelectMany(e => e.WORKS_ONs.Select(w => new { Project = w.Project, WorkingHours = w.Hours })).ToList();
                        ViewData["PROS"] = Prolst;
                        ViewData["EMP"] = E1;
                        return View();
                    }
                    else
                    {
                        return View("EmptyEmpolyee2",E1);
                    }

                }
                else
                {
                    return View("ErrorEmployeeNameSearch");
                }

            }
            else
            {
                return View("ErrorEmployeeNameSearch");
            }

        }


        public IActionResult AssignEmployeeToProject(int Id) 
        {
            Employee E = db.Employees.SingleOrDefault(x => x.SSN == Id);
            List<Project> Pros = db.Projects.ToList();
            if(Pros.Count != 0)
            {
                ViewData["PROS"] = Pros;
                ViewData["EMP"] = E;
                return View("FindEmpProRelation");
            }
            else
            {
                return View("NoProjectsYet");
            }

        }


        public ActionResult EditEmployeeProjects(WORKS_ON w) 
        {
            var W1 = db.WORKS_ON.FirstOrDefault(x => x.ESSN == w.ESSN && x.PNO == w.PNO);
            Employee E = db.Employees.SingleOrDefault(x => x.SSN == W1.ESSN);
            List<Project> prolst = db.Projects.ToList();
            ViewData["PROS"] = prolst;
            ViewData["EMP"] = E;
            return View(W1);

        }
        public IActionResult ProjectEmployees(Project p)
        {
            if (p.PNUMBER != 0) 
            {
                Project P1 = db.Projects.SingleOrDefault(x => x.PNUMBER == p.PNUMBER);
                if (P1 != null)
                {
                    var Test_W = db.WORKS_ON.Where(x => x.PNO == P1.PNUMBER).Select(w => w.Employee).ToList();
                    if (Test_W.Count != 0) 
                    {
                        var Emplst = db.Projects.Where(x => x.PNUMBER == P1.PNUMBER).SelectMany(e => e.WORKS_ONs.Select(w => new { Employee = w.Employee, WorkingHours = w.Hours })).ToList();
                        if(Emplst.Count != 0)
                        {
                            ViewData["EMPS"] = Emplst;
                            ViewData["PRO"] = P1;
                            return View();
                        }
                        else
                        {
                            return View("NoEmployeesYet");
                        }

                    }
                    else
                    {
                        return View("EmptyProject",P1);
                    }

                }
                else
                {
                    return View("ErrorProjectSearchID");
                }

            }
            else 
            {
                return View("ErrorProjectSearchID");
            }


        }

        public IActionResult ProjectEmployees2(Project p)
        {
            if (p.Pname != null)
            {
                Project P1 = db.Projects.SingleOrDefault(x => x.Pname == p.Pname);
                if (P1 != null)
                {
                    var Test_W = db.WORKS_ON.Where(x => x.PNO == P1.PNUMBER).Select(w => w.Employee).ToList();
                    if (Test_W.Count != 0)
                    {
                        var Emplst = db.Projects.Where(x => x.PNUMBER == P1.PNUMBER).SelectMany(e => e.WORKS_ONs.Select(w => new { Employee = w.Employee, WorkingHours = w.Hours })).ToList();
                        if (Emplst.Count != 0)
                        {
                            ViewData["EMPS"] = Emplst;
                            ViewData["PRO"] = P1;
                            return View("ProjectEmployees");
                        }
                        else
                        {
                            return View("NoEmployeesYet");
                        }
                    }
                    else
                    {
                        return View("EmptyProject", P1);
                    }

                }
                else
                {
                    return View("ErrorProjectSearchName");
                }

            }
            else
            {
                return View("ErrorProjectSearchName");
            }


        }

        [HttpPost]
        public IActionResult UnAssignProjectFromEmployee(WORKS_ON w) 
        {
            Project p = db.Projects.SingleOrDefault(x => x.PNUMBER == w.PNO);
            db.WORKS_ON.Remove(w);
            db.SaveChanges();
            return RedirectToAction("ProjectEmployees",p);
        }


        [HttpPost]
        public IActionResult UnAssignEmployeeFromProject(WORKS_ON w)
        {
            Employee e = db.Employees.SingleOrDefault(x => x.SSN == w.ESSN);
            db.WORKS_ON.Remove(w);
            db.SaveChanges();
            return RedirectToAction("FindEmployeeByID", e);
        }


        [HttpPost]
        public IActionResult SaveNewAssignment(WORKS_ON w) 
        {
            var W1 = db.WORKS_ON.FirstOrDefault(x => x.ESSN == w.ESSN && x.PNO == w.PNO);
            Employee e = db.Employees.SingleOrDefault(x => x.SSN == w.ESSN);

            if (W1 == null)
            {
                if(ModelState.IsValid == true)
                {
                    db.WORKS_ON.Add(w);
                    db.SaveChanges();
                    return RedirectToAction("FindEmployeeById", e);
                }
                else
                {
                    Employee E = db.Employees.SingleOrDefault(x => x.SSN == w.ESSN);
                    List<Project> Pros = db.Projects.ToList();
                    ViewData["PROS"] = Pros;
                    ViewData["EMP"] = E;

                    return View("FindEmpProRelation", w);
                }

            }
            else
            {
                ViewData["PRO"] = db.Projects.SingleOrDefault(x => x.PNUMBER == w.PNO);
                return View("ErrorEmployeeSave",e);
            }
        }



        [HttpPost]
        public IActionResult SaveEditAssignment(WORKS_ON New_W, [FromRoute] int id)
        {
            Employee e = db.Employees.SingleOrDefault(x => x.SSN == New_W.ESSN);
            WORKS_ON Old_W = db.WORKS_ON.FirstOrDefault(x => x.ESSN == New_W.ESSN && x.PNO == id);
            WORKS_ON Exisisting_W = db.WORKS_ON.FirstOrDefault(x => x.ESSN == New_W.ESSN && x.PNO == New_W.PNO);
            if(Exisisting_W != null) 
            {
                if (Old_W.PNO == Exisisting_W.PNO)
                {
                    if(ModelState.IsValid == true)
                    {
                        db.WORKS_ON.Remove(Old_W);
                        db.WORKS_ON.Add(New_W);
                        db.SaveChanges();
                        return RedirectToAction("FindEmployeeById", e);
                    }
                    else
                    {
                        Employee E = db.Employees.SingleOrDefault(x => x.SSN == Old_W.ESSN);
                        List<Project> prolst = db.Projects.ToList();
                        ViewData["PROS"] = prolst;
                        ViewData["EMP"] = E;

                        return View("EditEmployeeProjects", New_W);
                    }

                }
                else
                {
                    ViewData["PRO"] = db.Projects.SingleOrDefault(x => x.PNUMBER == New_W.PNO);
                    return View("ErrorEmployeeSave", e);
                }
            }
            else
            {
                db.WORKS_ON.Remove(Old_W);
                db.WORKS_ON.Add(New_W);
                db.SaveChanges();
                return RedirectToAction("FindEmployeeById", e);
            }
        }


    }
}
