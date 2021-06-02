using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.EntityFramework.Services.Common
{
    public class NonQueryDataService<T> where T : DomainObjet
    {
        private readonly TutoMVVMDbContextFactory _contextFactory;

        public NonQueryDataService(TutoMVVMDbContextFactory contextFactory)
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
    }
}
