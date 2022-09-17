using System;
namespace FabricTrackerMobileApp.Droid
{
    public class Bootstrapper : FabricTrackerMobileApp.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();
        }
    }
}

