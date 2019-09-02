using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRateWallet.ViewModels
{
    public class PaymentViewModels : INotifyPropertyChanged
    {
        public PaymentViewModels()
        {
            GoToScanQrCode = new Command<string>(ScanQrCodeMethod);
            qrText = default;
        }

        private string qrText;
        public string QrText
        {
            get
            {
                return qrText;
            }
            set
            {
                qrText = value;
                OnPropertyChanged(nameof(QrText));
            }
        }


        public ICommand GoToScanQrCode { get; set; }
        private async void ScanQrCodeMethod(string type)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    qrText = result;
                    if (Int32.Parse(type) == (int)StatusType.UserType.Merchant)
                    {
                        if(Int32.Parse(qrText.Substring(0, 1)) == (int)StatusType.UserType.Merchant)
                        {

                        }
                        else
                        {
                            if (Int32.Parse(qrText.Substring(0, 1)) == (int)StatusType.UserType.Admin)
                            {
                                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "โปรดเปลี่ยนโหมดแสกน", "ตกลง");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบไม่สามารถสแกน QR Code นี้ได้", "ตกลง");
                            }
                        }
                    }
                    else if (Int32.Parse(type) == (int)StatusType.UserType.Admin)
                    {
                        if (Int32.Parse(qrText.Substring(0, 1)) == (int)StatusType.UserType.Admin)
                        {

                        }
                        else
                        {
                            if (Int32.Parse(qrText.Substring(0, 1)) == (int)StatusType.UserType.Merchant)
                            {
                                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "โปรดเปลี่ยนโหมดแสกน", "ตกลง");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบไม่สามารถสแกน QR Code นี้ได้", "ตกลง");
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "ระบบเกิดความผิดพลาดโปรดติดต่อผู้ดูแล", "ตกลง");
                    }
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("เกิดความผิดพลาด!!!", "อุปกรณ์ของท่านไม่สามารถใฃ้แอพพลิเคชั่นนี้ได้", "ตกลง");
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
