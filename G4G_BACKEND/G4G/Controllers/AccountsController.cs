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
    public class AccountsController : ControllerBase
    {
        private readonly G4GContext _context;

        public AccountsController(G4GContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccount()
        {
            return await _context.Account.Include(ac => ac.Content).Select(ac => new AccountDto
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
            }).ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{name}")]
        public async Task<ActionResult<AccountDto>> GetAccount(string name)
        {
            var account = await _context.Account.Include(ac=>ac.Content).Select(ac=> new AccountDto
            {
                IdAccount=ac.IdAccount,
                Username=ac.Username,
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
            }).Where(ac=>ac.Username==name).ToListAsync();

            if (account == null)
            {
                return NotFound();
            }

            return account.FirstOrDefault();
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.IdAccount)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Account.Any(ac => ac.Username == account.Username)){
                return BadRequest();
            }
            else
            {
                _context.Account.Add(account);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAccount", new { id = account.IdAccount }, account);
            }
            
            
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.IdAccount == id);
        }
    }
}
