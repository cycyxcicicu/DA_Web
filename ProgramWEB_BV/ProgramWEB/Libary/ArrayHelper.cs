using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Libary
{
    public class ArrayHelper
    {
        public static string noiMang(string[]arr, string nganCach)
        {
            string result = arr[0];
            for(int i = 1; i < arr.Length; i++)
            {
                result += nganCach + arr[i];
            }
            return result;
        }
    }
}