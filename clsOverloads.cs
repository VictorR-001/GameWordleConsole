using System;
using System.Collections.Generic;
using System.Text;

namespace WordleConsole
{
    /// <summary>
    /// Class that I use to create custom Methods, to classes to the framework's own classes
    /// </summary>
    static class clsOverloads
    {
        public static void Write(this string Text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(Text, Console.ForegroundColor);
        }
        public static void WriteLine(this string Text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(Text, Console.ForegroundColor);
        }
        public static void PrintMenu(this string strMenu, string strError = "")
        {
            Console.Clear();
            strMenu.WriteLine(ConsoleColor.White);
            new string('_', strMenu.Length).WriteLine(ConsoleColor.White);
            if (!string.IsNullOrWhiteSpace(strError))
                strError.WriteLine(ConsoleColor.DarkYellow);
        }
    }
}
