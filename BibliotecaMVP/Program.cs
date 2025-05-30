using BibliotecaMVP.Components;
using BibliotecaMVP.Data;
using BibliotecaMVP.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace BibliotecaMVP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();
            builder.Services.AddTransient<SeedDb>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));

            var app = builder.Build();

            SeedData(app);
            static void SeedData(WebApplication app)
            {
                IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();
                using IServiceScope scope = scopedFactory!.CreateScope();
                SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
                service!.SeedAsync().Wait();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}