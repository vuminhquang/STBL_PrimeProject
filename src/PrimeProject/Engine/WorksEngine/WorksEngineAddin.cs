using System;
using System.ComponentModel.Composition;
using System.IO;
using AddinEngine.Abstract;
using AddInEngine.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorksEngine
{
    [Export(typeof(IDependencyResolver))]
    [Export(typeof(IConfigurationResolver))]
    [Export(typeof(IWebConfigurationResolver))]
    public class WorksEngineAddin : IDependencyResolver, IConfigurationResolver, IWebConfigurationResolver
    {
        //IDependencyResolver
        public void SetUp(IDependencyRegister services, IConfiguration configuration)
        {
            services.AddHostedService<WorksEngineService>();

            var connectionString = "";
            var currAssembly = typeof(WorksEngineAddin).Assembly;
            //Quick fix: sqlite cannot open DB
            var location = new Uri(currAssembly.Location);
            var basePath = new FileInfo(location.AbsolutePath).Directory.FullName;
            // connectionString = connectionString.Replace("Filename=", $"Filename={Environment.CurrentDirectory}/");
            //"Filename=/DB/CoreEngine.DB"
            connectionString = $"Filename={basePath}/DB/WorksEngineHangfire.db";

            // services.AddHangfire(
            //     config => {  }//config.UseSQLiteStorage(connectionString)
            // );
            // services.AddHangfireServer();
            
        }

        //IConfigurationResolver
        public void SetUp(IConfigurationRegister configurationRegister)
        {
        }

        //IWebConfigurationResolver
        public void SetUp(dynamic app, dynamic env)
        {
            // app.UseHangfireDashboard();
        }
    }
}
