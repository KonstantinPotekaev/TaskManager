using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManager.Utils
{
    public static class DirectoryTree
    {
        public static void DrawTree(DirectoryInfo dir, int page, int WINWidth)
        {
            StringBuilder tree = new StringBuilder();
            GetTree(tree, dir, "", true);
            ConsoleRendering.DrawWindow(0, 0, WINWidth, 25);
            (int CurrentLeft, int CurrentTop) = Cursor.GetCursorPosition();
            int pagelines = 21;
            string[] lines = tree.ToString().Split('\n');
            int pagetotal = (lines.Length + pagelines - 1) / pagelines;
            if (page > pagetotal)
                page = pagetotal;
            for (int i = (page - 1) * pagelines, cnt = 0; i < page * pagelines; i++, cnt++)
            {
                if (lines.Length - 1 > i)
                {
                    Console.SetCursorPosition(CurrentLeft + 1, CurrentTop + 1 + cnt);
                    Console.WriteLine(lines[i]);
                }
            }
            string footer = ($"╣{page} of {pagetotal}╠");
            Console.SetCursorPosition(WINWidth / 2 - footer.Length / 2, 24);
            Console.WriteLine(footer);
        }

        static void GetTree(StringBuilder tree, DirectoryInfo dir, string indent, bool LastDirectory)
        {
            tree.Append(indent);
            if (LastDirectory)
            {
                tree.Append("└─");
                indent += "  ";
            }
            else
            {
                tree.Append("├─");
                indent += "│ ";
            }

            tree.Append($"{dir.Name}\n");

            FileInfo[] subFiles = dir.GetFiles();
            for (int i = 0; i < subFiles.Length; i++)
            {
                if (i == subFiles.Length - 1)
                {
                    tree.Append($"{indent}└─{subFiles[i].Name}\n");
                }
                else
                {
                    tree.Append($"{indent}├─{subFiles[i].Name}\n");
                }
            }

            DirectoryInfo[] subDirects = dir.GetDirectories();
            for (int i = 0; i < subDirects.Length; i++)
            {
                GetTree(tree, subDirects[i], indent, i == subDirects.Length - 1);
            }
        }
    }
}
