using CRateWallet.Models;
using CRateWallet.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRateWallet.ViewModels
{
    public class UserViewModels : INotifyPropertyChanged
    {
        public UserViewModels()
        {
            DataInDevice();
            GoToCheckEmailPage = new Command(CheckEmailPageMethod);
            InputOTP = new Command<string>(InputOTPMethod);
            GoBackPage = new Command(BackPageMethod);
            DeletePin = new Command(DeletePinMethod);
            GoToCheckPinPage = new Command(CheckCheckPinMethod);
            GoBackByOtpPage = new Command(BackByOtpPageMethod);
            GoToCheckOtpPage = new Command(CheckOtpPageMethod);
            pinNumber = "";
            email = "";
            name = "";
            surname = "";
            birthDate = "";
            gender = "";
            countBirthDate = 0;
        }

        private int countBirthDate;

        private void DataInDevice()
        {
            keyboardData = new List<KeyboardOtpModel>();
            for(int i = 1; i <= 9; i++)
            {
                keyboardData.Add(new KeyboardOtpModel()
                {
                    Value = i,
                    Row = (i-1)/3,
                    Column = (i + 2) % 3
                });
            }
            roundColor = new List<PinColorModel>();
            for(int i = 1; i <= 6; i++)
            {
                roundColor.Add(new PinColorModel()
                {
                    Color = "White"
                });
            }
        }

        private List<KeyboardOtpModel> keyboardData;
        public List<KeyboardOtpModel> KeyboardData
        {
            get
            {
                return keyboardData;
            }
            set
            {
                keyboardData = value;
                OnPropertyChanged(nameof(KeyboardData));
            }
        }

        private List<PinColorModel> roundColor;
        public List<PinColorModel> RoundColor
        {
            get
            {
                return roundColor;
            }
            set
            {
                roundColor = value;
                OnPropertyChanged(nameof(RoundColor));
            }
        }


        private string pinNumber;
        public string PinNumber
        {
            get
            {
                return pinNumber;
            }
            set
            {
                pinNumber = value;
                OnPropertyChanged(nameof(PinNumber));
            }
        }

        private string rePinNumber;
        public string RePinNumber
        {
            get
            {
                return rePinNumber;
            }
            set
            {
                rePinNumber = value;
                OnPropertyChanged(nameof(RePinNumber));
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string birthDate;
        public string BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
                CheckFormatBirthDate();
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        private string mobileNo;
        public string MobileNo
        {
            get
            {
                return mobileNo;
            }
            set
            {
                mobileNo = value;
                OnPropertyChanged(nameof(MobileNo));
            }
        }

        private string gender;
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private void CheckFormatBirthDate()
        {
            string checkBirthDate = birthDate;
            int count = checkBirthDate.Length;
            if (count == 2 && count > countBirthDate)
            {
                birthDate = checkBirthDate + "/";
            }
            else if (count == 4 && count < countBirthDate)
            {
                birthDate = checkBirthDate.Remove(count - 1);
            }
            else if (count == 5 && count > countBirthDate)
            {
                birthDate = checkBirthDate + "/";
            }
            else if (count == 7 && count < countBirthDate)
            {
                birthDate = checkBirthDate.Remove(count - 1);
            }
            else
            {
                birthDate = checkBirthDate;
            }
            countBirthDate = count;
        }

        public ICommand GoToCheckEmailPage { get; set; }
        private async void CheckEmailPageMethod()
        {
            CheckOtpPageMethod();
        }

        public ICommand GoToCheckOtpPage { get; set; }
        private async void CheckOtpPageMethod()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CheckOtpPage(email));
        }

        public ICommand InputOTP { get; set; }
        private async void InputOTPMethod(string textOtp)
        {
            string value;
            int checkOtp = Int32.Parse(textOtp);
            if (checkOtp < 200)
            {
                value = textOtp.Substring(textOtp.Length - 1);
                pinNumber = pinNumber + value;
                string otp = pinNumber;
                int count = otp.Length;
                if (count == 6)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new UserData(email));
                }
                else
                {
                    ChangeColorPin(count);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("ERROE!!!", "SERVER ERROR", "OK");
            }
        }

        private void ChangeColorPin(int count)
        {
            RoundColor = new List<PinColorModel>();
            for (int i = 1; i <= 6; i++)
            {
                if (i <= count)
                {
                    RoundColor.Add(new PinColorModel()
                    {
                        Color = "#7C8476"
                    });
                }
                else
                {
                    RoundColor.Add(new PinColorModel()
                    {
                        Color = "White"
                    });
                }
            }
        }

        public ICommand GoBackPage { get; set; }
        private async void BackPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public ICommand GoBackByOtpPage { get; set; }
        private async void BackByOtpPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public ICommand DeletePin { get; set; }
        private void DeletePinMethod()
        {
            if (pinNumber != "")
            {
                string waitString = pinNumber;
                pinNumber = waitString.Remove(waitString.Length-1);
                int count = pinNumber.Length;
                ChangeColorPin(count);
            }
        }

        public ICommand GoToCheckPinPage { get; set; }
        private async void CheckCheckPinMethod()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CheckPin(1, name, surname, birthDate, mobileNo, gender, email));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
