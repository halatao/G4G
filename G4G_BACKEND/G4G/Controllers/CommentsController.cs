#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using G4G.Data;
using G4G.Models;
using G4G.Api;

namespace G4G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly G4GContext _context;

        public CommentsController(G4GContext context)
        {
            _context = context;
        }

        // GET: api/Comments/contentIdContent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComment(int contentIdContent)
        {
            if(contentIdContent == 0)
            {
                return await _context.Comment.Select(cm => new CommentDto
                {
                    IdComment = cm.IdComment,
                    ContentIdContent = cm.ContentIdContent,
                    Text = cm.Text,
                    Posted = cm.Posted,
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
                    }).Where(ac => ac.Username == cm.AccountUsername).First()
                }).ToListAsync();
            }
            return await _context.Comment.Select(cm => new CommentDto
            {
                IdComment = cm.IdComment,
                ContentIdContent = cm.ContentIdContent,
                Text = cm.Text,
                Posted = cm.Posted,
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
                }).Where(ac => ac.Username == cm.AccountUsername).First()
            }).Where(cm=>cm.ContentIdContent==contentIdContent).ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.IdComment)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.IdComment }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.IdComment == id);
        }
    }
}
