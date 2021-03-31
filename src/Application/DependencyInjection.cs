using AluguelIdeal.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace AluguelIdeal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateRequestBehaviour<,>));

            return services;
        }
    }
}
