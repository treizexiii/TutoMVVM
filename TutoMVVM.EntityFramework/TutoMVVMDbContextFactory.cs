using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TutoMVVM.EntityFramework
{
    public class TutoMVVMDbContextFactory : IDesignTimeDbContextFactory<TutoMVVMDbContext>
    {
        public TutoMVVMDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<TutoMVVMDbContext>();
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=TutoMVVM;Trusted_Connection=true");

            return new TutoMVVMDbContext(options.Options);
        }
    }
}
