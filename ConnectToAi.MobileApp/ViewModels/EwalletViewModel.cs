using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.Navigation;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class EwalletViewModel : BaseViewModel
    {
        public EwalletViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public delegate void PerformCalculation();
        [ObservableProperty]
        public float money = 0;

        [ObservableProperty]
        public float balanceMoney = 0;

        [RelayCommand]
        public void Payment()
        {
            if (Money > 0)
            {

            }

            PerformCalculation performCalculation = new PerformCalculation(test);
            var iasync = performCalculation.BeginInvoke(myFunction, null);
        }

        private void myFunction(IAsyncResult async)
        {

        }

        private static void test()
        {
            Thread.Sleep(10000);
        }
    }
}
