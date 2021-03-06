﻿using System;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PT.MarketDataService.Topshelf
{
    public class Bootstrapper
    {
        public Container Container { get; }

        public Bootstrapper()
        {
            Container = new Container();
        }

        public void Initialize()
        {
            // Default
            Container.Options.SuppressLifestyleMismatchVerification = true;
            Container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(
                defaultLifestyle: new ThreadScopedLifestyle(),
                fallbackLifestyle: new AsyncScopedLifestyle());

            // Register modules dynamically
            Assembly.Load("PT.MarketDataService.Application");
            Assembly.Load("PT.MarketDataService.Application.Owin");
            Assembly.Load("PT.MarketDataService.Core");
            Assembly.Load("PT.MarketDataService.Repository.EfRepository");
            Assembly.Load("PT.MarketDataService.Infrastructure");

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Container.RegisterPackages(assemblies);
        }
    }
}