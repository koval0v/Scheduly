﻿using Microsoft.EntityFrameworkCore;

namespace SpecialtyService.DbAccess
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SpecialtyDbContext>();

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
