using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business.IoC
{
    public static class MediatRServiceCollectionExtension
    {
        public static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
