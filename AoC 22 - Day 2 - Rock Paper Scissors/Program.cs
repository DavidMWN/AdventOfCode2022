using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC22Day2_RockPaperScissors
{
    class Program
    {
        private enum Throw
        {
            Rock,
            Paper,
            Scissors,
            Default
        }

        private enum Result
        {
            Win,
            Lose,
            Draw,
            Default
        }

        static void Main(string[] args)
        {
            // Part 1

            int totalScoreOne = 0;

            string filePath = "../../../RPS_Guide.txt";

            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();

            foreach (string l in lines)
            {
                totalScoreOne += GetRockPaperScissorsResult(l[0], l[2]);
            }

            Console.WriteLine(totalScoreOne);

            // Part 2

            int totalScoreTwo = 0;

            foreach (string l in lines)
            {
                totalScoreTwo += GetRockPaperScissorsThrow(l[0], l[2]);
            }

            Console.WriteLine(totalScoreTwo);
        }

        static int GetRockPaperScissorsResult(char opponentThrow, char yourThrow)
        {
            Throw opponent = Throw.Default;
            Throw you = Throw.Default;
            Result result = Result.Default;

            switch (opponentThrow)
            {
                case 'A':
                    opponent = Throw.Rock;
                    break;
                case 'B':
                    opponent = Throw.Paper;
                    break;
                case 'C':
                    opponent = Throw.Scissors;
                    break;
            }

            switch (yourThrow)
            {
                case 'X':
                    you = Throw.Rock;
                    break;
                case 'Y':
                    you = Throw.Paper;
                    break;
                case 'Z':
                    you = Throw.Scissors;
                    break;
            }

            if (opponent == you)
                result = Result.Draw;
            else
            {
                switch (opponent)
                {
                    case Throw.Rock:
                        {
                            switch (you)
                            {
                                case Throw.Paper:
                                    result = Result.Win;
                                    break;
                                case Throw.Scissors:
                                    result = Result.Lose;
                                    break;
                            }
                            break;
                        }
                    case Throw.Paper:
                        {
                            switch (you)
                            {
                                case Throw.Rock:
                                    result = Result.Lose;
                                    break;
                                case Throw.Scissors:
                                    result = Result.Win;
                                    break;
                            }
                            break;
                        }
                    case Throw.Scissors:
                        {
                            switch (you)
                            {
                                case Throw.Rock:
                                    result = Result.Win;
                                    break;
                                case Throw.Paper:
                                    result = Result.Lose;
                                    break;
                            }
                            break;
                        }
                }
            }

            return RockPaperScissorsScore(you, result);
        }

        static int GetRockPaperScissorsThrow(char opponentThrow, char resultNeeded)
        {
            Throw opponent = Throw.Default;
            Throw you = Throw.Default;
            Result result = Result.Default;

            switch (opponentThrow)
            {
                case 'A':
                    opponent = Throw.Rock;
                    break;
                case 'B':
                    opponent = Throw.Paper;
                    break;
                case 'C':
                    opponent = Throw.Scissors;
                    break;
            }

            switch (resultNeeded)
            {
                case 'X':
                    result = Result.Lose;
                    break;
                case 'Y':
                    result = Result.Draw;
                    break;
                case 'Z':
                    result = Result.Win;
                    break;
            }

            switch (result)
            {
                case Result.Draw:
                    you = opponent;
                    break;
                case Result.Win:
                    switch (opponent)
                    {
                        case Throw.Rock:
                            you = Throw.Paper;
                            break;
                        case Throw.Paper:
                            you = Throw.Scissors;
                            break;
                        case Throw.Scissors:
                            you = Throw.Rock;
                            break;
                    }
                    break;
                case Result.Lose:
                    switch (opponent)
                    {
                        case Throw.Rock:
                            you = Throw.Scissors;
                            break;
                        case Throw.Paper:
                            you = Throw.Rock;
                            break;
                        case Throw.Scissors:
                            you = Throw.Paper;
                            break;
                    }
                    break;
            }

            return RockPaperScissorsScore(you, result);
        }

        static int RockPaperScissorsScore(Throw you, Result result)
        {
            int score = 0;

            switch (you)
            {
                case Throw.Rock:
                    score += 1;
                    break;
                case Throw.Paper:
                    score += 2;
                    break;
                case Throw.Scissors:
                    score += 3;
                    break;
            }

            switch (result)
            {
                case Result.Lose:
                    score += 0;
                    break;
                case Result.Draw:
                    score += 3;
                    break;
                case Result.Win:
                    score += 6;
                    break;
            }

            return score;
        }
    }
}
