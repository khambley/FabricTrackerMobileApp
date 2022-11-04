using System;
using FabricTrackerMobileApp.Data;

namespace FabricTrackerMobileApp.ViewModels
{
    public class DataBackupRestoreViewModel : ViewModelBase
    {
        private readonly Repository repository;

        public DataBackupRestoreViewModel(Repository repository)
        {
            this.repository = repository;
        }
    }
}

