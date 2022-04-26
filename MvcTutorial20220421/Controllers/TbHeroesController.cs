#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTutorial20220421.Models;

namespace MvcTutorial20220421.Controllers
{
    public class TbHeroesController : Controller
    {
        private readonly DemoDBContext _context;

        public TbHeroesController(DemoDBContext context)
        {
            _context = context;
        }

        // GET: TbHeroes
        // Alt + Shift + .
        public IActionResult Index()
        {
            return View(_context.TbHeroes.ToList());
        }

        // GET: TbHeroes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbHero = _context.TbHeroes
                .FirstOrDefault(m => m.Id == id);
            if (tbHero == null)
            {
                return NotFound();
            }

            return View(tbHero);
        }

        // GET: TbHeroes/Create
        // CRUD
        // Create Read Update Delete
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbHeroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Atk,Hp,Skill")] TbHero tbHero)
        {
            if (ModelState.IsValid)
            {
                _context.TbHeroes.Add(tbHero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tbHero);
        }

        // GET: TbHeroes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Primary Key, PK, 主索引鍵
            var tbHero = _context.TbHeroes.Find(id);
            if (tbHero == null)
            {
                return NotFound();
            }
            return View(tbHero);
        }

        // POST: TbHeroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Atk,Hp,Skill")] TbHero tbHero)
        {
            if (id != tbHero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TbHeroes.Update(tbHero);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbHeroExists(tbHero.Id))
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
            return View(tbHero);
        }

        // GET: TbHeroes/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbHero = _context.TbHeroes
                .FirstOrDefault(m => m.Id == id);
            if (tbHero == null)
            {
                return NotFound();
            }

            return View(tbHero);
        }

        // POST: TbHeroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbHero = _context.TbHeroes.Find(id);
            _context.TbHeroes.Remove(tbHero);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbHeroExists(int id)
        {
            return _context.TbHeroes.Any(e => e.Id == id);
        }
    }
}
