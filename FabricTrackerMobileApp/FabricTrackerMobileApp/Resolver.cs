using System;
using Autofac;

namespace FabricTrackerMobileApp
{
    public static class Resolver
    {
        private static IContainer container;

        public static void Initialize(IContainer container)
        {
            Resolver.container = container;
        }

        public static T Resolve<T>(string name = null, object value = null)
        {
            if (name != null)
            {
                return container.Resolve<T>(new NamedParameter(name, value));
            }
            else
            {
                return container.Resolve<T>();
            }
            
            
        }
    }
}

