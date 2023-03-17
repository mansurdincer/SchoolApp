using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class ParentController : Controller
    {
        private readonly MyDbContext _context;

        public ParentController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var parents = _context.Parents.Include(p => p.Children).ToList();
            return View(parents);
        }

        public IActionResult Details(int id)
        {
            var parent = _context.Parents.Include(p => p.Children).FirstOrDefault(p => p.Id == id);

            return PartialView("_Details", parent);
        }

        public IActionResult Create()
        {
            Parent parent = new();

            parent.Children.Add(new Child());
            parent.Children.Add(new Child());

            return PartialView("_Create", parent);
        }

        [HttpPost]
        public IActionResult Create(Parent parent)
        {
            //foreach (var child in parent.Children)
            //{
            //    child.Parent = parent;
            //}

            //if (ModelState.IsValid)
            //{
            _context.Add(parent);
            _context.SaveChanges();
            return RedirectToAction("Index");
            //}
            //return View(parent);
        }

        public IActionResult Edit(int id)
        {
            var parent = _context.Parents.FirstOrDefault(p => p.Id == id);
            parent.Children = _context.Children.Where(c => c.ParentId == id).ToList();

            return PartialView("_Edit", parent);
        }

        [HttpPost]
        public IActionResult Edit(Parent parent)
        {
            if (!ModelState.IsValid)
            {

                //delete children that are not in the parent anymore
                var children = _context.Children.Where(c => c.ParentId == parent.Id).ToList();
                foreach (var child in children)
                {
                    if (!parent.Children.Contains(child))
                    {
                        _context.Children.Remove(child);
                    }
                }


               _context.Parents.Update(parent);

                foreach (var child in parent.Children)
                {
                    if (child.Id == 0)
                    {
                        _context.Children.Add(child);
                    }
                    else if (child.Id > 0)
                    {
                        _context.Children.Update(child);
                    }
                }
                _context.SaveChanges();

            }
            return View(parent);
        }

        public IActionResult Delete(int id)
        {
            var parent = _context.Parents.FirstOrDefault(p => p.Id == id);
            parent.Children = _context.Children.Where(c => c.ParentId == id).ToList();
            return PartialView("_Delete", parent);
        }

        [HttpPost]
        public IActionResult Delete(Parent parent)
        {
            _context.Parents.Remove(parent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
