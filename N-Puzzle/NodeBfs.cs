using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class NodeBfs : Node
    {
        public NodeBfs parent;
        public static int[,] goalState;
        public NodeBfs(int[,] arr, int size) : base(arr, size)
        {
            parent = null;
            int count = 1;
            goalState = new int[size,size];
            for (int row = 0; row < size; row++) 
            {
                for (int col = 0; col < size; col++)
                {
                    if (row == size - 1 && col == size - 1)
                        continue;
                    goalState[row, col] = count;
                    count++;
                }
            }
            goalState[size - 1, size - 1] = 0;
        }


        public NodeBfs(NodeBfs parent) : base(parent) 
        {
            this.parent = parent;
        }

        public NodeBfs moveUp()
        {
            // Copy the parent's data (grid, g, x, y) and add parent
            NodeBfs child = new NodeBfs(this);

            child.grid[blankRow, blankCol] = grid[blankRow - 1, blankCol];
            child.grid[blankRow - 1, blankCol] = 0;
            child.blankRow = blankRow - 1;
            child.blankCol = blankCol;
            return child;
        }

        public NodeBfs moveDown()
        {
            // Copy the parent's data
            NodeBfs child = new NodeBfs(this);


            child.grid[blankRow, blankCol] = grid[blankRow + 1, blankCol];
            child.grid[blankRow + 1, blankCol] = 0;
            child.blankRow = blankRow + 1;
            child.blankCol = blankCol;
            return child;
        }

        public NodeBfs moveRight()
        {
            // Copy the parent's data
            NodeBfs child = new NodeBfs(this);

            child.grid[blankRow, blankCol] = grid[blankRow, blankCol + 1];
            child.grid[blankRow, blankCol + 1] = 0;
            child.blankRow = blankRow;
            child.blankCol = blankCol + 1;
            return child;
        }

        public NodeBfs moveLeft()
        {
            // Copy the parent's data
            NodeBfs child = new NodeBfs(this);

            child.grid[blankRow, blankCol] = grid[blankRow, blankCol - 1];
            child.grid[blankRow, blankCol - 1] = 0;
            child.blankRow = blankRow;
            child.blankCol = blankCol - 1;
            return child;
        }

        public bool isFinalState()
        {
            int count = 1;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    
                        if (grid[row, col] != goalState[row,col])
                        {
                            return false;
                        }
                    
                    count++;
                }
            }
            return true;
        }
    }
}