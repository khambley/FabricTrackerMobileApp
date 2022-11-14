using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Input;
using FabricTrackerMobileApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FabricTrackerMobileApp.Pages
{
    public partial class DataBackupRestorePage : ContentPage
    {
        public DataBackupRestorePage(DataBackupRestoreViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}

