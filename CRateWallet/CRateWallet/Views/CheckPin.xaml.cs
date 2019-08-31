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
    public partial class CheckPin : ContentPage
    {
        public CheckPin(int check, string name, string surname, string birthDate, string mobileNo, string gender, string email)
        {
            InitializeComponent();
            if(check == 1)
            {
                BindingContext = new UserViewModels()
                {
                    Email = email,
                    Name = name,
                    Surname = surname,
                    BirthDate = birthDate,
                    MobileNo = mobileNo,
                    Gender = gender
                };
            }
        }
    }
}