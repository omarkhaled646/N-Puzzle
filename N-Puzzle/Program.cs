using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Priority_Queue;

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
            TextReader origConsole = Console.In;
            Console.WriteLine("N-Puzzle:\n[1] Sample test cases\n[2] Complete testing for both\n[3] complete testing for Manhattan only\n");
            Console.Write("\nEnter your choice [1-2-3]: ");
            char choice = (char)Console.ReadLine()[0];

            switch (choice)
            {
                case '1':
                    #region SAMPLE CASES
                    testsFile = new FileStream("sampleTests.txt", FileMode.Open, FileAccess.Read);

                    reader = new StreamReader(testsFile);
                    line = reader.ReadLine();
                    cases = int.Parse(line);
                    for (int Case = 0; Case < cases; Case++)
                    {
                        line = reader.ReadLine();
                        int size = int.Parse(line);
                        int[,] puzzle = new int[size, size];
                        for (int row = 0; row < size; row++)
                        {
                            line = reader.ReadLine();
                            string[] rowNumbers = line.Split(' ');
                            for (int col = 0; col < size; col++)
                            {
                                puzzle[row, col] = int.Parse(rowNumbers[col]);
                            }
                        }
                        AStar aStar = new AStar();
                        Node parent = new Node(puzzle, size);
                        line = reader.ReadLine();
                        int expectedResult = int.Parse(line);
                        Stopwatch sw_hamming = Stopwatch.StartNew();
                        Node recievedHammingNode = aStar.aStar(parent, "hamming");
                        sw_hamming.Stop();
                        if (recievedHammingNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for hamming at test case " + (Case + 1));
                            return;
                        }
                        if(recievedHammingNode.level == -1)
                        {
                            Console.WriteLine("Not solvable");
                        }
                        else
                        {
                            Console.WriteLine("Solvable");
                            Console.WriteLine("Number of steps = " + recievedHammingNode.level);
                        }
                       
                        aStar.printSteps();
                        
                        Console.WriteLine("time for hamming at case " + (Case + 1) + " is " + sw_hamming.ElapsedMilliseconds + " ms");
                        Stopwatch sw_manhattan = Stopwatch.StartNew();
                        Node recievedManhattanNode = aStar.aStar(parent, "manhattan");
                        sw_manhattan.Stop();
                        if (recievedManhattanNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for mannhattan at test case " + (Case + 1));
                            return;
                        }
                        Console.WriteLine("time for manhattan at case " + (Case + 1) + " is " + sw_manhattan.ElapsedMilliseconds + " ms");
                        Console.WriteLine("-----------------------------------------------------------------------------");
                    }
                    Console.WriteLine("Congratulation your sample tests ran successfully.");
                    break;
                #endregion
                case '2':
                    #region Complete Tests Both
                    testsFile = new FileStream("completeTestsBoth.txt", FileMode.Open, FileAccess.Read);

                    reader = new StreamReader(testsFile);
                    line = reader.ReadLine();
                    cases = int.Parse(line);
                    for (int Case = 0; Case < cases; Case++)
                    {
                        line = reader.ReadLine();
                        int size = int.Parse(line);
                        int[,] puzzle = new int[size, size];
                        for (int row = 0; row < size; row++)
                        {
                            line = reader.ReadLine();
                            string[] rowNumbers = line.Split(' ');
                            for (int col = 0; col < size; col++)
                            {
                                puzzle[row, col] = int.Parse(rowNumbers[col]);
                            }
                        }
                        AStar aStar = new AStar();
                        Node parent = new Node(puzzle, size);
                        line = reader.ReadLine();
                        int expectedResult = int.Parse(line);
                        Stopwatch sw_hamming = Stopwatch.StartNew();
                        Node recievedHammingNode = aStar.aStar(parent, "hamming");
                        sw_hamming.Stop();
                        if (recievedHammingNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for hamming at test case " + (Case + 1));
                            return;
                        }
                        if (recievedHammingNode.level == -1)
                        {
                            Console.WriteLine("Not solvable");
                        }
                        else
                        {
                            Console.WriteLine("Solvable");
                            Console.WriteLine("Number of steps = " + recievedHammingNode.level);
                        }

                        aStar.printSteps();

                        Console.WriteLine("time for hamming at case " + (Case + 1) + " is " + sw_hamming.ElapsedMilliseconds +" ms");
                        Stopwatch sw_manhattan = Stopwatch.StartNew();
                        Node recievedManhattanNode = aStar.aStar(parent, "manhattan");
                        sw_manhattan.Stop();
                        if (recievedManhattanNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for mannhattan at test case " + (Case + 1));
                            return;
                        }
                        Console.WriteLine("time for manhattan at case " + (Case + 1) + " is " + sw_manhattan.ElapsedMilliseconds+ " ms");
                        Console.WriteLine("-----------------------------------------------------------------------------");
                    }
                    Console.WriteLine("Congratulation your complete tests for both ran successfully.");
                    break;
                #endregion
                case '3':
                    #region Tests Manhattan
                    testsFile = new FileStream("completeTestsManhattan.txt", FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(testsFile);
                    line = reader.ReadLine();
                    cases = int.Parse(line);
                    for (int Case = 0; Case < cases; Case++)
                    {
                        line = reader.ReadLine();
                        int size = int.Parse(line);
                        int[,] puzzle = new int[size, size];
                        for (int row = 0; row < size; row++)
                        {
                            line = reader.ReadLine();
                            string[] rowNumbers = line.Split(' ');
                            for (int col = 0; col < size; col++)
                            {
                                puzzle[row, col] = int.Parse(rowNumbers[col]);
                            }
                        }
                        AStar aStar = new AStar();
                        Node parent = new Node(puzzle, size);
                        line = reader.ReadLine();
                        int expectedResult = int.Parse(line);
                        Stopwatch sw = Stopwatch.StartNew();
                        Node recievedManhattanNode = aStar.aStar(parent, "manhattan");
                        sw.Stop();
                        if (recievedManhattanNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for manhattan at test case " + (Case + 1));
                            //return;
                        }
                        if (recievedManhattanNode.level == -1)
                        {
                            Console.WriteLine("Not solvable");
                        }
                        else
                        {
                            Console.WriteLine("Solvable");
                            Console.WriteLine("Number of steps = " + recievedManhattanNode.level);
                        }
                      
                            aStar.printSteps();
                        
                        Console.WriteLine("time for manhattan at case " + (Case + 1) + " is " + sw.ElapsedMilliseconds + " ms");
                        Console.WriteLine("-----------------------------------------------------------------------------");
                    }
                    Console.WriteLine("Congratulation your complete tests for Manhattan ran successfully.");
                    break;
                #endregion
              
                default:
                    #region
                    Console.WriteLine("Invalid choice");
                    break;
                    #endregion

            }


        }

    }
}