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
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetShortPathName(
        [MarshalAs(UnmanagedType.LPTStr)]
        string lpszLongPath,
        [MarshalAs(UnmanagedType.LPTStr)]
        StringBuilder lpszShortPath,
        uint cchBuffer);
    }
}
