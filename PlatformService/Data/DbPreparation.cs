using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DbPreparation
    {
        public static void Populate(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                BootstrapData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void BootstrapData(AppDbContext context)
        {
            if(context.Platforms.Any())
            {
                return;
            }

            Console.WriteLine("--> Bootstraping Data...");
            context.Platforms.AddRange(
                new Platform() { Name = ".NET", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Golang", Publisher = "Google", Cost = "Free" }
            );

            context.SaveChanges();
        }
    }
    
}