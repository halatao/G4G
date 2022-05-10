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
    public class ContentsController : ControllerBase
    {
        private readonly G4GContext _context;

        public ContentsController(G4GContext context)
        {
            _context = context;
        }

        // GET: api/Contents?subcategoryIdSubcategory=f
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentDto>>> GetContent(int subcategoryIdSubcategory)
        {
            if (subcategoryIdSubcategory == 0)
            {
                return await _context.Content.Include(cm => cm.Comment).Select(cn => new ContentDto
                {
                    Account = _context.Account.Select(ac => new AccountDto
                    {
                        IdAccount = ac.IdAccount,
                        Username = ac.Username,
                        CommentsPosted = ac.Comment.Select(cn => new CommentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            ContentIdContent = cn.ContentIdContent,
                            IdComment = cn.IdComment,
                            Posted = cn.Posted,
                            Text = cn.Text
                        }).Where(cm => cm.AccountUsername == ac.Username).Count(),
                        ContentsPosted = ac.Content.Select(cn => new ContentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            Headline = cn.Headline,
                            IdContent = cn.IdContent,
                            Posted = cn.Posted,
                            SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                            Text = cn.Text,
                            Views = cn.Views
                        }).Where(ct => ct.AccountUsername == ac.Username).Count()
                    }).Where(ac => ac.Username == cn.AccountUsername).First(),
                    Headline = cn.Headline,
                    IdContent = cn.IdContent,
                    Posted = cn.Posted,
                    SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                    Text = cn.Text,
                    Views = cn.Views,
                    Comment = (ICollection<CommentDto>)cn.Comment.Select(cn => new CommentDto
                    {
                        Account = _context.Account.Select(ac => new AccountDto
                        {
                            IdAccount = ac.IdAccount,
                            Username = ac.Username,
                            CommentsPosted = ac.Comment.Select(cn => new CommentDto
                            {
                                AccountIdAccount = cn.AccountIdAccount,
                                AccountUsername = cn.AccountUsername,
                                ContentIdContent = cn.ContentIdContent,
                                IdComment = cn.IdComment,
                                Posted = cn.Posted,
                                Text = cn.Text
                            }).Where(cm => cm.AccountUsername == ac.Username).Count(),
                            ContentsPosted = ac.Content.Select(cn => new ContentDto
                            {
                                AccountIdAccount = cn.AccountIdAccount,
                                AccountUsername = cn.AccountUsername,
                                Headline = cn.Headline,
                                IdContent = cn.IdContent,
                                Posted = cn.Posted,
                                SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                                Text = cn.Text,
                                Views = cn.Views
                            }).Where(ct => ct.AccountUsername == ac.Username).Count()
                        }).Where(ac => ac.Username == cn.AccountUsername).First(),
                        AccountIdAccount = cn.AccountIdAccount,
                        AccountUsername = cn.AccountUsername,
                        ContentIdContent = cn.ContentIdContent,
                        IdComment = cn.IdComment,
                        Posted = cn.Posted,
                        Text = cn.Text
                    }),
                    CommentsCount = cn.Comment.Select(cn => new CommentDto
                    {
                        AccountIdAccount = cn.AccountIdAccount,
                        AccountUsername = cn.AccountUsername,
                        ContentIdContent = cn.ContentIdContent,
                        IdComment = cn.IdComment,
                        Posted = cn.Posted,
                        Text = cn.Text
                    }).Count()

                }).OrderByDescending(ct=>ct.IdContent).ToListAsync();
            }
            return await _context.Content.Include(cm => cm.Comment).Select(cn => new ContentDto
            {
                Account = _context.Account.Select(ac=>new AccountDto
                {
                    IdAccount = ac.IdAccount,
                    Username = ac.Username,
                    CommentsPosted = ac.Comment.Select(cn => new CommentDto
                    {
                        AccountIdAccount = cn.AccountIdAccount,
                        AccountUsername = cn.AccountUsername,
                        ContentIdContent = cn.ContentIdContent,
                        IdComment = cn.IdComment,
                        Posted = cn.Posted,
                        Text = cn.Text
                    }).Where(cm => cm.AccountUsername == ac.Username).Count(),
                    ContentsPosted = ac.Content.Select(cn => new ContentDto
                    {
                        AccountIdAccount = cn.AccountIdAccount,
                        AccountUsername = cn.AccountUsername,
                        Headline = cn.Headline,
                        IdContent = cn.IdContent,
                        Posted = cn.Posted,
                        SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                        Text = cn.Text,
                        Views = cn.Views
                    }).Where(ct => ct.AccountUsername == ac.Username).Count()
                }).Where(ac=>ac.Username == cn.AccountUsername).First(),
                Headline = cn.Headline,
                IdContent = cn.IdContent,
                Posted = cn.Posted,
                SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                Text = cn.Text,
                Views = cn.Views,
                Comment = (ICollection<CommentDto>)cn.Comment.Select(cn => new CommentDto
                {
                    Account = _context.Account.Select(ac => new AccountDto
                    {
                        IdAccount = ac.IdAccount,
                        Username = ac.Username,
                        CommentsPosted = ac.Comment.Select(cn => new CommentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            ContentIdContent = cn.ContentIdContent,
                            IdComment = cn.IdComment,
                            Posted = cn.Posted,
                            Text = cn.Text
                        }).Where(cm => cm.AccountUsername == ac.Username).Count(),
                        ContentsPosted = ac.Content.Select(cn => new ContentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            Headline = cn.Headline,
                            IdContent = cn.IdContent,
                            Posted = cn.Posted,
                            SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                            Text = cn.Text,
                            Views = cn.Views
                        }).Where(ct => ct.AccountUsername == ac.Username).Count()
                    }).Where(ac => ac.Username == cn.AccountUsername).First(),
                    AccountIdAccount = cn.AccountIdAccount,
                    AccountUsername = cn.AccountUsername,
                    ContentIdContent = cn.ContentIdContent,
                    IdComment = cn.IdComment,
                    Posted = cn.Posted,
                    Text = cn.Text
                }),
                CommentsCount = cn.Comment.Select(cn => new CommentDto
                {
                    AccountIdAccount = cn.AccountIdAccount,
                    AccountUsername = cn.AccountUsername,
                    ContentIdContent = cn.ContentIdContent,
                    IdComment = cn.IdComment,
                    Posted = cn.Posted,
                    Text = cn.Text
                }).Count()

            }).Where(cn => cn.SubcategoryIdSubcategory == subcategoryIdSubcategory).OrderByDescending(ct => ct.IdContent).ToListAsync();
        }
        // GET: api/Contents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentDto>> GetContentById(int id)
        {
            var content = _context.Content.Include(cm => cm.Comment).Select(cn => new ContentDto
            {
                Account = _context.Account.Select(ac => new AccountDto
                {
                    IdAccount = ac.IdAccount,
                    Username = ac.Username,
                    CommentsPosted = ac.Comment.Select(cn => new CommentDto
                    {
                        AccountIdAccount = cn.AccountIdAccount,
                        AccountUsername = cn.AccountUsername,
                        ContentIdContent = cn.ContentIdContent,
                        IdComment = cn.IdComment,
                        Posted = cn.Posted,
                        Text = cn.Text
                    }).Where(cm => cm.AccountUsername == ac.Username).Count(),
                    ContentsPosted = ac.Content.Select(cn => new ContentDto
                    {
                        AccountIdAccount = cn.AccountIdAccount,
                        AccountUsername = cn.AccountUsername,
                        Headline = cn.Headline,
                        IdContent = cn.IdContent,
                        Posted = cn.Posted,
                        SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                        Text = cn.Text,
                        Views = cn.Views
                    }).Where(ct => ct.AccountUsername == ac.Username).Count()
                }).Where(ac => ac.Username == cn.AccountUsername).First(),
                Headline = cn.Headline,
                IdContent = cn.IdContent,
                Posted = cn.Posted,
                SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                Text = cn.Text,
                Views = cn.Views,
                Comment = (ICollection<CommentDto>)cn.Comment.Select(cn => new CommentDto
                {
                    Account = _context.Account.Select(ac => new AccountDto
                    {
                        IdAccount = ac.IdAccount,
                        Username = ac.Username,
                        CommentsPosted = ac.Comment.Select(cn => new CommentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            ContentIdContent = cn.ContentIdContent,
                            IdComment = cn.IdComment,
                            Posted = cn.Posted,
                            Text = cn.Text
                        }).Where(cm => cm.AccountUsername == ac.Username).Count(),
                        ContentsPosted = ac.Content.Select(cn => new ContentDto
                        {
                            AccountIdAccount = cn.AccountIdAccount,
                            AccountUsername = cn.AccountUsername,
                            Headline = cn.Headline,
                            IdContent = cn.IdContent,
                            Posted = cn.Posted,
                            SubcategoryIdSubcategory = cn.SubcategoryIdSubcategory,
                            Text = cn.Text,
                            Views = cn.Views
                        }).Where(ct => ct.AccountUsername == ac.Username).Count()
                    }).Where(ac => ac.Username == cn.AccountUsername).First(),
                    AccountIdAccount = cn.AccountIdAccount,
                    AccountUsername = cn.AccountUsername,
                    ContentIdContent = cn.ContentIdContent,
                    IdComment = cn.IdComment,
                    Posted = cn.Posted,
                    Text = cn.Text
                }),
                CommentsCount = cn.Comment.Select(cn => new CommentDto
                {
                    AccountIdAccount = cn.AccountIdAccount,
                    AccountUsername = cn.AccountUsername,
                    ContentIdContent = cn.ContentIdContent,
                    IdComment = cn.IdComment,
                    Posted = cn.Posted,
                    Text = cn.Text
                }).Count()

            }).Where(ct=>ct.IdContent==id).First();

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }

        // PUT: api/Contents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(int id, Content content)
        {
            if (id != content.IdContent)
            {
                return BadRequest();
            }

            _context.Entry(content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
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

        // POST: api/Contents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Content>> PostContent(Content content)
        {
            _context.Content.Add(content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.IdContent }, content);
        }

        // DELETE: api/Contents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            _context.Content.Remove(content);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.IdContent == id);
        }
    }
}
