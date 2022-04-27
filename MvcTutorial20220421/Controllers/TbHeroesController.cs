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

        //GET
        public IActionResult Search()
        {
            ViewData["Message"] = "英雄搜尋 GET => 取得表單";
            return View(new HeroSearchViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search([Bind("Name,MinAtk,MaxAtk")] HeroSearchParams searchParams)
        {
            
            var viewModel = new HeroSearchViewModel(); // 清出記憶體空間 => 打掃房間
            if (ModelState.IsValid)
            {
                var searchResult =  _context.TbHeroes.ToList(); // 取出所有資料
                if (!string.IsNullOrEmpty(searchParams.Name)) // 如果有輸入名字，就用名字當條件搜尋
                {
                    searchResult = searchResult
                        .Where(x => x.Name == searchParams.Name)
                        .ToList();
                }

                // 如果攻擊力的範圍合邏輯，就用加攻擊力條件再多篩一次
                if (searchParams.MinAtk >= 0
                    && searchParams.MaxAtk > 0
                    && searchParams.MinAtk < searchParams.MaxAtk)
                {
                    // 最小 ATK = 10；最大 ATK = 20
                    // ==> 那些英雄攻擊歷介在 10~20 之間
                    searchResult = searchResult
                        .Where(x => x.Atk >= searchParams.MinAtk && x.Atk <= searchParams.MaxAtk)
                        .ToList();
                }
                else // 如果攻擊力的範圍不合邏輯，就顯示忽略攻擊力條件字樣
                {
                    ViewData["Message"] = $"（忽略攻擊力搜尋條件）";
                }

                // 用 ViewData 讓 Controller 與 View 共享資料
                ViewData["Message"] += $"搜尋到 {searchResult.Count} 個英雄";

                // 把搜尋條件與搜尋結果賦值到 ViewModel 的 property
                // MVC 才有辦法幫我們把資料打包進 Search.cshtml
                viewModel.SearchParams = searchParams;
                viewModel.Heroes = searchResult;
            }
            // 回傳 View + ViewModel 進行打包作業
            return View(viewModel);
        }

        private bool TbHeroExists(int id)
        {
            return _context.TbHeroes.Any(e => e.Id == id);
        }
    }
}
