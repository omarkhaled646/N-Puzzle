using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using priorityQueue;
namespace N_Puzzle
{
    class AStar
    {
        public Stack<NodeAstar> finalAnswer;
        public HashSet<uint> finished;
        public priorityqueue nodes;
        public NodeAstar current, child;

        public NodeAstar aStar(NodeAstar parent, string costType)
        {
            finalAnswer = new Stack<NodeAstar>();
            finished = new HashSet<uint>();
            nodes = new priorityqueue();

            // Exit if puzzle is unsolvable
            if (!parent.isSolvable())
            {
                parent.level = -1;
                return parent;
            }

            parent.cost = parent.computeCost(parent, costType);

            // If final state, compute number of steps
            if (parent.h == 0)
            {
                getSteps(parent);
                return parent;
            }


            // Let's go through all four options
            getChildern(parent, costType);

            // Now we are done with the parent, add to finished and remove from current queue
            finished.Add(parent.hashCode());
            current = nodes.dequeue();

            while (nodes.Count() != 0)
            {
                // Base case (Finish state) when heuristic = 0
                if (current.h == 0)
                {
                    getSteps(current);
                    return current;
                }

                // Done with current, add to finished and remove from current queue
                finished.Add(current.hashCode());

                // Repeat the four combinations again
                getChildern(current, costType);
                current = nodes.dequeue();
            }

            // If some error happened, return null
            return null;
        }
        public void getChildern(NodeAstar node, string costType)
        {
            if (node.blankRow + 1 <= Node.size - 1)
            {
                child = node.moveDown();
                if (!finished.Contains(child.hashCode()))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.enqueue(child);
                }
            }
            if (node.blankRow - 1 >= 0)
            {
                child = node.moveUp();
                if (!finished.Contains(child.hashCode()))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.enqueue(child);
                }
            }
            if (node.blankCol + 1 <= Node.size - 1)
            {
                child = node.moveRight();
                if (!finished.Contains(child.hashCode()))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.enqueue(child);
                }
            }
            if (node.blankCol - 1 >= 0)
            {
                child = node.moveLeft();
                if (!finished.Contains(child.hashCode()))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.enqueue(child);
                }
            }

        }

        // Gets the path from start state to goal
        public void getSteps(NodeAstar node)
        {
            while (node.parent != null)
            {
                finalAnswer.Push(node);
                node = node.parent;
            }
            finalAnswer.Push(node);
        }

        public void printSteps()
        {
            int stpCntr = 0;
            while (finalAnswer.Count != 0)
            {
                if (stpCntr == 0)
                {
                    Console.WriteLine("Parent :");
                    stpCntr++;
                }
                else
                {
                    Console.WriteLine("Step " + stpCntr + " :");
                    stpCntr++;
                }
                finalAnswer.Pop().printState();
                Console.WriteLine("-------------");
            }
        }

    }
}
