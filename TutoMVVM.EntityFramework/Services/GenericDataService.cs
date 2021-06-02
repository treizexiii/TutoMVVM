using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.EntityFramework.Services.Common;

namespace TutoMVVM.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObjet
    {
        private readonly TutoMVVMDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(TutoMVVMDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<T> Get(int id)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }
    }
}
