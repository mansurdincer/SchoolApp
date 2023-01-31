using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Data;
using System.Threading.Tasks;

namespace SchoolApp.ViewComponents
{    
    public class CommentsViewComponent : ViewComponent
    {
        private readonly MyDbContext _context;

        public CommentsViewComponent(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var comments = await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
            return View(comments);
        }
    }
}
