using Microsoft.EntityFrameworkCore;

namespace TokenService.DbAccess
{
    public static class MigrationsExtension
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            if (dbContext is not null)
            {
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError($"Could not apply migrations. {ex.Message}");
                    throw;
                }
            }
        }
    }
}
