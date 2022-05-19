using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    abstract class Node
    {
        public int[,] grid;                  
        public int level;            
        public int blankRow;        
        public int blankCol;          
        public static int size;


        public Node(int[,] arr, int size)
        {
            Node.size = size;
            grid = new int[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    grid[row, col] = arr[row, col];
                    if (arr[row, col] == 0)
                    {
                        blankRow = row;
                        blankCol = col;
                    }
                }
            }
            level = 0;
        }

        public Node(Node parent)
        {
            grid = parent.grid.Clone() as int[,];
            level = parent.level + 1;
            blankRow = parent.blankRow;
            blankCol = parent.blankCol;
        }

        public Boolean isSolvable()
        {
            int[] tempGrid = new int[size * size];
            int cells = 0, ans = 0;

            // Convert to 1D array
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    tempGrid[cells] = grid[row, col];
                    cells++;
                }
            }

            for (int cell = 0; cell < size * size; cell++)
            {
                if (tempGrid[cell] == 0) continue;
                for (int j = cell + 1; j < size * size; j++)
                {
                    if (tempGrid[j] != 0 && tempGrid[cell] < tempGrid[j]) ans++;
                }
            }

            if (size % 2 == 1 && ans % 2 == 0) return true;

            if (size % 2 == 0)
                if (blankRow % 2 == ans % 2) return true;

            return false;
        }

   
        public uint hashCode()
        {
            uint hash = 193;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    hash = hash * 59 + (uint)(grid[row, col]);

                }
            }


            return hash;
        }
    }
}
