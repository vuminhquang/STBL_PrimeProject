using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SharedClassy;

namespace WebApplication
{
    public static class MvcHelper
    {
        public static void AddMvcFromOtherAssemblies(this IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers(options => options.EnableEndpointRouting = false);
            var mvcControllers = services.Where(service => typeof(IDiscoverableMVC).IsAssignableFrom(service.ServiceType));
            var mvcAssemblies = mvcControllers.Select(controller => controller.ServiceType.Assembly)
                .GroupBy(assembly => assembly.FullName).Select(grp => grp.First()).ToList();
            foreach (var assembly in mvcAssemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
            }
        }
    }
}