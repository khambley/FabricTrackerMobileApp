﻿using System;
using System.Reflection;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using FabricTrackerMobileApp.ViewModels;
using FabricTrackerMobileApp.Data;

namespace FabricTrackerMobileApp
{
    public abstract class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder { get; private set; }

        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(Page)) ||
                    e.IsSubclassOf(typeof(ViewModelBase))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            ContainerBuilder.RegisterType<Repository>().SingleInstance();
        }
        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }

        
    }
}

