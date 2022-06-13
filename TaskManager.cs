using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Utils;

namespace TaskManager
{
    internal class Program
    {

        const int WINHeight = 40, WINWidth = 130;
        private static string currentDir = Directory.GetCurrentDirectory();
        static List<string> CommandsHistory = new List<string>(0);

        static void Main(string[] args)
        {
            Console.Title = "Task Manager";
            Console.SetWindowSize(WINWidth, WINHeight);
            Console.SetBufferSize(WINWidth, WINHeight);

            DrawWindow(0, 0, WINWidth, 25);
            DrawWindow(0, 25, WINWidth, 8);
            UpdateConsole();
            string s = Console.ReadLine();
        }
        /// <summary>
        /// Функция обновления консоли
        /// </summary>
        static void UpdateConsole()
        {
            DrawConsole(currentDir, 0, 33, WINWidth, 3);
            ProcessEnterCommands(WINWidth);
        }
        
        /// <summary>
        /// Функция ввода команд в консоль
        /// </summary>
        /// <param name="width">ширина строки</param>
        static void ProcessEnterCommands(int width)
        {
            (int left, int top) = GetCursorPosition();
            StringBuilder command = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            char key;
            int k = 1;
            do
            {
                keyInfo = Console.ReadKey();
                key = keyInfo.KeyChar;
                if (keyInfo.Key != ConsoleKey.Enter &&
                    keyInfo.Key != ConsoleKey.Backspace &&
                    keyInfo.Key != ConsoleKey.UpArrow &&
                    keyInfo.Key != ConsoleKey.DownArrow)
                {
                    command.Append(key);
                }
                (int CurrentLeft, int CurrentTop) = GetCursorPosition();

                if (CurrentLeft == width - 2)
                {
                    Console.SetCursorPosition(CurrentLeft - 1, top);
                    Console.Write(" ");
                    Console.SetCursorPosition(CurrentLeft - 1, top);
                }

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    
                    if (command.Length > 0)
                        command.Remove(command.Length - 1, 1);
                    if (CurrentLeft >= left)
                    {
                        Console.SetCursorPosition(CurrentLeft, top);
                        Console.Write(" ");
                        Console.SetCursorPosition(CurrentLeft, top);
                    }
                    else
                    {
                        command.Clear();
                        Console.SetCursorPosition(left, top);
                    }
                }

                if (keyInfo.Key == ConsoleKey.UpArrow && CommandsHistory.Capacity > 0)
                {
                    StringBuilder temp = command;
                    command.Clear();
                    command.Append(CommandsHistory[CommandsHistory.Count - k].ToString());
                    k++;
                    UpdateConsole();
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            ParseCommand(command.ToString());
            k = 1;
        }

        static void ParseCommand(string command)
        {
            string[] CommandParams = command.ToLower().Split(' ');
            if (CommandParams.Length > 0)
            {
                switch (CommandParams[0])
                {
                    case "cd":
                        if (CommandParams.Length > 1)
                        {
                            if (Directory.Exists(CommandParams[1]))
                            {
                                currentDir = CommandParams[1];
                                CommandsHistory.Add(command);

                            }
                        }
                        break;

                }
            }
            UpdateConsole(); 
        }
        static (int left, int top) GetCursorPosition()
        {
            return (Console.CursorLeft, Console.CursorTop);
        }

       

        static void DrawConsole(string dir, int x, int y, int width, int height)
        {
            DrawWindow(x, y, width, height);
            Console.SetCursorPosition(x + 1, y + height / 2);
            Console.Write(GetShortPath(dir) + '>');
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

        /// <summary>
        /// Функция для преобразование пути
        /// </summary>
        /// <param name="path">путь до директории</param>
        /// <returns>укороченная директрия</returns>
        static string GetShortPath(string path) 
        {
            StringBuilder shortPathName = new StringBuilder((int)API.MAX_LEN);
            API.GetShortPathName(path, shortPathName, API.MAX_LEN);
            return shortPathName.ToString();
        }


    }
}








