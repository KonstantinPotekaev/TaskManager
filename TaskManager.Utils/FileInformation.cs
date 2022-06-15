using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Utils
{
    public static class FileInformation
    {
        private static string[] FileGetInfo(string FileName)
        {
            string[] info = new string[5];
            
            return info;
        }
        public static void Print(string FileName)
        {

            for (int i = 27; i < 31; i++)
            {
                for (int j = 1; j < 128; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write()
                }
            }
        }
    }
}
