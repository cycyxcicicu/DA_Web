using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Libary
{
    public class Validate
    {
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

        public static string to2(int x)
        {
            if (x >= 10)
                return x.ToString();
            if (x < 0)
                return "00";
            return "0" + x;

        }

        public static string toDayString(DateTime date)
        {
            if (date == null || date.Year < 1900)
                return "";
            return to2(date.Day) + "-" + to2(date.Month) + "-" + date.Year;
        }

        public static string toDayInput(string date)
        {
            if (string.IsNullOrEmpty(date) || date.Length != 10)
                return "";
            return date.Substring(6, 10) + "-" + date.Substring(3, 5) + "-" + date.Substring(0, 2);
        }
    }
}