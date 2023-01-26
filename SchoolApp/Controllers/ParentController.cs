using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class ParentController : Controller
    {
        private readonly MyDbContext _db;

        public ParentController(MyDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var parents = _db.Parents.Include(p => p.Children).ToList();
            return View(parents);
        }

        public IActionResult Details(int id)
        {
            var parent = _db.Parents.Include(p => p.Children).FirstOrDefault(p => p.Id == id);

            return View(parent);
        }

        public IActionResult Create()
        {
            Parent parent = new();
            //{
            //    Children = new List<Child>()
            //};

            //parent.Name = "Mansur";

            //add 2 child objects to the parent
            parent.Children.Add(new Child { Name = "Osman" });
            parent.Children.Add(new Child { Name = "Ahsen" });

            //return View(parent);

            return PartialView("_Create", parent);
        }

        [HttpPost]
        public IActionResult Create(Parent parent)
        {
           // if (ModelState.IsValid)
            //{
                _db.Add(parent);
                _db.SaveChanges();
                return RedirectToAction("Index");
            //}
           // return View(parent);
        }

        public IActionResult Edit(int id)
        {
            var parent = _db.Parents.FirstOrDefault(p => p.Id == id);

            //add all children to the parent
            parent.Children = _db.Children.Where(c => c.ParentId == id).ToList();

            return PartialView("_Edit", parent);
        }

        [HttpPost]
        public IActionResult Edit(Parent parent)
        {
            if (!ModelState.IsValid)
            {
                _db.Parents.Update(parent);

                foreach (var child in parent.Children)
                {
                    if (child.Id == 0)
                    {
                        _db.Children.Add(child);
                    }
                    else if (child.Id > 0)
                    {
                        _db.Children.Update(child);
                    }
                    else if (child.Id < 0)
                    {
                        _db.Children.Remove(child);
                    }
                }
                _db.SaveChanges();

                //return RedirectToAction("Index");
            }
            return View(parent);
        }
    }
}
