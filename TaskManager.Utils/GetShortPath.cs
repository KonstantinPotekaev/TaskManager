using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TaskManager.Utils
{
   
    public static class API
    {
        public const uint MAX_LEN = 256;

        /// <summary>
        /// Функция для преобразование пути
        /// </summary>
        /// <param name="path">путь до директории</param>
        /// <returns>укороченная директрия</returns>
        public static string GetShortPath(string path)
        {
            StringBuilder shortPathName = new StringBuilder((int)API.MAX_LEN);
            API.GetShortPathName(path, shortPathName, API.MAX_LEN);
            return shortPathName.ToString();
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetShortPathName(
        [MarshalAs(UnmanagedType.LPTStr)]
        string lpszLongPath,
        [MarshalAs(UnmanagedType.LPTStr)]
        StringBuilder lpszShortPath,
        uint cchBuffer);
    }
}
