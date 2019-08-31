using CRateWallet.Models;
using CRateWallet.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRateWallet.ViewModels
{
    public class UserViewModels : INotifyPropertyChanged
    {
        public UserViewModels()
        {
            DataInDevice();
            DataPinPage();
            GoToCheckEmailPage = new Command(CheckEmailPageMethod);
            InputOTP = new Command<string>(InputOTPMethod);
            GoBackPage = new Command(BackPageMethod);
            DeletePin = new Command(DeletePinMethod);
            GoToCheckPinPage = new Command(CheckCheckPinMethod);
            GoToCheckOtpPage = new Command(CheckOtpPageMethod);
            GoBackByPin = new Command<int>(BackByPinMethod);
            pinNumber = "";
            email = "";
            name = "";
            surname = "";
            birthDate = "";
            gender = "";
            checkPin = 0;
            countBirthDate = 0;
            referencePin = "";

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

        private void DataPinPage()
        {
            if (checkPin == 1)
            {
                TitlePin = "สร้างรหัสผ่าน";
                ImgPin = "lock";
                BlackTextPin = "สร้างรหัสผ่าน";
                GrayTextPin = "ใส่รหัสผ่านของคุณ";
                CheckRefPin = true;
                SentPinCom = null;
                CheckSentPin = true;
                CheckWanPin = true;
            }
            else if (checkPin == 2)
            {
                TitlePin = "ยืนยันรหัสผ่าน";
                ImgPin = "lock";
                BlackTextPin = "ยืนยันรหัสผ่าน";
                GrayTextPin = "ใส่รหัสผ่านของคุณอีกครั้ง";
                CheckRefPin = true;
                SentPinCom = null;
                CheckSentPin = true;
                CheckWanPin = true;
            }
            else if (checkPin == 3)
            {
                TitlePin = "การยืนยัน OTP";
                ImgPin = "mail";
                BlackTextPin = "กรุณาใส่ OTP\nเพื่อยืนยัน email ของคุณ";
                GrayTextPin = "กรุณาใส่ OTP&#10;เพื่อยืนยัน email ของคุณ";
                CheckRefPin = false;
                SentPinCom = null;
                CheckSentPin = false;
                CheckWanPin = true;
            }
            else
            {
                TitlePin = "";
                ImgPin = "";
                BlackTextPin = "";
                GrayTextPin = "";
                CheckRefPin = false;
                SentPinCom = "";
                CheckSentPin = false;
                CheckWanPin = false;
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

        private int checkPin;
        public int CheckPin
        {
            get
            {
                return checkPin;
            }
            set
            {
                checkPin = value;
                OnPropertyChanged(nameof(CheckPin));
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

        private string referencePin;
        public string ReferencePin
        {
            get
            {
                return referencePin;
            }
            set
            {
                referencePin = value;
                OnPropertyChanged(nameof(ReferencePin));
            }
        }

        private string titlePin;

        public string TitlePin
        {
            get
            {
                return titlePin;
            }
            set
            {
                titlePin = value;
                OnPropertyChanged(nameof(TitlePin));
            }
        }

        private string imgPin;

        public string ImgPin
        {
            get
            {
                return imgPin;
            }
            set
            {
                imgPin = value;
                OnPropertyChanged(nameof(ImgPin));
            }
        }

        private string blackTextPin;
        public string BlackTextPin
        {
            get
            {
                return blackTextPin;
            }
            set
            {
                blackTextPin = value;
                OnPropertyChanged(nameof(BlackTextPin));
            }
        }

        private string grayTextPin;
        public string GrayTextPin
        {
            get
            {
                return grayTextPin;
            }
            set
            {
                grayTextPin = value;
                OnPropertyChanged(nameof(GrayTextPin));
            }
        }

        private string sentPinCom;
        public string SentPinCom
        {
            get
            {
                return sentPinCom;
            }
            set
            {
                sentPinCom = value;
                OnPropertyChanged(nameof(SentPinCom));
            }
        }

        private string wanningTextPin;
        public string WanningTextPin
        {
            get
            {
                return wanningTextPin;
            }
            set
            {
                wanningTextPin = value;
                OnPropertyChanged(nameof(WanningTextPin));
            }
        }

        private bool checkRefPin;
        public bool CheckRefPin
        {
            get
            {
                return checkRefPin;
            }
            set
            {
                checkRefPin = value;
                OnPropertyChanged(nameof(CheckRefPin));
            }
        }

        private bool checkSentPin;
        public bool CheckSentPin
        {
            get
            {
                return checkSentPin;
            }
            set
            {
                checkSentPin = value;
                OnPropertyChanged(nameof(CheckSentPin));
            }
        }

        private bool checkWanPin;
        public bool CheckWanPin
        {
            get
            {
                return checkWanPin;
            }
            set
            {
                checkWanPin = value;
                OnPropertyChanged(nameof(CheckWanPin));
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
        private void CheckEmailPageMethod()
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
                    await GoPageByPinAsync();
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

        private async Task GoPageByPinAsync()
        {
            if (checkPin == 1)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckPin(2, name, surname, birthDate, mobileNo, gender, email, pinNumber, null));
            }
            else if (checkPin == 2)
            {
                var check = CheckreRePin();
                if (check.Status)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisSuccess());
                }
                else
                {
                    WanningTextPin = check.Message;
                    CheckWanPin = false;
                }
            }
            else if (checkPin == 3)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UserData(email));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("ERROE!!!", "SERVER ERROR", "OK");
            }
        }

        private CheckStatusModel CheckreRePin()
        {
            if(Regex.IsMatch(rePinNumber, @"\D"))
            {
                return new CheckStatusModel()
                {
                    Status = false,
                    Message = "รหัสใหม่ที่ใส่ไม่ใฃ้ตัวเลข"
                };
            }
            else if(rePinNumber.Length != 6)
            {
                return new CheckStatusModel()
                {
                    Status = false,
                    Message = "รหัสใหม่ที่ใส่ไม่เท่ากับ 6 ตัว"
                };
            }
            else if(pinNumber != rePinNumber)
            {
                return new CheckStatusModel()
                {
                    Status = false,
                    Message = "รหัสใหม่ที่ใส่ไม่ตรงกับรหัสก่อนหน้า"
                };
            }
            else
            {
                return new CheckStatusModel()
                {
                    Status = true
                };
            }
            
        }

        public ICommand GoBackByPin { get; set; }
        private async void BackByPinMethod(int check)
        {
            if(check == 1)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UserData(email));
            }
            else if(check == 2)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckPin(1, name, surname, birthDate, mobileNo, gender, email, null, null));
            }
            else if (check == 3)
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
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
            await Application.Current.MainPage.Navigation.PushAsync(new CheckPin(1, name, surname, birthDate, mobileNo, gender, email, null, null));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
