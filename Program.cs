using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class Program
    {
        const int WINHeight = 30, WINWidth = 120;
        private static string currentDir = Directory.GetCurrentDirectory();
        static void Main(string[] args)
        {
            Console.Title = "Task Manager";
            Console.SetWindowSize(WINWidth, WINHeight);
            Console.SetBufferSize(WINWidth, WINHeight);

            DrawWindow(0, 0, WINWidth, 18);
            DrawWindow(0, 18, WINWidth, 8);
            UpdateConsole();
            Console.ReadKey();
        }

        static void UpdateConsole()
        {
            DrawConsole(currentDir, 0, 26, WINWidth, 3);
        }

        static void DrawConsole(string dir, int x, int y, int width, int height)
        {
            DrawWindow(x, y, width, height);
            Console.SetCursorPosition(x + 1, y + height / 2);
            Console.Write(dir + '>');
        }




        /// <summary>
        /// Функция отрисовки окна TaskManager;
        /// </summary>
        /// <param name="x">Начальная позиция по оси X</param>
        /// <param name="y">Начальная позиция по оси Y</param>
        /// <param name="width">Ширина окна</param>
        /// <param name="height">Высота окна</param>
        static void DrawWindow(int x, int y, int width, int height)
        {
            //Header
            Console.SetCursorPosition(x, y);
            Console.Write('╔');
            for (int i = 0; i < width - 2; i++)
                Console.Write('═');
            Console.Write('╗');

            //Body 
            Console.SetCursorPosition(x, y + 1);
            for (int i = 0; i < height - 2; i++)
            {
                Console.Write('║');
                for (int j = x + 1; j < x + width - 1; j++)
                {
                    Console.Write(" ");
                }
                Console.Write('║');
            }

            //Footer

            Console.Write('╚');
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write('═');
            }
            Console.Write('╝');
            Console.SetCursorPosition(x, y);

        }

    }
}








