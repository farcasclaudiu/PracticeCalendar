﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Interfaces;
using PracticeCalendar.Infrastructure.Notification;
using PracticeCalendar.Infrastructure.Persistence;

namespace PracticeCalendar.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            IConfigurationRoot configuration)
        {
            string connectionString = configuration.GetConnectionString("SqliteConnection");

            services.AddDbContext(connectionString);
            services.AddTransient<IEmailSender, FileEmailSender>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }

        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
    }
}