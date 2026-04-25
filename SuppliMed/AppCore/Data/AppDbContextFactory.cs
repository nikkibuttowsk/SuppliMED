using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace AppCore.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // Using 127.0.0.1 is more stable than 'localhost' on Mac
            var connectionString = "server=127.0.0.1;port=3306;database=supplimed_db;user=root;password=;";
            
            // This requires the 'using Microsoft.EntityFrameworkCore;' above
            optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 32)));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}