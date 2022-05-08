using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class Node
    {
        public int[,] grid;         // Game's state
        public int h;               // Heuristic value
        public int g;               // Cost so far
        public int f;               // h + g
        public int x;               // Empty slot's X
        public int y;               // Empty slot's Y
        public Node parent;         // Parent (if exists)
        public static int size;     // Size of the grid

        public Node(int[,] arr, int size)
        {
            Node.size = size;

            // Could've used this => grid = arr.Clone() as int[,];
            // But wouldn't have found X and Y

            grid = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = arr[i, j];
                    if (arr[i, j] == 0)
                    {
                        x = i;
                        y = j;
                    }
                }
            }
            g = 0;
            h = getDistance(this, 'H');
            f = g + h;
            parent = null;
        }

        public Node(Node parent)
        {
            grid = parent.grid.Clone() as int[,];
            g = parent.g + 1;
            this.parent = parent;
            x = parent.x;
            y = parent.y;
        }

        public int getFinal()
        {
            h = getDistance(this, 'H');
            return g +h;
        }

        public bool isFinalState()
        {
            int count = 1;
            int[,] finalState = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    finalState[i, j] = count++;

            finalState[size - 1, size - 1] = 0;

            /*
             * 1 2 3
             * 4 5 6
             * 7 8 0
             */

            return isEqual(finalState);
        }

        public bool isEqual(int[,] arr)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (arr[i, j] != grid[i, j])
                        return false;
            return true;
        }

        public Node moveUp()
        {
            // Copy the parent's data (grid, g, x, y) and add parent
            Node child = new Node(this);

            child.grid[x, y] = grid[x - 1, y];
            child.grid[x - 1, y] = 0;
            child.x = x - 1;
            child.y = y;
            h = getDistance(this, 'H');
            child.f = child.h + child.g;
            return child;
        }

        public Node moveDown()
        {
            // Copy the parent's data
            Node child = new Node(this);

            child.grid[x, y] = grid[x + 1, y];
            child.grid[x + 1, y] = 0;
            child.x = x + 1;
            child.y = y;
            h = getDistance(this, 'H');
            child.f = child.h + child.g;
            return child;
        }

        public Node moveRight()
        {
            // Copy the parent's data
            Node child = new Node(this);

            child.grid[x, y] = grid[x, y + 1];
            child.grid[x, y + 1] = 0;
            child.x = x;
            child.y = y + 1;
            h = getDistance(this, 'H');
            child.f = child.h + child.g;
            return child;
        }

        public Node moveLeft()
        {
            // Copy the parent's data
            Node child = new Node(this);

            child.grid[x, y] = grid[x, y - 1];
            child.grid[x, y - 1] = 0;
            child.x = x;
            child.y = y - 1;
            h = getDistance(this, 'H');
            child.f = child.h + child.g;
            return child;
        }

        public int getDistance(Node node, char choice)
        {
            if (choice == 'M' || choice == 'm')
            {
                int c = 1;
                int[,] finalState = new int[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        finalState[i, j] = c++;

                finalState[size - 1, size - 1] = 0;

                int m = 0, newX = 0, newY = 0;
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        // Now current num is node.grid[row,col]
                        // Search for its equivalent in finalState through ii, jj

                        for (int ii = 0; ii < size; ii++)
                        {
                            for (int jj = 0; jj < size; jj++)
                            {
                                if (node.grid[row, col] == finalState[ii, jj])
                                {
                                    newX = ii;
                                    newY = jj;
                                }
                            }
                        }
                        m += Math.Abs(newX - row) + Math.Abs(newY - col);
                    }
                }
                return m;
            } else
            {
                int h = 0, count = 1;
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        if (node.grid[row, col] != 0)
                        {
                            if (node.grid[row, col] != count)
                                h++;
                        }
                        count++;
                        count %= size * size;
                    }
                }
                return h;
            }
        }


        public void printState()
        {
            //Console.WriteLine(getHamming(this));
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // For beautifying reasons
                    String num = "0" + grid[i, j].ToString();
                    Console.Write(num.Substring(num.Length - 2));
                    if (j < size - 1) Console.Write(" | ");
                }
                Console.Write('\n');
            }
        }

        public Boolean isSolvable()
        {
            int[] tempArr = new int[size * size];
            int k = 0, ans=0;

            // Convert to 1D array
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tempArr[k++] = grid[i, j];
                }
            }

            for (int i = 0; i < size*size; i++)
            {
                if (tempArr[i] == 0) continue;
                for (int j = i+1; j < size*size; j++)
                {
                    if (tempArr[j] != 0 && tempArr[i] < tempArr[j]) ans++;
                }
            }

            if (size%2 == 1 && ans % 2 == 0) return true;

            if (size %2 == 0)
                if (x % 2 == ans % 2) return true;

            return false;
        }
    }
}
