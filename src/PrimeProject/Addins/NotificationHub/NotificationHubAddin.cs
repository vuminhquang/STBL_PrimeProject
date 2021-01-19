using System;
using System.ComponentModel.Composition;
using System.Linq;
using AddinEngine.Abstract;
using AddInEngine.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedClassy;

namespace NotificationHub
{
    [Export(typeof(IDependencyResolver))]
    [Export(typeof(IConfigurationResolver))]
    [Export(typeof(IWebConfigurationResolver))]
    public class NotificationHubAddin : IDependencyResolver, IConfigurationResolver, IWebConfigurationResolver
    {
        public void SetUp(IDependencyRegister services, IConfiguration configuration)
        {
            var registered = services.Any(i => i.ServiceType.Assembly.FullName.Contains("NotificationHub", StringComparison.OrdinalIgnoreCase));
            if (registered) return;
            
            //Add the UI discoverable class, for blazor to add dynamically later
            services.AddSingleton<IDiscoverableUI>(provider => new _discovery());

            //Using SignalR with Blazor
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr-blazor-webassembly?view=aspnetcore-5.0&tabs=visual-studio
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        public void SetUp(IConfigurationRegister app)
        {
            // app.UseEndpoints(endpoints =>
            // {
            //     //SignalR
            //     endpoints.MapHub<SignalRHub>("/signalr-hub");
            // });
        }

        public void SetUp(dynamic app, dynamic env)
        {
            var app2Setup = app as IApplicationBuilder;
            app2Setup.UseResponseCompression();

            app2Setup.UseEndpoints(endpoints =>
            {
                //SignalR
                endpoints.MapHub<SignalRHub>("/signalr-hub");
            });
        }
    }
}
