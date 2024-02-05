using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();  
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPartyRepository, PartyRepository>();
            

            services.AddSingleton<ICurrentTime, CurrentTime>();



          



            // ATTENTION: if you do migration please check file README.md
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(databaseConnection);
                /*option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);*/
            });

            // this configuration just use in-memory for fast develop
            //services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("test"));

            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            return services;
        }
    }
}
