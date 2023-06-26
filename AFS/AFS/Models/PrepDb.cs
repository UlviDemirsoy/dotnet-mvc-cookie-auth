using Microsoft.EntityFrameworkCore;
using AFS.Models;
using System;

namespace AFS.Models
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DatabaseContext>(), isProd);
            }
        }

        private static void SeedData(DatabaseContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Translations.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Translations.AddRange(
                    new Translation() { Text = "Dummy Data For Search", TextTranslation = "Dummy Data For Search translated" },
                    new Translation() { Text = "example data", TextTranslation = "example data translated" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
