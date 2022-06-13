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

            ConsoleRendering.DrawWindow(0, 0, WINWidth, 25);
            ConsoleRendering.DrawWindow(0, 25, WINWidth, 8);
            UpdateConsole();
            
        }

        /// <summary>
        /// Функция обновления консоли
        /// </summary>
        static void UpdateConsole()
        {
            ConsoleRendering.DrawConsole(0, 33, WINWidth, 3);
            ProcessEnterCommands(WINWidth);
        }
        
        /// <summary>
        /// Функция ввода команд в консоль
        /// </summary>
        /// <param name="width">ширина строки</param>
        static void ProcessEnterCommands(int width)
        {
            (int left, int top) = Cursor.GetCursorPosition();
            
            StringBuilder command = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            char key;
            
            do
            {
                (int CurrentLeft, int CurrentTop) = Cursor.GetCursorPosition();
                keyInfo = Console.ReadKey();
                key = keyInfo.KeyChar;
                if (keyInfo.Key != ConsoleKey.Enter &&
                    keyInfo.Key != ConsoleKey.Backspace &&
                    keyInfo.Key != ConsoleKey.UpArrow &&
                    keyInfo.Key != ConsoleKey.DownArrow)
                    command.Append(key);
                else if (keyInfo.Key != ConsoleKey.Backspace)
                    Console.SetCursorPosition(CurrentLeft, CurrentTop);
                (CurrentLeft, CurrentTop) = Cursor.GetCursorPosition();

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

                /*if (keyInfo.Key == ConsoleKey.UpArrow && CommandsHistory.Capacity > 0)
                {
                    StringBuilder temp = command;
                    command.Clear();
                    command.Append(CommandsHistory[CommandsHistory.Count - 1].ToString());
                    
                    UpdateConsole();
                    
                }*/
            }while (keyInfo.Key != ConsoleKey.Enter);
            ParseCommand(command.ToString());

        }

        /// <summary>
        /// Функция для обработки команд
        /// </summary>
        /// <param name="command">команда</param>
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
                                //CommandsHistory.Add(command);

                            }
                        }
                        break;
                    case "ls":
                        if (CommandParams.Length > 1 && Directory.Exists(CommandParams[1]))
                        {
                            if (CommandParams.Length > 3 && CommandParams[2] == "-p" && int.TryParse(CommandParams[3], out int n))
                            {
                                DirectoryTree.DrawTree(new DirectoryInfo(CommandParams[1]), n, WINWidth);
                            }
                            else
                            {
                                DirectoryTree.DrawTree(new DirectoryInfo(CommandParams[1]), 1, WINWidth);
                            }
                        }
                        break;
                    case "cp":
                        if (CommandParams.Length > 2)
                        {
                            Copy.CopyDirectory(CommandParams[1], CommandParams[2], true);
                        }
                        break;
                }
            }
            UpdateConsole();
        }
    }
}








