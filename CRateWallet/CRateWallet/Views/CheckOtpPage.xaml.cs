using CRateWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRateWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckOtpPage : ContentPage
    {
        public CheckOtpPage(string email)
        {
            InitializeComponent();
            BindingContext = new UserViewModels()
            {
                Email = email
            };
        }

        protected override bool OnBackButtonPressed()
        {
            BackMethod();
            return true;
        }

        private void BackMethod()
        {
            Navigation.PopToRootAsync();
        }
    }
}