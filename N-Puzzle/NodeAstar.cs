using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class NodeAstar : Node
    {      
        public int h;                    
        public int cost;       
        public NodeAstar parent;         
    

        public NodeAstar(int[,] arr, int size): base(arr, size)
        {
            parent = null;
        }

        public NodeAstar(NodeAstar parent): base(parent)
        {
            this.parent = parent;
        }

        public int computeCost(NodeAstar node,string costType)
        {
            if(costType == "hamming" || costType == "Hamming")
            {
                node.h = ComputeHamming(node);
            }
            else if(costType == "manhattan" || costType == "Manhattan")
            {
                node.h = computeManhattan(node);
            }
            else
            {
                throw new Exception("Wrong cost function.");
            }
            node.cost = node.level + node.h;
            return node.cost;

        }
        public NodeAstar moveUp() 
        {
            // Copy the parent's data (grid, g, x, y) and add parent
            NodeAstar child = new NodeAstar(this);
            
            child.grid[blankRow, blankCol] = grid[blankRow - 1, blankCol];
            child.grid[blankRow - 1, blankCol] = 0;
            child.blankRow = blankRow - 1;
            child.blankCol = blankCol;
            return child;
        }

        public NodeAstar moveDown()
        {
            // Copy the parent's data
            NodeAstar child = new NodeAstar(this);

         
            child.grid[blankRow, blankCol] = grid[blankRow + 1, blankCol];
            child.grid[blankRow + 1, blankCol] = 0;
            child.blankRow = blankRow + 1;
            child.blankCol = blankCol;
            return child;
        }

        public NodeAstar moveRight()
        {
            // Copy the parent's data
            NodeAstar child = new NodeAstar(this);

            child.grid[blankRow, blankCol] = grid[blankRow, blankCol + 1];
            child.grid[blankRow, blankCol + 1] = 0;
            child.blankRow = blankRow;
            child.blankCol = blankCol + 1;
            return child;
        }

        public NodeAstar moveLeft()
        {
            // Copy the parent's data
            NodeAstar child = new NodeAstar(this);
           
            child.grid[blankRow, blankCol] = grid[blankRow, blankCol - 1];
            child.grid[blankRow, blankCol - 1] = 0;
            child.blankRow = blankRow;
            child.blankCol = blankCol - 1;
            return child;
        }

        public int ComputeHamming(NodeAstar node)
        {
            int hamming = 0,count = 1;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (node.grid[row, col] != 0)
                    {
                        if (node.grid[row, col] != count)
                            hamming++;
                    }
                    count++;
                    count %= size * size;
                }
            }
            return hamming;
        }

        public int computeManhattan(NodeAstar node)
        {
            int manhattan = 0, expectedRow = 0, exepectedCol = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (node.grid[row, col] != 0) 
                    {
                        exepectedCol = (node.grid[row, col] - 1) % size;
                        expectedRow = (node.grid[row, col] - 1) / size;
                        manhattan = manhattan + Math.Abs(row - expectedRow) + Math.Abs(col - exepectedCol);
                    }

                }
             
            }
            return manhattan;
        }
        public void printState()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // For beautifying reasons
                    String num = "0" + grid[row, col].ToString();
                    Console.Write(num.Substring(num.Length - 2));
                    if (col < size - 1) Console.Write(" | ");
                }
                Console.Write('\n');
            }
        }

       
     
    }
}
