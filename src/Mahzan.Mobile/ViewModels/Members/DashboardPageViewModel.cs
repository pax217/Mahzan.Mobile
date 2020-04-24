using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mahzan.Mobile.ViewModels.Members
{
    public class DashboardPageViewModel : ViewModelBase
    {
        public DashboardPageViewModel(
            INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Dashboard";
        }
    }
}
