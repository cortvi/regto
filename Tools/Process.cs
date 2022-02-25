using System;
using System.IO;
using System.Threading;
using regto.Tools;

namespace regto
{
    internal static class Process
    {
        private static bool restartFlag = false;
        private static string[] fileLines = null;

        internal static bool Ask ()
        {
            Console.Write("\n~> Enter a command: ");
            string[] command = Console.ReadLine().Split(' ');
            switch (command[0])
            {
                case "start":
                    do Start(command[1]);
                    while (restartFlag);
                    restartFlag = false;
                    fileLines = null;
                    break;

                case "move":
                    MoveTo(command);
                    break;

                case "click":
                    Cursor.Click();
                    break;

                case "exit": return true;
                default:
                    Console.WriteLine("Wrong command, try again.");
                    break;
            }
            return false;
        }

        static void Start (string file)
        {
            fileLines = fileLines ?? File.ReadAllLines(file);
            foreach (string line in fileLines)
            {
                if ( GetCommand(line, out string[] command) )
                {
                    Console.WriteLine("Command → " + string.Join(' ', command) );
                    Execute(command);
                }
            }
        }

        static void MoveTo (string[] command)
        {
            int x = int.Parse(command[1]);
            int y = int.Parse(command[2]);
            Cursor.SetPosition(x, y);
        }

        static void Sleep (string[] command)
        {
            int time = int.Parse(command[1]);
            Thread.Sleep(time);
        }

        static void Execute (string[] command)
        {
            switch (command[0])
            {
                case "move":
                    MoveTo(command);
                    break;

                case "click":
                    Cursor.Click();
                    break;

                case "wait":
                    Sleep(command);
                    break;

                case "restart":
                    restartFlag = true;
                    break;

                default:
                    Console.WriteLine("Wrong command! → " + command[0]);
                    break;
            }
        }

        static bool GetCommand (string line, out string[] command)
        {
            string trimmedLine = line.Trim();
            if (!string.IsNullOrEmpty(trimmedLine) && !trimmedLine.StartsWith('#') && !trimmedLine.StartsWith("//"))
            {
                command = trimmedLine.Split(new string[] {"#", "//"}, System.StringSplitOptions.RemoveEmptyEntries)
                            [0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return true;
            }
            else
            {
                command = null;
                return false;
            }
        }
    }
}
