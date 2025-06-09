using Blazored.LocalStorage;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Radzen;

namespace L4FutureTechBlazor.Services;
public static class L4FutureTechBlazor_DI
{
    public static IServiceCollection Add_L4FutureTechBlazor_DI(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        _ = services.AddRadzenComponents();
        _ = services.AddLocalization();
        _ = services.AddBlazoredLocalStorage();

        return services;
    }
}
