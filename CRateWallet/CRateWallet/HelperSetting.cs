using System;
using System.Collections.Generic;
using System.Text;

namespace CRateWallet
{
    public class HelperSetting
    {
        private string host = "http://192.168.1.20:6000/walllet/";
        public string Host
        {
            get { return host; }
        }

        private string checkToken = "CRateWallet";
        public string CheckToken
        {
            get { return checkToken; }
        }
    }
}
