using Forum.bg.Data;
using Forum.bg.Data.Models;
using Forum.bg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.bg.Controllers
{
    /// <summary>
    /// CRUD Operations 
    /// </summary>
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="context"></param>
        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return view for all posts in DB
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All posts";
            var posts = await _context.Posts
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .ToListAsync();

            return View(posts);
        }

        /// <summary>
        /// Get view 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add() => View();

        /// <summary>
        /// Return view model and add post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get view post from DB for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Post post = await _context
                .Posts
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id && !p.IsDeleted) ?? new Post();

            if (post == null)
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

        /// <summary>
        /// Edit post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Post post = await _context
                .Posts
                .FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted) ?? new Post();

            if (post == null)
            {
                return BadRequest();
            }

            post.Title = model.Title;
            post.Content = model.Content;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Change the flag (IsDeleted) when the post is deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            Post post = (await _context
                .Posts
                .FirstOrDefaultAsync(p => p.Id == id))!;

            if (post == null)
            {
                return BadRequest();
            }

            post.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
