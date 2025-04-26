using DatabaseContext.DBHelper.Methods;
using Microsoft.Extensions.DependencyInjection;

namespace CatsLibrary.Extensions
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DbHelper>();
            db.Database.EnsureCreated();
        }
    }
}
