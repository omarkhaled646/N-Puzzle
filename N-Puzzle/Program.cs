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
            char algorithmChoice;
            StreamReader reader;
            string line;
            string costType = " ";
            char choiceOfCost = ' ';
            TextReader origConsole = Console.In;

            Console.WriteLine("Enter [1] for Astar or [2] for Bfs.");
            algorithmChoice = Console.ReadLine()[0];
            if(algorithmChoice != '1' && algorithmChoice != '2')
            {
                throw new Exception("Invalid choice");
            }
            if (algorithmChoice == '1')
            {
                Console.WriteLine("Enter [1] for Manhattan or [2] for Hamming.");
                choiceOfCost = Console.ReadLine()[0];
                if (choiceOfCost == '1')
                {
                    costType = "manhattan";
                }
                else if (choiceOfCost == '2')
                {
                    costType = "hamming";
                }
                else
                {
                    throw new Exception("Invalid choice");
                }
            }
            Console.WriteLine("N-Puzzle:\n[1] Sample test cases\n[2] Complete testing for both\n[3] complete testing for Manhattan only\n");
            Console.Write("\nEnter your choice [1-2-3]: ");
            char choice = Console.ReadLine()[0];
            if(choice == '3' && choiceOfCost == '2')
            {
                throw new Exception("These tests are for Manhattan only.");
            }
            
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

                        Node parent = null;
                        Node recievedNode = null;
                        AStar aStar = null;
                        Bfs bfs = null;
                        line = reader.ReadLine();
                        int expectedResult = int.Parse(line);
                        Stopwatch sw = Stopwatch.StartNew();
                        if (algorithmChoice == '1')
                        {
                            aStar = new AStar();
                            parent = new NodeAstar(puzzle, size);
                            recievedNode = aStar.aStar((NodeAstar)parent, costType);
                        }
                        else 
                        {
                            bfs = new Bfs();
                            parent = new NodeBfs(puzzle, size);
                            recievedNode = bfs.bfs((NodeBfs)parent);
                        }
                        sw.Stop();
                        if (recievedNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for hamming at test case " + (Case + 1));
                            return;
                        }
                        if(recievedNode.level == -1)
                        {
                            Console.WriteLine("Not solvable");
                        }
                        else
                        {
                            Console.WriteLine("Solvable");

                            if (algorithmChoice == '1')
                            {
                                aStar.printSteps();
                            }
                            Console.WriteLine("Number of steps = " + recievedNode.level);
                        }
                       
                        
                        Console.WriteLine("time for " + costType + " at case " + (Case + 1) + " is " + sw.ElapsedMilliseconds + " ms");
                        Console.WriteLine("time for " + costType + " at case " + (Case + 1) + " is " + (float)sw.ElapsedMilliseconds / 1000 + " s");
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
                        Node parent = null;
                        Node recievedNode = null;
                        AStar aStar = null;
                        Bfs bfs = null;
                        line = reader.ReadLine();
                        int expectedResult = int.Parse(line);
                        Stopwatch sw = Stopwatch.StartNew();
                        if (algorithmChoice == '1')
                        {
                            aStar = new AStar();
                            parent = new NodeAstar(puzzle, size);
                            recievedNode = aStar.aStar((NodeAstar)parent, costType);
                        }
                        else
                        {
                            bfs = new Bfs();
                            parent = new NodeBfs(puzzle, size);
                            recievedNode = bfs.bfs((NodeBfs)parent);
                        }
                        sw.Stop();
                        if (recievedNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for hamming at test case " + (Case + 1));
                            return;
                        }
                        if (recievedNode.level == -1)
                        {
                            Console.WriteLine("Not solvable");
                        }
                        else
                        {
                            Console.WriteLine("Solvable");
                            aStar.printSteps();
                            Console.WriteLine("Number of steps = " + recievedNode.level);
                        }

                        Console.WriteLine("time for "+ costType + " at case " + (Case + 1) + " is " + sw.ElapsedMilliseconds +" ms");
                        Console.WriteLine("time for " + costType + " at case " + (Case + 1) + " is " + (float)sw.ElapsedMilliseconds /1000+ " s");
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
                        NodeAstar parent = new NodeAstar(puzzle, size);
                        line = reader.ReadLine();
                        int expectedResult = int.Parse(line);
                        Stopwatch sw = Stopwatch.StartNew();
                        NodeAstar recievedManhattanNode = aStar.aStar(parent, costType);
                        sw.Stop();
                        if (recievedManhattanNode.level != expectedResult)
                        {
                            Console.WriteLine("Wrong answer for manhattan at test case " + (Case + 1));
                            return;
                        }
                        if (recievedManhattanNode.level == -1)
                        {
                            Console.WriteLine("Not solvable");
                        }
                        else
                        {
                            Console.WriteLine("Solvable");
                            aStar.printSteps();
                            Console.WriteLine("Number of steps = " + recievedManhattanNode.level);
                        }
                      
                        Console.WriteLine("time for manhattan at case " + (Case + 1) + " is " + sw.ElapsedMilliseconds + " ms");
                        Console.WriteLine("time for manhattan at case " + (Case + 1) + " is " + (float)sw.ElapsedMilliseconds /1000 + " s");
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