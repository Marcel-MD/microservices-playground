using CommandService.Models;

namespace CommandService.Data
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
                new Platform() { Name = ".NET"},
                new Platform() { Name = "Node.js"},
                new Platform() { Name = "Golang"}
            );

            context.Commands.AddRange(
                new Command() {HowTo = "Run application", CommandLine = "dotnet run", PlatformId = 1},
                new Command() {HowTo = "Install Package", CommandLine = "npm install", PlatformId = 2},
                new Command() {HowTo = "Generate Module File", CommandLine = "go mod init", PlatformId = 3},
                new Command() {HowTo = "Build application", CommandLine = "dotnet build", PlatformId = 1},
                new Command() {HowTo = "Generate New Web Api", CommandLine = "dotnet new webapi", PlatformId = 1}
            );

            context.SaveChanges();
        }
    }
    
}