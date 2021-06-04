using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.EntityFramework.Services.Common;

namespace TutoMVVM.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly TutoMVVMDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccountDataService(TutoMVVMDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Account> Get(int id)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .ToListAsync();
            }
        }

        public async Task<Account> GetByUsername(string username)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(e => e.AccountHolder.Username == username);
            }
        }

        public async Task<Account> GetByEmail(string email)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(e => e.AccountHolder.Email == email);
            }
        }
    }
}
