using System;
using System.Collections.Generic;
using System.Text;

namespace NapsterMobileDevelopment.Services
{
    public static class NavigationDataService
    {

        public static Dictionary<string, object?> Data = new Dictionary<string, object?>();


        public static void Add(string key, object? value)
        {

            Data.Add(key, value);

        }

        public static void Clear() { Data.Clear(); }

        public static object Get(string key=null)
        {
            if (key == null)
            {
                return Data;
            }
            else
            {
                return Data[key];
            }
        }

    }
}
