using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using PracticeCalendar.Application;
using PracticeCalendar.Infrastructure;
using PracticeCalendar.Infrastructure.Persistence;
using System;

namespace PracticeCalendar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddProblemDetails();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //inject application
            builder.Services.AddApplicationServices();
            //inject infrastructure
            builder.Services.AddInfrastructureServices(builder.Configuration);


            var app = builder.Build();

            app.UseProblemDetails();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            // Seed Database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    //context.Database.Migrate();
                    context.Database.EnsureCreated();
                    //SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
                }
            }

            app.Run();
        }
    }
}