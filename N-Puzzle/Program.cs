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

            Queue<Node> finalAnswer = new Queue<Node>();

            int[,] notSolvable = { { 1, 2, 3 },
                           { 4, 5, 6 },
                           { 8, 7, 0 } };

            int[,] solvable1 = { { 1, 2, 3 },
                           { 4, 5, 6 },
                           { 7, 0, 8 } };

            int[,] solvable8 = { { 0, 1, 2 },
                           { 5, 6, 3 },
                           { 4, 7, 8 } };

            int[,] solvable910 = { { 2, 3, 6 },
                                   { 1, 4, 8 },
                                   { 7, 5, 0 } };


            int[,] arr4 = { { 5, 3, 9, 11 },
                            { 7, 10, 15, 4 },
                            { 2, 14, 12, 1 },
                            { 6, 13, 8, 0} };

            Node parentNode = new Node(solvable910, 3);


            if (!parentNode.isSolvable())
                Console.WriteLine("Not Solvable");
            else
            {
                Console.WriteLine("Solvable");
                Console.WriteLine("Number of moves = " + aStar(parentNode));
                Console.WriteLine("-------------");

                int stpCntr = 0;
                while (finalAnswer.Count != 0)
                {
                    Console.WriteLine("Step " + stpCntr++);
                    finalAnswer.Dequeue().printState();
                    Console.WriteLine("-------------");
                }
            }

            // aStar function goes through all possible combinations and returns the number of steps

            int aStar(Node parent)
            {
                finalAnswer.Enqueue(parent);
                //parent.printState();
                List<int[,]> finished = new List<int[,]>();
                SimplePriorityQueue<Node> nodes = new SimplePriorityQueue<Node>();
                Node current, child;

                if (parent.isFinalState()) return 0;

                // Let's go through all four options
                if (parent.x + 1 <= Node.size - 1)
                    nodes.Enqueue(parent.moveDown(), parent.moveDown().getFinal());

                if (parent.x - 1 >= 0)
                    nodes.Enqueue(parent.moveUp(), parent.moveUp().getFinal());

                if (parent.y + 1 <= Node.size - 1)
                    nodes.Enqueue(parent.moveRight(), parent.moveRight().getFinal());

                if (parent.y - 1 >= 0)
                    nodes.Enqueue(parent.moveLeft(), parent.moveLeft().getFinal());

                // Now we are done with the parent
                finished.Add(parent.grid);

                current = nodes.First();

                while (nodes.Count() != 0)
                {
                    finalAnswer.Enqueue(current);
                    //current.printState();

                    // Base case (Finish state)
                    if (current.isFinalState()) return current.g;

                    // We will start pushing the children so remove the parent from top
                    nodes.Dequeue();

                    // Repeat the four combinations again (beware of the states in finished)
                    if (current.x + 1 <= Node.size - 1)
                    {
                        child = current.moveDown();
                        if (!finished.Contains(child.grid)) nodes.Enqueue(child, child.getFinal());
                    }
                    if (current.x - 1 >= 0)
                    {
                        child = current.moveUp();
                        if (!finished.Contains(child.grid)) nodes.Enqueue(child, child.getFinal());
                    }
                    if (current.y + 1 <= Node.size - 1)
                    {
                        child = current.moveRight();
                        if (!finished.Contains(child.grid)) nodes.Enqueue(child, child.getFinal());
                    }
                    if (current.y - 1 >= 0)
                    {
                        child = current.moveLeft();
                        if (!finished.Contains(child.grid)) nodes.Enqueue(child, child.getFinal());
                    }

                    finished.Add(current.grid);
                    current = nodes.First();
                }
                // If some error happened, return 0 or -1 or anything else (number of steps)
                return -1;
            }

        }
    }
}
