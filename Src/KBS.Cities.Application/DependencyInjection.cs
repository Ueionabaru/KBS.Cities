using AutoMapper;
using FluentValidation;
using KBS.Cities.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KBS.Cities.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var execAssembly = Assembly.GetExecutingAssembly();

            services.AddValidatorsFromAssembly(execAssembly);
            services.AddAutoMapper(execAssembly);
            services.AddMediatR(execAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
