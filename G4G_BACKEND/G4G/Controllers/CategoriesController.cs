#nullable disable
using G4G.Api;
using G4G.Data;
using G4G.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G4G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly G4GContext _context;

        public CategoriesController(G4GContext context)
        {
            _context = context;
        }
        public static int GetCount(List<ContentDto> contents)
        {
            int i = 0;
            foreach(ContentDto content in contents)
            {
                i+=content.Comment.Count();
            }
            return i;
        }
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategory()
        {

            return await _context.Category.Include(x => x.SubCategory)
                .Select(c => new CategoryDto
                {
                    IdCategory = c.IdCategory,
                    Name = c.Name,
                    SubCategory = (ICollection<SubCategoryDto>)c.SubCategory
                    .Select(sc => new SubCategoryDto
                    {
                        Icon = sc.Icon,
                        IdSubcategory = sc.IdSubcategory,
                        Name = sc.Name,
                        lastContentInSubCategory = _context.Content.Include(cm => cm.Comment).Select(cn => new ContentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            Headline = cn.Headline,
                            IdContent = cn.IdContent,
                            Posted = cn.Posted,
                            SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                            Text = cn.Text,
                            Views = cn.Views,
                            Comment = (ICollection<CommentDto>)cn.Comment.Select(cn => new CommentDto
                            {
                                AccountIdAccount = cn.AccountIdAccount,
                                AccountUsername = cn.AccountUsername,
                                ContentIdContent = cn.ContentIdContent,
                                IdComment = cn.IdComment,
                                Posted = cn.Posted,
                                Text = cn.Text
                            })
                        }).Where(cn => cn.SubcategoryIdSubcategory == sc.IdSubcategory).OrderBy(cn => cn.IdContent).Last(),
                        totalCommentInInSubCategory = GetCount(_context.Content.Include(cm => cm.Comment).Select(cn => new ContentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            Headline = cn.Headline,
                            IdContent = cn.IdContent,
                            Posted = cn.Posted,
                            SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                            Text = cn.Text,
                            Views = cn.Views,
                            Comment = (ICollection<CommentDto>)cn.Comment.Select(cn => new CommentDto
                            {
                                AccountIdAccount = cn.AccountIdAccount,
                                AccountUsername = cn.AccountUsername,
                                ContentIdContent = cn.ContentIdContent,
                                IdComment = cn.IdComment,
                                Posted = cn.Posted,
                                Text = cn.Text
                            })
                        }).Where(cn => cn.SubcategoryIdSubcategory == sc.IdSubcategory).ToList()),
                        totalContentsInSubCategory = _context.Content.Include(cm => cm.Comment).Select(cn => new ContentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            Headline = cn.Headline,
                            IdContent = cn.IdContent,
                            Posted = cn.Posted,
                            SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                            Text = cn.Text,
                            Views = cn.Views,
                            Comment = (ICollection<CommentDto>)cn.Comment.Select(cn => new CommentDto
                            {
                                AccountIdAccount = cn.AccountIdAccount,
                                AccountUsername = cn.AccountUsername,
                                ContentIdContent = cn.ContentIdContent,
                                IdComment = cn.IdComment,
                                Posted = cn.Posted,
                                Text = cn.Text
                            })

                        }).Where(cn => cn.SubcategoryIdSubcategory == sc.IdSubcategory).OrderBy(cn => cn.IdContent).Count()
                    })
                }).ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.IdCategory)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.IdCategory }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.IdCategory == id);
        }
    }
}
