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
        private int thisCheck;
        private string thisEmail;
        private string thisName;
        private string thisSurname;
        private string thisBirthDate;
        private string thisMobileNo;
        private string thisGender;
        public CheckPin(int check, string name, string surname, string birthDate, string mobileNo, string gender, string email, string pin, string reference)
        {
            InitializeComponent();
            thisCheck = check;
            thisEmail = email;
            thisName = name;
            thisSurname = surname;
            thisBirthDate = birthDate;
            thisMobileNo = mobileNo;
            thisGender = gender;
            if(check == 1)
            {
                BindingContext = new UserViewModels()
                {
                    Email = email,
                    Name = name,
                    Surname = surname,
                    BirthDate = birthDate,
                    MobileNo = mobileNo,
                    Gender = gender,
                    CheckPin = check
                };
            }
            else if(check == 2)
            {
                BindingContext = new UserViewModels()
                {
                    Email = email,
                    Name = name,
                    Surname = surname,
                    BirthDate = birthDate,
                    MobileNo = mobileNo,
                    Gender = gender,
                    RePinNumber = pin,
                    CheckPin = check
                };
            }
            else if (check == 3)
            {
                BindingContext = new UserViewModels()
                {
                    CheckPin = check,
                    Email = email,
                    ReferencePin = reference
                };
            }
            else
            {
                DisplayAlert("ERROE!!!", "SERVER ERROR", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            CheckEmailPageMethod();
            return true;
        }

        private void CheckEmailPageMethod()
        {
            if (thisCheck == (int)StatusType.PinPage.SetPassword)
            {
                Navigation.PushAsync(new UserData(thisEmail));
            }
            else if (thisCheck == (int)StatusType.PinPage.ReSetPaqssword)
            {
                Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.SetPassword, thisName, thisSurname, thisBirthDate, thisMobileNo, thisGender, thisEmail, null, null));
            }
            else if (thisCheck == (int)StatusType.PinPage.SentOTPRegis || thisCheck == (int)StatusType.PinPage.SentOTPLogin)
            {
                Navigation.PopToRootAsync();
            }
            else if (thisCheck == (int)StatusType.PinPage.ChangePassword)
            {
                Navigation.PopToRootAsync();
            }
            else if (thisCheck == (int)StatusType.PinPage.ReChangePassword)
            {
                Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.ChangePassword, null, null, null, null, null, thisEmail, null, null));
            }
            else
            {
                DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดตอดต่อผู้ดูแล", "ตกลง");
                Navigation.PopToRootAsync();
            }
        }
    }
}