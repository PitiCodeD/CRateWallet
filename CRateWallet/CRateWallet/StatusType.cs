using System;
using System.Collections.Generic;
using System.Text;

namespace CRateWallet
{
    static class StatusType
    {
        public enum UserType
        {
            Null,
            Customer,
            Merchant,
            Admin
        }

        public enum StatusRetureData
        {
            Null,
            Success,
            ShowMessage,
            NotShowMessage,
            BackToFirstPage,
            SecondLogin
        }

        public enum PinPage
        {
            Null,
            SetPassword,
            ReSetPaqssword,
            SentOTPRegis,
            SentOTPLogin,
            ChangePassword,
            ReChangePassword
        }
    }
}
