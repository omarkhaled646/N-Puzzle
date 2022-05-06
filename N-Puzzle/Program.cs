using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace N_Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream testsFile;
            int cases;
            StreamReader reader;
            string line;
            int wrongAnswers;
            TextReader origConsole = Console.In;
            Console.WriteLine("N-Puzzle:\n[1] Sample test cases\n[2] Complete testing\n");
            Console.Write("\nEnter your choice [1-2]: ");
            char choice = (char)Console.ReadLine()[0];
            switch(choice)
            {
                case '1':
                    #region SAMPLE CASES
                    testsFile = new FileStream("in-out.txt", FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(testsFile);
                    line = reader.ReadLine();
                    cases = int.Parse(line);
                    wrongAnswers = 0;
                    for(int Case = 0; Case < cases; Case++)
                    {
                        line = reader.ReadLine();
                        int size = int.Parse(line);
                        int[,] puzzle = new int[size,size];
                        for (int row = 0; row < size; row++)
                        {
                            line = reader.ReadLine();
                            string[] temp = line.Split(' ');
                            for (int col = 0; col < size; col++)
                            {
                                puzzle[row, col] = int.Parse(temp[col]);

                            }
                        }
                        line = reader.ReadLine();
                        int receivedAnswer = N_Puzzle.solvePuzzle(size,puzzle), expectedAnswer = int.Parse(line);
                        if (receivedAnswer != expectedAnswer)
                        {
                            Console.WriteLine("wrong answer at case " + (Case + 1) + " expected answer = " + expectedAnswer + " , received answer= " + receivedAnswer);
                            wrongAnswers++;
                            return;
                        }
                    }
                    reader.Close();
                    testsFile.Close();
                    if (wrongAnswers == 0)
                        Console.WriteLine("Congratulation");

                    Console.WriteLine("Sample cases run successfully. you should run Complete Testing... ");
                    Console.Write("Do you want to run Complete Testing now (y/n)? ");
                    choice = (char)Console.Read();
                    if (choice == 'n' || choice == 'N')
                        break;
                    else if (choice == 'y' || choice == 'Y')
                        goto CompleteTest;
                    else
                    {
                        Console.WriteLine("Invalid Choice!");
                        break;
                    }
                    break;
                #endregion

                case '2':
                    #region CompleteCases
                    CompleteTest:
                    break;
                    #endregion
            }

        }
    }
}
