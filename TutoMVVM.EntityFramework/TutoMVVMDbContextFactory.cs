using Microsoft.EntityFrameworkCore;
using System;

namespace TutoMVVM.EntityFramework
{
    public class TutoMVVMDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public TutoMVVMDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public TutoMVVMDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TutoMVVMDbContext> options = new DbContextOptionsBuilder<TutoMVVMDbContext>();

            _configureDbContext(options);

            return new TutoMVVMDbContext(options.Options);
        }
    }
}
