using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJohnson_CodingProblemEmpowr
{
    static class PuzzleSolver
    {
        public static string[] DICTIONARY = { "OX", "CAT", "TOY", "AT", "DOG", "CATAPULT", "T" };

        public static int FindWords(char[,] puzzle)
        {
            List<string> WordsFound = new List<string>();

            int Height = puzzle.GetLength(0);
            int Width = puzzle.GetLength(1);
            string GoingRight = "", GoingDown = "", GoingDownRight = "", GoingDownLeft = "";
            int TrackingRight = 0, TrackingDown = 0, TrackingLeft = 0;
            bool TrackingRightComplete = false, TrackingDownComplete = false, TrackingDownRightComplete = false, TrackingDownLeftComplete = false;


            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    CheckAddWord(WordsFound, puzzle[i, j].ToString());

                    GoingRight = GoingDown = GoingDownRight = GoingDownLeft = puzzle[i, j].ToString();
                    TrackingRightComplete = TrackingDownComplete = TrackingDownRightComplete = TrackingDownLeftComplete = false;

                    TrackingRight = j + 1;
                    TrackingDown = i + 1;
                    TrackingLeft = j - 1;
                    while (!TrackingDownComplete || !TrackingDownRightComplete || !TrackingRightComplete || !TrackingDownLeftComplete)
                    {
                        HorizontalWords(puzzle, WordsFound, Width, ref GoingRight, TrackingRight, ref TrackingRightComplete, i);

                        VerticalWords(puzzle, WordsFound, Height, ref GoingDown, TrackingDown, ref TrackingDownComplete, j);

                        DiagonalTopLeftToBottomRight(puzzle, WordsFound, Height, Width, ref GoingDownRight, TrackingRight, TrackingDown, ref TrackingDownRightComplete);

                        DiagonalTopRightToBottomLeft(puzzle, WordsFound, Height, ref GoingDownLeft, TrackingDown, TrackingLeft, ref TrackingDownLeftComplete);

                        TrackingDown++;
                        TrackingRight++;
                        TrackingLeft--;
                    }
                }
            }
            return WordsFound.Count;
        }

        private static void DiagonalTopRightToBottomLeft(char[,] puzzle, List<string> PossibleCombination, int Height, ref string GoingDownLeft, int TrackingDown, int TrackingLeft, ref bool TrackingDownLeftComplete)
        {
            if (TrackingLeft >= 0 && TrackingDown < Height && !TrackingDownLeftComplete)
            {
                char[] tempCharArray;
                string WordReversed;

                GoingDownLeft = GoingDownLeft + puzzle[TrackingDown, TrackingLeft];
                CheckAddWord(PossibleCombination, GoingDownLeft);

                tempCharArray = GoingDownLeft.ToCharArray();
                Array.Reverse(tempCharArray);
                WordReversed = new string(tempCharArray);
                CheckAddWord(PossibleCombination, WordReversed);
            }
            else
            {
                TrackingDownLeftComplete = true;
            }
        }



        private static void DiagonalTopLeftToBottomRight(char[,] puzzle, List<string> PossibleCombination, int Height, int Width, ref string GoingDownRight, int TrackingRight, int TrackingDown, ref bool TrackingDownRightComplete)
        {
            if (TrackingRight < Width && TrackingDown < Height && !TrackingDownRightComplete)
            {
                char[] tempCharArray;
                string WordReversed;

                GoingDownRight = GoingDownRight + puzzle[TrackingDown, TrackingRight];
                CheckAddWord(PossibleCombination, GoingDownRight);

                tempCharArray = GoingDownRight.ToCharArray();
                Array.Reverse(tempCharArray);
                WordReversed = new string(tempCharArray);
                CheckAddWord(PossibleCombination, WordReversed);

            }
            else
            {
                TrackingDownRightComplete = true;
            }
        }

        private static void VerticalWords(char[,] puzzle, List<string> PossibleCombination, int Height, ref string GoingDown, int TrackingDown, ref bool TrackingDownComplete, int j)
        {
            if (TrackingDown < Height && !TrackingDownComplete)
            {
                char[] tempCharArray;
                string WordReversed;

                GoingDown = GoingDown + puzzle[TrackingDown, j];
                CheckAddWord(PossibleCombination, GoingDown);

                tempCharArray = GoingDown.ToCharArray();
                Array.Reverse(tempCharArray);
                WordReversed = new string(tempCharArray);
                CheckAddWord(PossibleCombination, WordReversed);

            }
            else
            {
                TrackingDownComplete = true;
            }
        }

        private static void HorizontalWords(char[,] puzzle, List<string> PossibleCombination, int Width, ref string GoingRight, int TrackingRight, ref bool TrackingRightComplete, int i)
        {
           
            if (TrackingRight < Width && !TrackingRightComplete)
            {
                char[] tempCharArray;
                string WordReversed;

                GoingRight = GoingRight + puzzle[i, TrackingRight];
                CheckAddWord(PossibleCombination, GoingRight);

                tempCharArray = GoingRight.ToCharArray();
                Array.Reverse(tempCharArray);
                WordReversed = new string(tempCharArray);
                CheckAddWord(PossibleCombination, WordReversed);
            }
            else
            {
                TrackingRightComplete = true;
            }

        }
        private static void CheckAddWord(List<string> PossibleCombination, string word)
        {
            if (IsWord(word))
            {
                PossibleCombination.Add(word);
            }
        }

        static bool IsWord(string testWord)
        {
            if (DICTIONARY.Contains(testWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            char[,] puzzle = new char[,] { 
                {'C','A','T'},
                {'X','Z','T'},
                {'Y','O','T'}
            };
            System.Console.WriteLine("Output: " + FindWords(puzzle));
            Console.Read();
        }
    }
}
//            {'C','A','T','A','P','U','L','T'},
//            {'X','Z','T','T','O','Y','O','O'},
//            {'Y','O','T','O','X','T','X','X'}