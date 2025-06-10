using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace RazorClassLibrary;

public static class RoutesExtensions
{
    public static IServiceCollection UrlRout(this IServiceCollection services, string route)
    {
        services.Configure<RazorPagesOptions>(opt =>
        {
            opt.Conventions.AddAreaPageRoute(
                areaName: "MyFeature",
                pageName: "/About",
                route: route);
        });

        return services;
    }
}