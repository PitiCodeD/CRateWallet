using CRateWallet.Models;
using CRateWallet.Views;
using MvvmCross.Platform;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRateWallet.ViewModels
{
    public class UserViewModels : INotifyPropertyChanged
    {
        private HttpClient client = new HttpClient();
        private readonly HelperSetting helperSetting = new HelperSetting();

        public UserViewModels()
        {
            DataInDevice();
            DataPinPage();
            GoToCheckEmailPage = new Command(CheckEmailPageMethod);
            InputOTP = new Command<string>(InputOTPMethod);
            GoBackPage = new Command(BackPageMethod);
            DeletePin = new Command(DeletePinMethod);
            GoToCheckPinPage = new Command(CheckCheckPinMethod);
            GoToRootPage = new Command(RootPageMethod);
            GoBackByPin = new Command(BackByPinMethod);
            GoToSentOtpRegis = new Command(SentOtpRegisMethod);
            GoToSetFingerPrint = new Command(SetFingerPrintMethod);
            GoToHomePage = new Command(HomePageMethod);
            pinNumber = "";
            email = "";
            name = "";
            surname = "";
            birthDate = "";
            gender = "";
            checkPin = 0;
            countBirthDate = 0;
            referencePin = "";
            setDate = default;
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
            if (checkPin == (int)StatusType.PinPage.SetPassword || checkPin == (int)StatusType.PinPage.ChangePassword)
            {
                TitlePin = "สร้างรหัสผ่าน";
                ImgPin = "lock";
                BlackTextPin = "สร้างรหัสผ่าน";
                GrayTextPin = "ใส่รหัสผ่านของคุณ";
                CheckRefPin = false;
                SentPinCom = null;
                CheckSentPin = false;
                CheckWanPin = false;
            }
            else if (checkPin == (int)StatusType.PinPage.ReSetPaqssword || checkPin == (int)StatusType.PinPage.ReChangePassword)
            {
                TitlePin = "ยืนยันรหัสผ่าน";
                ImgPin = "lock";
                BlackTextPin = "ยืนยันรหัสผ่าน";
                GrayTextPin = "ใส่รหัสผ่านของคุณอีกครั้ง";
                CheckRefPin = false;
                SentPinCom = null;
                CheckSentPin = false;
                CheckWanPin = false;
            }
            else if (checkPin == (int)StatusType.PinPage.SentOTPRegis || checkPin == (int)StatusType.PinPage.SentOTPLogin)
            {
                TitlePin = "การยืนยัน OTP";
                ImgPin = "mail";
                BlackTextPin = "กรุณาใส่ OTP\nเพื่อยืนยัน email ของคุณ";
                GrayTextPin = "เราได้ส่ง OTP ไปที่ email ของคุณแล้ว";
                CheckRefPin = true;
                SentPinCom = "ส่ง OTP อีกครั้ง";
                CheckSentPin = true;
                CheckWanPin = false;
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
                DataPinPage();
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

        private DateTime setDate;
        public DateTime SetDate
        {
            get
            {
                return setDate;
            }
            set
            {
                setDate = value;
                OnPropertyChanged(nameof(SetDate));
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
            await GetRequestOtpForRegis();
        }

        private async Task GetRequestOtpForRegis()
        {
            var dataOtp = await RequestOtpForRegis(email);
            if (dataOtp == null)
            {
                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                if (dataOtp.Status == (int)StatusType.StatusRetureData.Success)
                {
                    string referenceOtp = "ref. " + dataOtp.Data;
                    await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", dataOtp.Message, "ตกลง");
                    await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.SentOTPRegis, null, null, null, null, null, email, null, referenceOtp));
                }
                else if (dataOtp.Status == (int)StatusType.StatusRetureData.ShowMessage)
                {
                    await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", dataOtp.Message, "ตกลง");
                }
                else if (dataOtp.Status == (int)StatusType.StatusRetureData.NotShowMessage)
                {
                    await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                }
                else if (dataOtp.Status == (int)StatusType.StatusRetureData.SecondLogin)
                {
                    string referenceOtp = "ref. " + dataOtp.Data;
                    await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", dataOtp.Message, "ตกลง");
                    await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.SentOTPLogin, null, null, null, null, null, email, null, referenceOtp));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
            }
        }

        private async Task<ResultModel<string>> RequestOtpForRegis(string email)
        {
            try
            {
                string host = helperSetting.Host + "user/otpregis";
                var objModel = new
                {
                    Text = email
                };
                StringContent requestMessage = new StringContent($"{JsonConvert.SerializeObject(objModel)}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(host, requestMessage);
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel<string>>(body);
            }
            catch (Exception e)
            {
                return new ResultModel<string>()
                {
                    Status = (int)StatusType.StatusRetureData.ShowMessage,
                    Message = "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง"
                };
            }
            
        }
        public ICommand GoToRootPage { get; set; }
        private async void RootPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public ICommand InputOTP { get; set; }
        private async void InputOTPMethod(string textOtp)
        {
            string value;
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

        private async Task TestSecureStorage()
        {
            var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            string a = "a";
            string b = "b";
            string c = "c";

        }

        private async Task GoPageByPinAsync()
        {
            if (checkPin == (int)StatusType.PinPage.SetPassword)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.ReSetPaqssword, name, surname, birthDate, mobileNo, gender, email, pinNumber, null));
            }
            else if (checkPin == (int)StatusType.PinPage.ReSetPaqssword)
            {
                var check = CheckreRePin();
                if (check.Status)
                {
                    string[] perDate = birthDate.Split('/');
                    gender += 1;
                    setDate = new DateTime(Int32.Parse(perDate[2]), Int32.Parse(perDate[1]), Int32.Parse(perDate[0]));
                    var dataToken = await Register();
                    if (dataToken == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        if (dataToken.Status == (int)StatusType.StatusRetureData.Success)
                        {
                            try
                            {
                                await SecureStorage.SetAsync("RefreshToken", dataToken.Data.RefreshToken);
                                await SecureStorage.SetAsync("AccessToken", dataToken.Data.AccessToken);
                                await TestSecureStorage();
                                await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", dataToken.Message, "ตกลง");
                                await Application.Current.MainPage.Navigation.PushAsync(new RegisSuccess());
                            }
                            catch (Exception)
                            {
                                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "อุปกรณ์ของท่านไม่สามารถใฃ้แอพพลิเคชั่นนี้ได้", "ตกลง");
                                await Application.Current.MainPage.Navigation.PopToRootAsync();
                            }
                            
                        }
                        else if (dataToken.Status == (int)StatusType.StatusRetureData.ShowMessage)
                        {
                            WanningTextPin = dataToken.Message;
                            CheckWanPin = true;
                            pinNumber = "";
                            ChangeColorPin(0);
                        }
                        else if (dataToken.Status == (int)StatusType.StatusRetureData.NotShowMessage)
                        {
                            WanningTextPin = "ระบบไม่สามารถใช้ได้";
                            CheckWanPin = true;
                            pinNumber = "";
                            ChangeColorPin(0);
                        }
                        else if (dataToken.Status == (int)StatusType.StatusRetureData.BackToFirstPage)
                        {
                            WanningTextPin = "ระบบไม่สามารถใช้ได้";
                            CheckWanPin = true;
                            pinNumber = "";
                            ChangeColorPin(0);
                            await Application.Current.MainPage.Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                            await Application.Current.MainPage.Navigation.PopToRootAsync();
                        }
                    }
                }
                else
                {
                    WanningTextPin = check.Message;
                    CheckWanPin = true;
                    pinNumber = "";
                    ChangeColorPin(0);
                }
            }
            else if (checkPin == (int)StatusType.PinPage.SentOTPRegis)
            {
                var dataCheckEmail = await VerifiedEmailForRegis();
                if (dataCheckEmail == null)
                {
                    await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else
                {
                    if (dataCheckEmail.Status == (int)StatusType.StatusRetureData.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", dataCheckEmail.Message, "ตกลง");
                        await Application.Current.MainPage.Navigation.PushAsync(new UserData(email));
                    }
                    else if (dataCheckEmail.Status == (int)StatusType.StatusRetureData.ShowMessage)
                    {
                        WanningTextPin = dataCheckEmail.Message;
                        CheckWanPin = true;
                        pinNumber = "";
                        ChangeColorPin(0);
                    }
                    else if (dataCheckEmail.Status == (int)StatusType.StatusRetureData.NotShowMessage)
                    {
                        WanningTextPin = "ระบบไม่สามารถใช้ได้";
                        CheckWanPin = true;
                        pinNumber = "";
                        ChangeColorPin(0);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                }
            }
            else if(checkPin == (int)StatusType.PinPage.SentOTPLogin)
            {
                var dataCheckEmail = await VerifiedEmailForChangePin();
                if (dataCheckEmail == null)
                {
                    await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else
                {
                    if (dataCheckEmail.Status == (int)StatusType.StatusRetureData.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", dataCheckEmail.Message, "ตกลง");
                        await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.ChangePassword, null, null, null, null, null, email, null, null));
                    }
                    else if (dataCheckEmail.Status == (int)StatusType.StatusRetureData.ShowMessage)
                    {
                        WanningTextPin = dataCheckEmail.Message;
                        CheckWanPin = true;
                        pinNumber = "";
                        ChangeColorPin(0);
                    }
                    else if (dataCheckEmail.Status == (int)StatusType.StatusRetureData.NotShowMessage)
                    {
                        WanningTextPin = "ระบบไม่สามารถใช้ได้";
                        CheckWanPin = true;
                        pinNumber = "";
                        ChangeColorPin(0);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                }
            }
            else if (checkPin == (int)StatusType.PinPage.ChangePassword)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.ReChangePassword, null, null, null, null, null, email, pinNumber, null));
            }
            else if (checkPin == (int)StatusType.PinPage.ReChangePassword)
            {
                var check = CheckreRePin();
                if (check.Status)
                {
                    var dataToken = await ChangePin();
                    if (dataToken == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        if (dataToken.Status == (int)StatusType.StatusRetureData.Success)
                        {
                            try
                            {
                                await SecureStorage.SetAsync("RefreshToken", dataToken.Data.RefreshToken);
                                await SecureStorage.SetAsync("AccessToken", dataToken.Data.AccessToken);
                                await TestSecureStorage();
                                await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", dataToken.Message, "ตกลง");
                                HomePageMethod();
                            }
                            catch (Exception)
                            {
                                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "อุปกรณ์ของท่านไม่สามารถใฃ้แอพพลิเคชั่นนี้ได้", "ตกลง");
                                await Application.Current.MainPage.Navigation.PopToRootAsync();
                            }

                        }
                        else if (dataToken.Status == (int)StatusType.StatusRetureData.ShowMessage)
                        {
                            WanningTextPin = dataToken.Message;
                            CheckWanPin = true;
                            pinNumber = "";
                            ChangeColorPin(0);
                        }
                        else if (dataToken.Status == (int)StatusType.StatusRetureData.NotShowMessage)
                        {
                            WanningTextPin = "ระบบไม่สามารถใช้ได้";
                            CheckWanPin = true;
                            pinNumber = "";
                            ChangeColorPin(0);
                        }
                        else if (dataToken.Status == (int)StatusType.StatusRetureData.BackToFirstPage)
                        {
                            WanningTextPin = "ระบบไม่สามารถใช้ได้";
                            CheckWanPin = true;
                            pinNumber = "";
                            ChangeColorPin(0);
                            await Application.Current.MainPage.Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดกรอกข้อมูลใหม่อีกครั้ง", "ตกลง");
                            await Application.Current.MainPage.Navigation.PopToRootAsync();
                        }
                    }
                }
                else
                {
                    WanningTextPin = check.Message;
                    CheckWanPin = true;
                    pinNumber = "";
                    ChangeColorPin(0);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดตอดต่อผู้ดูแล", "ตกลง");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private async Task<ResultModel<RegisModel>> ChangePin()
        {
            try
            {
                string host = helperSetting.Host + "user/changepin";
                var objModel = new
                {
                    Email = email,
                    Pin = pinNumber,
                    RePin = rePinNumber
                };
                StringContent requestMessage = new StringContent($"{JsonConvert.SerializeObject(objModel)}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(host, requestMessage);
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel<RegisModel>>(body);
            }
            catch (Exception e)
            {
                return new ResultModel<RegisModel>()
                {
                    Status = (int)StatusType.StatusRetureData.ShowMessage,
                    Message = "ระบบเกิดความผิดพลาด"
                };
            }
        }

        private async Task<ResultModel<RegisModel>> Register()
        {
            try
            {
                string host = helperSetting.Host + "user/register";
                var objModel = new
                {
                    Email = email,
                    Name = name,
                    Surname = surname,
                    BirthDate = setDate,
                    MobileNo = mobileNo,
                    Gender = gender,
                    Pin = pinNumber,
                    RePin = rePinNumber
                };
                StringContent requestMessage = new StringContent($"{JsonConvert.SerializeObject(objModel)}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(host, requestMessage);
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel<RegisModel>>(body);
            }
            catch (Exception e)
            {
                return new ResultModel<RegisModel>()
                {
                    Status = (int)StatusType.StatusRetureData.ShowMessage,
                    Message = "ระบบเกิดความผิดพลาด"
                };
            }
        }

        private async Task<ResultModel<bool>> VerifiedEmailForRegis()
        {
            try
            {
                string host = helperSetting.Host + "user/checkemail";
                var objModel = new
                {
                    Email = email,
                    Otp = pinNumber
                };
                StringContent requestMessage = new StringContent($"{JsonConvert.SerializeObject(objModel)}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(host, requestMessage);
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel<bool>>(body);
            }
            catch (Exception e)
            {
                return new ResultModel<bool>()
                {
                    Status = (int)StatusType.StatusRetureData.ShowMessage,
                    Message = "ระบบเกิดความผิดพลาด"
                };
            }
        }

        private async Task<ResultModel<bool>> VerifiedEmailForChangePin()
        {
            try
            {
                string host = helperSetting.Host + "user/checkemailpin";
                var objModel = new
                {
                    Email = email,
                    Otp = pinNumber
                };
                StringContent requestMessage = new StringContent($"{JsonConvert.SerializeObject(objModel)}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(host, requestMessage);
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel<bool>>(body);
            }
            catch (Exception e)
            {
                return new ResultModel<bool>()
                {
                    Status = (int)StatusType.StatusRetureData.ShowMessage,
                    Message = "ระบบเกิดความผิดพลาด"
                };
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
        private async void BackByPinMethod()
        {
            if(checkPin == (int)StatusType.PinPage.SetPassword)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UserData(email));
            }
            else if(checkPin == (int)StatusType.PinPage.ReSetPaqssword)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.SetPassword, name, surname, birthDate, mobileNo, gender, email, null, null));
            }
            else if (checkPin == (int)StatusType.PinPage.SentOTPRegis || checkPin == (int)StatusType.PinPage.SentOTPLogin)
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else if (checkPin == (int)StatusType.PinPage.ChangePassword)
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else if (checkPin == (int)StatusType.PinPage.ReChangePassword)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.ChangePassword, null, null, null, null, null, email, null, null));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดตอดต่อผู้ดูแล", "ตกลง");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
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
            await Application.Current.MainPage.Navigation.PushAsync(new CheckPin((int)StatusType.PinPage.SetPassword, name, surname, birthDate, mobileNo, gender, email, null, null));
        }

        public ICommand GoToSentOtpRegis { get; set; }
        private async void SentOtpRegisMethod()
        {
            if(checkPin == (int)StatusType.PinPage.SentOTPRegis)
            {
                await GetRequestOtpForRegis();
            }
        }

        public ICommand GoToSetFingerPrint { get; set; }
        private async void SetFingerPrintMethod()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync("แสนก Fingeprint เพื่อยืนยัน");
            if (result.Authenticated)
            {
                Preferences.Set("FingerPrint", true);
                await Application.Current.MainPage.DisplayAlert("ทำรายการสำเร็จ", "เปิดระบบแสกนลายนิ้วมือ", "ตกลง");
            }
            else
            {
                Preferences.Set("FingerPrint", false);
                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ลายนิ้วมือไม่ถูกต้อง", "ตกลง");
            }
        }

        public ICommand GoToHomePage { get; set; }
        private void HomePageMethod()
        {
            Application.Current.MainPage = new NavigationPage(new TabHome());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
