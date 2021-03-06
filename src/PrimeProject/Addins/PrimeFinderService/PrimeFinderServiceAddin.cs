﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using AddinEngine.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeFinderService.Services;
using PrimeService.Data;
using SharedClassy;

namespace PrimeFinderService
{
    [Export(typeof(IDependencyResolver))]
    [Export(typeof(IConfigurationResolver))]
    [Export(typeof(IWebConfigurationResolver))]
    public class PrimeFinderServiceAddin : IDependencyResolver, IConfigurationResolver, IWebConfigurationResolver
    {
        public void SetUp(IDependencyRegister services, IConfiguration configuration)
        {
            //Not do this twice
            var tDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(PrimeFinderServiceAddin));
            if(tDescriptor != null)
                return;
            services.AddSingleton<PrimeFinderServiceAddin>();

            var currAssembly = typeof(PrimeFinderServiceAddin).Assembly;
            foreach (var type in Helper.GetAllTypesThatImplementInterface<IDiscoverableMVC>(currAssembly))
            {
                services.AddTransient(type);
            }
            foreach (var type in Helper.GetAllTypesThatImplementInterface<IDiscoverableUI>(currAssembly))
            {
                services.AddSingleton(provider => Activator.CreateInstance(type) as IDiscoverableUI);
            }

            var connectionString = configuration["CoreEngine:ConnectionString"];
            //Quick fix: sqlite cannot open DB
            var location = new Uri(currAssembly.Location);
            var basePath = new FileInfo(location.AbsolutePath).Directory.FullName;
            connectionString = $"Filename={basePath}/DB/PrimeService.db";

            //Working with Blazor -> need using DbContextFactory
            services.AddDbContextFactory<PrimeServiceContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<PrimeFinder>();
        }

        public void SetUp(IConfigurationRegister configurationRegister)
        {
        }

        public void SetUp(dynamic app, dynamic env)
        {
        }
    }
}
