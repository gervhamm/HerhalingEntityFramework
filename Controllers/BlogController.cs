using HerhalingEntityFramework.Database;
using HerhalingEntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HerhalingEntityFramework.Controllers
{
    public class BlogController : Controller
    {
        public HerhalingEntityFrameworkContext context;
        private readonly ILogger<HomeController> _logger;
        public BlogController(ILogger<HomeController> logger, HerhalingEntityFrameworkContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var blogs = context.Blogs.AsNoTracking().ToList();

            return View(blogs);
        }

        [HttpGet]
        public IActionResult Create() => View(new Blog());

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid == false)
            {
                return View(blog);
            }
            blog.CreatedAt = DateTime.Now;
            blog.UpdatedAt = DateTime.Now;

            await context.Blogs.AddAsync(blog);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Show(ulong id)
        {
            var blog = await context.Blogs
                .Where(b => b.Id == id)
                .Include(b => b.Comments.OrderBy(c => c.CreatedAt))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return View(blog);
        }
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await context.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            var blog = await context.Blogs.Include(b => b.Comments).FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            context.Comments.RemoveRange(blog.Comments);
            context.Blogs.Remove(blog);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(ulong id)
        {
            return context.Blogs.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Update(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await context.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost, ActionName("Update")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ulong id, Blog model)
        {
            var blog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Title = model.Title;
            blog.Author = model.Author;
            blog.UpdatedAt = DateTime.Now;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
