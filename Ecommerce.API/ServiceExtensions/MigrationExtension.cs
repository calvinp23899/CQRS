using Ecommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.API.ServiceExtensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using EcommerceDbContext dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Migration database is successful");
        }
    }
}
