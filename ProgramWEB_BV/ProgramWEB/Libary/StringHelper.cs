using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ProgramWEB.Libary
{
    public class StringHelper
    {
        public static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static bool IsPhoneNbr(string number)
        {
            string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch { }
            return false;
        }
        public static bool IsValidCCCD(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }

            foreach (var c in id)
            {
                if (c > '9' || c < '0')
                    return false;
            }

            int maTinh = int.Parse(id.Substring(0, 3));
            if (maTinh < 1 || maTinh > 96)
                return false;

            int gioiTinh = int.Parse(id.Substring(3, 1));
            if (gioiTinh != 2 && gioiTinh != 3)
                return false;

            return true;       
        }
        public static string toDateView(DateTime date)
        {
            if (date == null)
                return string.Empty;
            return to2(date.Day.ToString()) + "/" + to2(date.Month.ToString()) + "/" + to2(date.Year.ToString());
        }
        public static string toDateTimeView(DateTime date)
        {
            if (date == null)
                return string.Empty;
            return to2(date.Hour.ToString()) + ":" + to2(date.Minute.ToString()) + ":" + to2(date.Second.ToString()) + " " + to2(date.Day.ToString()) + "/" + to2(date.Month.ToString()) + "/" + to2(date.Year.ToString());
        }
        public static string to2(string s)
        {
            if (s.Length > 1)
                return s;
            return "0" + s;
        }
        public static string boKhoangTrang(string str)
        {
            for (int i = 0; i < str.Length;)
            {
                if (str[i] == ' ')
                {
                    if (i == str.Length - 1)
                        return str.Substring(0, i - 1);
                    str = str.Substring(0, i) + str.Substring(i + 1, str.Length - i - 1);
                }
                else i++;
            }
            return str;
        }

        public static string NonUnicode(string text)
        {
            string[] arr1 = new string[] {
                "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ","é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string TaoMaXacThuc()
        {
            string result = "";
            Random ran = new Random();
            for (int i = 0; i < 6; i++)
                result += ran.Next(0, 10);
            return result;
        }
    }
}