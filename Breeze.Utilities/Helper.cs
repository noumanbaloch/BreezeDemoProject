using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Utilities
{
    public static class Helper
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public static bool IsNullOrEmpty<T>(T obj)
        {
            return obj is null;
        }
    }

    public static class StoreProcedureNames
    {
        public static string GetUserStoreProcedure = "GJS_PROC_Fetch_User";
    }
}
