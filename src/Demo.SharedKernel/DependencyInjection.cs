using Demo.SharedKernel.Core.Abstractions;
using Demo.SharedKernel.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection InitializeSharedKernel(this IServiceCollection services)
    {
        services.AddSingleton<ITimeProvider, SystemClock>();
        return services;
    }
}