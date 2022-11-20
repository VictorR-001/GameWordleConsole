using System;
using System.Collections.Generic;
using System.Text;

namespace WordleConsole
{
    /// <summary>
    /// Class controlling the game
    /// </summary>
    class clsGame
    {
        /// <summary>
        /// Class that helps me to paint the letters of the corresponding color.
        /// </summary>
        private class clsLetter
        {
            public char Letter { get; set; } = ' ';
            public ConsoleColor Color { get; set; } = ConsoleColor.White;
            public clsLetter(char l) => Letter = l;
            /// <summary>
            /// Print the letter
            /// </summary>
            public void Print() => Letter.ToString().Write(Color);
        }
        public clsGame(string Word)
        {
            if (!string.IsNullOrWhiteSpace(Word))
                Play(Word);
            else
            {
                Console.Clear();
                "There are no words to play with :(".WriteLine(ConsoleColor.Red);
                "Press any key to return to the menu".WriteLine(ConsoleColor.Red);
                "Please add words in the menu 3)Dictionary".WriteLine(ConsoleColor.Red);
                Console.ReadKey();
            }
        }
        /// <summary>
        /// game control
        /// </summary>
        public void Play(string word)
        {
            //It is an active game
            bool playGame = true;
            int Tries = 1;
            //List of results of previous attempts
            List<(List<clsLetter>, int)> lstTriesWord = new List<(List<clsLetter>, int)>();
            PrintMenu(Tries, word, lstTriesWord);
            do
            {
                StringBuilder inputWord = new StringBuilder();
                do
                {
                    ConsoleKeyInfo inputKey = Console.ReadKey(true);
                    switch (inputKey.Key)
                    {
                        //New game
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            playGame = true;
                            Tries = 1;
                            string newWord = "";
                            inputWord = new StringBuilder();
                            do
                            {
                                newWord = clsDictionary.RandoWord();
                            } while (newWord.Equals(word));
                            word = newWord;
                            lstTriesWord = new List<(List<clsLetter>, int)>();
                            PrintMenu(Tries, word, lstTriesWord);
                            break;

                        //It takes us out of the game menu.
                        case ConsoleKey.Escape:
                            return;

                        //delete the last valid character
                        case ConsoleKey.Backspace:
                            if (playGame && inputWord.Length > 0)
                            {
                                inputWord.Remove(inputWord.Length - 1, 1);
                                PrintMenu(Tries, word, lstTriesWord);
                                inputWord.ToString().Write(ConsoleColor.White);
                            }
                            break;

                        //Add a valid character
                        case ConsoleKey k when (playGame && k >= ConsoleKey.A && k <= ConsoleKey.Z):
                            inputWord.Append(k.ToString());
                            k.ToString().Write(ConsoleColor.White);
                            break;
                    }
                } while (inputWord.Length < 5);

                //We keep the word that was captured
                List<clsLetter> lstLetters = new List<clsLetter>();
                foreach (var l in inputWord.ToString())
                    lstLetters.Add(new clsLetter(l));

                //Validate that the word exists in the dictionary
                if (clsDictionary.Contains(inputWord.ToString()) == false)
                {
                    string sorry = $" Sorry, I don't know the word \"{inputWord.ToString()}\"";
                    foreach (var l in sorry)
                        lstLetters.Add(new clsLetter(l) { Color = ConsoleColor.DarkMagenta });
                    lstTriesWord.Add((lstLetters, Tries));
                    sorry.WriteLine(ConsoleColor.DarkMagenta);
                    $"Try {Tries}: ".Write(ConsoleColor.White);
                }
                else
                {
                    Console.WriteLine();
                    //We validate the word to know if it is the one we are looking for.
                    if (ValidateWord(word, lstLetters))
                    {
                        $"You won the word was \"{word}\"".WriteLine(ConsoleColor.Green);
                        playGame = false;
                    }
                    //in the event that the attempts run out
                    else if (Tries >= 6)
                    {
                        "You lost :( ".WriteLine(ConsoleColor.DarkRed);
                        "The word was : ".Write(ConsoleColor.DarkRed);
                        word.WriteLine(ConsoleColor.White);
                        playGame = false;
                    }
                    //we prepare for the capture of the following word
                    else
                    {
                        lstTriesWord.Add((lstLetters, Tries));
                        Tries++;
                        PrintMenu(Tries, word, lstTriesWord);
                        playGame = Tries <= 6;
                    }
                }
            } while (true);
        }
        /// <summary>
        /// Print the menu
        /// </summary>
        /// <param name="iTries">current Trie</param>
        /// <param name="word">word</param>
        /// <param name="lstTriesWord">List of Tries</param>
        private static void PrintMenu(int iTries, string word, List<(List<clsLetter> word, int Try)> lstTriesWord)
        {
            "1)New Game  Esc)Back".PrintMenu();
            //word.WriteLine(ConsoleColor.Red);//view word
            foreach (var item in lstTriesWord)
            {
                $"Try {item.Try}: ".Write(ConsoleColor.White);
                item.word.ForEach(l => l.Print());
                Console.WriteLine();
            }
            $"Try {iTries}: ".Write(ConsoleColor.White);
        }
        private void InitializeGame(out int iTries, out string word, out List<clsLetter> lstLetters, out StringBuilder AuxWord)
        {
            iTries = 1;
            word = clsDictionary.RandoWord();
            lstLetters = new List<clsLetter>();
            AuxWord = new StringBuilder();
        }
        private bool ValidateWord(string word, List<clsLetter> lstLetters)
        {
            //We paint in green the letters that are in the correct position.
            for (int i = 4; i >= 0; i--)
            {
                if (lstLetters[i].Letter.Equals(word[i]))
                {
                    lstLetters[i].Color = ConsoleColor.Green;
                    //we remove the letters that if we consider
                    word = word.Remove(i, 1);
                }
            }
            //if there are no more letters, we return true, since the word is the one we are looking for.
            if (word.Length <= 0)
                return true;

            //if there are no more letters, we return true, since the word is the one we are looking for.
            foreach (var l in lstLetters.FindAll(l => l.Color.Equals(ConsoleColor.White)))
            {
                //in case the word contains the letter we paint it dark yellow and remove the letter from the word
                if (word.Contains(l.Letter))
                {
                    l.Color = ConsoleColor.DarkYellow;
                    word = word.Remove(word.IndexOf(l.Letter), 1);
                }
            }
            return false;
        }
    }
}
