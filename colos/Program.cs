using colos.Data;
using colos.Services;
using Microsoft.EntityFrameworkCore;

namespace colos;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddDbContext<DatabaseContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
 
        builder.Services.AddScoped<IDbService, DbService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}