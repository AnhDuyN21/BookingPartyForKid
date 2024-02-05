using Application;
using Application.Interfaces;
using Application.Services;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.RegisterAccountDTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructures;
using System.Diagnostics;
using WebAPI.Middlewares;
using WebAPI.Services;
using WebAPI.Validations.AccountValidate;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidation();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<IClaimsService, ClaimsService>();
            
           
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddTransient<IValidator<CreatedAccountDTO>, CreateAccountViewModelValidation>();
            services.AddTransient<IValidator<RegisterAccountDTO>, RegisterAccountViewModelValidation>();

            services.AddMemoryCache();
            return services;
        }
    }
}
