using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MangoAppsLoginApi.ViewModel
{
    public static class Common
    {

        public static bool IsValidLoginId(string InputEmail)
        {
            //Regex To validate Email Address
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static string EncodeToBase64(string password)
        {
            byte[] encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            string returnValue = System.Convert.ToBase64String(encodeAsBytes);
            return returnValue;
        }
    }
}

