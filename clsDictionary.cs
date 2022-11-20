using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WordleConsole
{
    /// <summary>
    /// Static class where the dictionary and its functionalities are stored.
    /// </summary>
    static class clsDictionary
    {
        /// <summary>
        /// List of words
        /// </summary>
        private static List<string> Words = ConfigurationManager.AppSettings["Words"] != null ? ConfigurationManager.AppSettings["Words"].Split(',').ToList() : new List<string>();
        /// <summary>
        /// Object that random
        /// </summary>
        private static readonly Random rdm = new Random((int)DateTime.Now.Ticks);
        private static readonly string strMenu = "1)New Word  2)Show Words  Enter)Save  Esc)Back";
        /// <summary>
        /// Gets a random word
        /// </summary>
        /// <returns></returns>
        public static string RandoWord() => Words.Count > 0 ? Words.ElementAt(rdm.Next(Words.Count)).ToUpper() : "";
        /// <summary>
        /// Display word list
        /// </summary>
        private static void ShowWords()
        {
            string FirstLetter = "";
            $"Total number of words I know {Words.Count()}".WriteLine(ConsoleColor.White);
            foreach (var w in Words.OrderBy(w => w).ToList())
            {
                if (!FirstLetter.Equals(w[0].ToString().ToUpper()))
                {
                    FirstLetter = w[0].ToString().ToUpper();
                    FirstLetter.WriteLine(ConsoleColor.Blue);
                }
                w.ToUpper().WriteLine(ConsoleColor.White);
            }
            Console.SetWindowPosition(0, 0);
        }
        /// <summary>
        /// We add a new word to the dictionary
        /// </summary>
        /// <param name="Word">New word</param>
        /// <returns>Returns true if the word is saved otherwise false</returns>
        private static void AddWord(string Word)
        {
            if (Contains(Word))
            {
                strMenu.PrintMenu($"The word \"{Word.ToString().ToUpper()}\" already exists");
                return;
            }
            else if (Word.Length != 5)
            {
                strMenu.PrintMenu($"The word \"{Word.ToString().ToUpper()}\"  is not 5 characters long");
                return;
            }

            Words.Add(Word.ToUpper());
            if (ConfigurationManager.AppSettings["Words"] != null)
                ConfigurationManager.AppSettings["Words"] = string.Join(',', Words);
            else
                ConfigurationManager.AppSettings["Words"] = string.Join(',', Words);

            Console.WriteLine();
            $"The word \"{Word.ToString().ToUpper()}\" was successfully added.".WriteLine(ConsoleColor.Green);
            "='w'=".WriteLine(ConsoleColor.Green);
        }
        /// <summary>
        /// Displays the menu, to edit the dictionary.
        /// </summary>
        public static void Edit()
        {
            StringBuilder AuxWord = new StringBuilder();
            strMenu.PrintMenu();
            do
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        strMenu.PrintMenu();
                        AuxWord = new StringBuilder();
                        break;

                    //We show the words   
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        strMenu.PrintMenu();
                        ShowWords();
                        break;

                    //It takes us out of the dictionary edit menu.
                    case ConsoleKey.Escape:
                        return;

                    //delete the last valid character
                    case ConsoleKey.Backspace:
                        if (AuxWord.Length > 0)
                        {
                            AuxWord.Remove(AuxWord.Length - 1, 1);
                            strMenu.PrintMenu();
                            AuxWord.ToString().Write(ConsoleColor.White);
                        }
                        break;

                    //Add a valid character
                    case ConsoleKey k when (k >= ConsoleKey.A && k <= ConsoleKey.Z):
                        AuxWord.Append(k.ToString());
                        k.ToString().Write(ConsoleColor.White);
                        break;

                    //Save the word
                    case ConsoleKey.Enter:
                        AddWord(AuxWord.ToString());
                        AuxWord = new StringBuilder();
                        break;
                }
            } while (true);
        }
        /// <summary>
        /// look up if the word is contained in the dictionary
        /// </summary>
        /// <param name="Word">string Word </param>
        /// <returns>True in case it contains the word</returns>
        public static bool Contains(string Word) => Words.Count(w => w.ToUpper().Equals(Word.ToUpper())).Equals(1);
    }
}
