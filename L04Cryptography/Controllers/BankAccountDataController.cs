using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L04Cryptography.Data;
using L04Cryptography.Models;
using Microsoft.AspNetCore.DataProtection;

namespace L04Cryptography.Controllers
{
    public class BankAccountDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataProtector _protector;
        private readonly IDataProtector _protector2;

        public BankAccountDataController(ApplicationDbContext context, IDataProtectionProvider provider)
        {

            _context = context;
            _protector = provider.CreateProtector("BankAccounts");
            _protector2 = provider.CreateProtector("BankAccounts");

        }

        // GET: BankAccountData
        public async Task<IActionResult> Index()
        {
              return _context.BankAccountData != null ? 
                          View(await _context.BankAccountData.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BankAccountData'  is null.");
        }

        // GET: BankAccountData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BankAccountData == null)
            {
                return NotFound();
            }

            var bankAccountData = await _context.BankAccountData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccountData == null)
            {
                return NotFound();
            }

            return View(bankAccountData);
        }

        // GET: BankAccountData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccountData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number")] BankAccountData bankAccountData)
        {
            if (ModelState.IsValid)
            {
                bankAccountData.EncryptedNumber = _protector.Protect(bankAccountData.Number);
                bankAccountData.DecryptedNumber = _protector2.Unprotect(bankAccountData.EncryptedNumber); 
                _context.Add(bankAccountData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccountData);
        }

        // GET: BankAccountData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BankAccountData == null)
            {
                return NotFound();
            }

            var bankAccountData = await _context.BankAccountData.FindAsync(id);
            if (bankAccountData == null)
            {
                return NotFound();
            }
            return View(bankAccountData);
        }

        // POST: BankAccountData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,EncryptedNumber,DecryptedNumber")] BankAccountData bankAccountData)
        {
            if (id != bankAccountData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccountData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountDataExists(bankAccountData.Id))
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
            return View(bankAccountData);
        }

        // GET: BankAccountData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BankAccountData == null)
            {
                return NotFound();
            }

            var bankAccountData = await _context.BankAccountData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccountData == null)
            {
                return NotFound();
            }

            return View(bankAccountData);
        }

        // POST: BankAccountData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BankAccountData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BankAccountData'  is null.");
            }
            var bankAccountData = await _context.BankAccountData.FindAsync(id);
            if (bankAccountData != null)
            {
                _context.BankAccountData.Remove(bankAccountData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountDataExists(int id)
        {
          return (_context.BankAccountData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
