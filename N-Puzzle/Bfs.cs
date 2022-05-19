using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class Bfs
    {
        public HashSet<uint> finished;
        public Queue<NodeBfs> nodes;
        public NodeBfs current, child;

        public NodeBfs bfs(NodeBfs parent)
        {
            finished = new HashSet<uint>();
            nodes = new Queue<NodeBfs>();

            if (!parent.isSolvable())
            {
                parent.level = -1;
                return parent;
            }

            if (parent.isFinalState())
            {
                return parent;
            }


            getChildern(parent);
            finished.Add(parent.hashCode());


            while (nodes.Count() != 0)
            {

                current = nodes.Dequeue();
                if (current.isFinalState())
                {
                    return current;
                }
                
                getChildern(current);
                finished.Add(current.hashCode());
             
            }

            // If some error happened, return null or anything else (number of steps)
            return null;
        }
        public void getChildern(NodeBfs node)
        {
            if (node.blankRow + 1 <= Node.size - 1)
            {
                child = node.moveDown();
                if (!finished.Contains(child.hashCode()))
                {
                    nodes.Enqueue(child);
                }
            }
            if (node.blankRow - 1 >= 0)
            {
                child = node.moveUp();
                if (!finished.Contains(child.hashCode()))
                {
                    nodes.Enqueue(child);
                }
            }
            if (node.blankCol + 1 <= Node.size - 1)
            {
                child = node.moveRight();
                if (!finished.Contains(child.hashCode()))
                {
                    nodes.Enqueue(child);
                }
            }
            if (node.blankCol - 1 >= 0)
            {
                child = node.moveLeft();
                if (!finished.Contains(child.hashCode()))
                {
                    nodes.Enqueue(child);
                }
            }

        }

    }
}
