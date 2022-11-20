using System;

namespace WordleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            string strMenu = "1)New Game  2)Help  3)Dictionary  Esc)Exit Game";
            strMenu.PrintMenu();
            do
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new clsGame(clsDictionary.RandoWord());
                        strMenu.PrintMenu();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        strMenu.PrintMenu();
                        Help();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        clsDictionary.Edit();
                        strMenu.PrintMenu();
                        break;

                    case ConsoleKey.Escape:
                        return;

                    default:
                        strMenu.PrintMenu("Select valid option");
                        break;
                }
            } while (true);
        }
        private static void Help()
        {
            "How To Play".WriteLine(ConsoleColor.Blue);
            Console.WriteLine();
            "Guess the Wordle in 6 tries.".WriteLine(ConsoleColor.White);
            Console.WriteLine();
            "Each guess must be a valid 5-letter word.".WriteLine(ConsoleColor.White);
            "The color of the tiles will change to show how close your guess was to the word.".WriteLine(ConsoleColor.White);
            Console.WriteLine();
            "Examples".WriteLine(ConsoleColor.White);
            "W is in the word and in the correct spot.".WriteLine(ConsoleColor.White);
            "W".Write(ConsoleColor.Green);
            "EARY".Write(ConsoleColor.White);
            Console.WriteLine();
            "I is in the word but in the wrong spot.".WriteLine(ConsoleColor.White);
            "P".Write(ConsoleColor.White);
            "I".Write(ConsoleColor.DarkYellow);
            "LLS".Write(ConsoleColor.White);
            Console.WriteLine();
            "When the letter is white, it does not correspond to the word.".WriteLine(ConsoleColor.White);
            "VAGUE".Write(ConsoleColor.White);
        }
    }
}
