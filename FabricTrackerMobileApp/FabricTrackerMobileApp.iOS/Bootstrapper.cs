using System;
namespace FabricTrackerMobileApp.iOS
{
    public class Bootstrapper : FabricTrackerMobileApp.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();
        }
    }
}

