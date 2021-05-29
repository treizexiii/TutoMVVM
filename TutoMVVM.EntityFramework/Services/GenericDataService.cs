using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;

namespace TutoMVVM.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObjet
    {
        private readonly TutoMVVMDbContextFactory _contextFactory;

        public GenericDataService(TutoMVVMDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                T deletedEntity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);

                try
                {
                    context.Set<T>().Remove(deletedEntity);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
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

        public async Task<T> Update(int id, T entity)
        {
            using (TutoMVVMDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            }
        }
    }
}
