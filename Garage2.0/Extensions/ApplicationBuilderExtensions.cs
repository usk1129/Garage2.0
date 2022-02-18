﻿using Garage2._0.Data;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Extensions
{

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<Garage2_0Context>();

             //   db.Database.EnsureDeleted();
             //   db.Database.Migrate();

                try
                {
                    await SeedData.InitAsync(db);
                }
                catch (Exception e)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(string.Join(" ", e.Message));
                    //throw;
                }
            }

            return app;
        }
    }
}