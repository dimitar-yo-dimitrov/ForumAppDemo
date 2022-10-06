using Forum.bg.Data;
using Forum.bg.Data.Models;
using Forum.bg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.bg.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All posts";
            var posts = await _context.Posts
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .ToListAsync();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Post post = new()
            {
                Title = model.Title,
                Content = model.Content
            };

            await this._context.Posts.AddAsync(post);
            await this._context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Post post = await this._context
                .Posts
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id && !p.IsDeleted) ?? new Post();

            if (post.Title == null)
            {
                return BadRequest();
            }

            return View(new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Post post = await this._context
                .Posts
                .FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted) ?? new Post();

            if (post.Title == null)
            {
                return BadRequest();
            }

            post.Title = model.Title;
            post.Content = model.Content;

            await this._context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Post post = (await this._context
                .Posts
                .FirstOrDefaultAsync(p => p.Id == id))!;

            if (post == null)
            {
                return BadRequest();
            }

            post.IsDeleted = true;

            await this._context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
