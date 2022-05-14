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
        public Stack<Node> finalAnswer;
        public HashSet<int> finished;
        public  priorityqueue nodes;
        public Node current, child;
        public Node aStar(Node parent,string costType)
        {
            finalAnswer = new Stack<Node>();
            finished = new HashSet<int>(); 
            nodes = new priorityqueue();
     
            if (!parent.isSolvable())
            {
                parent.level = -1;
                return parent;
            }

           parent.cost = parent.computeCost(parent,costType);
            if (parent.h == 0)
            {
                getSteps(parent);
                return parent;
            }
          

            // Let's go through all four options
            getChildern(parent, costType);
            // Now we are done with the parent
            finished.Add(parent.gridToHash());
            current = nodes.dequeue();
            while (nodes.Count()!=0)
            {

                //current.printState();
                // Base case (Finish state)
             
                   if (current.h == 0)
                {
                    getSteps(current);
                    return current;
                }
                // We will start pushing the children so remove the parent from top
             
                
                // Repeat the four combinations again (beware of the states in finished)
                finished.Add(current.gridToHash());
                getChildern(current,costType);
                current = nodes.dequeue();

            }
          
            // If some error happened, return null or anything else (number of steps)
            return null;
        }
        public void getChildern(Node node,string costType)
        { 
                if (node.blankRow + 1 <= Node.size - 1)
                {
                    child = node.moveDown();
                    if (!finished.Contains(child.gridToHash()))
                    {
                        child.cost = child.computeCost(child, costType);
                        nodes.enqueue(child);
                    }
            }
                if (node.blankRow - 1 >= 0)
                {
                    child = node.moveUp();
                    if (!finished.Contains(child.gridToHash()))
                {
                         child.cost = child.computeCost(child, costType);
                         nodes.enqueue(child);
                }
            }
                if (node.blankCol + 1 <= Node.size - 1)
                {
                    child = node.moveRight();
                    if (!finished.Contains(child.gridToHash()))
                {
                        child.cost = child.computeCost(child, costType);
                        nodes.enqueue(child);
                }
                }
                if (node.blankCol - 1 >= 0)
                {
                    child = node.moveLeft();
                    if (!finished.Contains(child.gridToHash()))
                {
                        child.cost = child.computeCost(child, costType);
                        nodes.enqueue(child);
                }
                }
           
        }

        
        public void getSteps(Node node)
        {
           
            while(node.parent!=null)
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
                    Console.WriteLine("Step " + stpCntr +" :");
                    stpCntr++;
                }
                finalAnswer.Pop().printState();
                Console.WriteLine("-------------");
            }
        }
       
    }
}
