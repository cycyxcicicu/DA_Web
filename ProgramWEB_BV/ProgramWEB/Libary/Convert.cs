using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
namespace ProgramWEB.Libary
{
    public class Convert<T, T1>
    {
        public static void ConvertObj(ref T obj, T1 obj1)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    PropertyInfo property1 = typeof(T1).GetProperty(property.Name);
                    if (property1 != null)
                    {
                        object value = property1.GetValue(obj1);
                        property.SetValue(obj, value);
                    }
                }
            }
        }
        public static List<T> ConvertObjs(IEnumerable<T1> list1)
        {
            List<T> resultList = new List<T>();

            foreach (T1 obj1 in list1)
            {
                T obj = Activator.CreateInstance<T>();
                ConvertObj(ref obj, obj1);
                resultList.Add(obj);
            }
            return resultList;
        }
    }
}