using System;
using FabricTrackerMobileApp.Models;

namespace FabricTrackerMobileApp.ViewModels
{
    public class FabricViewModel : ViewModelBase
    {
        public FabricViewModel(Fabric item)
        {
            Item = item;
        }

        public event EventHandler ItemStatusChanged;

        public Fabric Item { get; private set; }
    }
}

