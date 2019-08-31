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
    public partial class UserData : ContentPage
    {
        private string thisEmail { get; set; }
        public UserData(string email)
        {
            InitializeComponent();
            thisEmail = email;
            BindingContext = new UserViewModels()
            {
                Email = email
            };
        }

        protected override bool OnBackButtonPressed()
        {
            CheckEmailPageMethod();
            return true;
        }

        private void CheckEmailPageMethod()
        {
            Navigation.PushAsync(new CheckOtpPage(thisEmail));
        }
    }
}