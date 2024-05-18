using HerhalingEntityFramework.Database;
using HerhalingEntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace HerhalingEntityFramework.Controllers
{
    public class CommentController : Controller
    {
        public HerhalingEntityFrameworkContext context;
        private readonly ILogger<HomeController> _logger;
        public CommentController(ILogger<HomeController> logger, HerhalingEntityFrameworkContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public async Task<IActionResult> Index(ulong id)
        {
            var comment = await context.Comments.Where(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();

            return View(comment);
        }
        public async Task<IActionResult> Delete(ulong id)
        {
            var comment = await context.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
            var blogId = comment.BlogId;
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();

            return RedirectToAction("Show", "Blog", new { id = blogId });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ulong id, Comment model)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = model.Content;

            await context.SaveChangesAsync();

            //return to BlogController Show method
            return RedirectToAction("Show", "Blog", new { id = comment.BlogId });
        }

        [HttpGet]
        public async Task<IActionResult> Create(ulong id)
        {
            var blog = await context.Blogs.Where(b => b.Id == id).AsNoTracking().FirstOrDefaultAsync();

            var comment = new Comment { BlogId = blog.Id };

            return View(comment);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid == false)
            {
                return View(comment);
            }
            comment.Id = 0;
            comment.CreatedAt = DateTime.Now;

            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();

            //return to BlogController Show method
            return RedirectToAction("Show", "Blog", new { id = comment.BlogId });
        }
    }
}

