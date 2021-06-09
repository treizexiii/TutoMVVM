using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TutoMVVM.EntityFramework
{
    public class TutoMVVMDbContextFactory
    {
        private readonly string _connectionString;

        public TutoMVVMDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TutoMVVMDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TutoMVVMDbContext> options = new DbContextOptionsBuilder<TutoMVVMDbContext>();
            options.UseSqlServer(_connectionString);

            return new TutoMVVMDbContext(options.Options);
        }
    }
}
