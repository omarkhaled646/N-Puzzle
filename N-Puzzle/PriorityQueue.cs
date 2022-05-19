using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N_Puzzle;
namespace priorityQueue
{
    class priorityqueue
    {
        NodeAstar[] nodes;
        int size = 0;

        public priorityqueue()
        {
            nodes = new NodeAstar[100000000];
        }

        public int Count()
        {
            return size;
        }

        public NodeAstar extractMin()
        {
            if (size == 0) 
            {
                throw new InvalidOperationException("There is no elements in the queue.");
            }

            NodeAstar minNode = nodes[1];
            nodes[1] = nodes[size];
            size -= 1;
            minHeapify(1, size);
            return minNode;
        }

        public void minHeapify(int index , int size)
        {
            int leftNodeIndex = 2 * index;
            int rightNodeIndex = 2 * index + 1;
            int minNodeIndex;

            if(leftNodeIndex <= size && nodes[leftNodeIndex].cost < nodes[index].cost)
            {
                minNodeIndex = leftNodeIndex;
            }
            else
            {
                minNodeIndex = index;
            }

            if(rightNodeIndex <= size && nodes[rightNodeIndex].cost < nodes[minNodeIndex].cost)
            {
                minNodeIndex = rightNodeIndex;
            }

            if(minNodeIndex!=index)
            {
                swap(ref nodes[index], ref nodes[minNodeIndex]);
                minHeapify(minNodeIndex, size);
            }
        }
        public void swap(ref NodeAstar firstNode , ref NodeAstar secondNode)
        {
            NodeAstar tempNode = firstNode;
            firstNode = secondNode;
            secondNode = tempNode;
        }

        public NodeAstar dequeue()
        {
            return extractMin();
        }

        public void enqueue(NodeAstar node)
        {
            insert(node);
        }

        public void insert(NodeAstar node)
        {
            size += 1;
            nodes[size] = null;
            increaseKey(node, size);
        }

        public void increaseKey(NodeAstar node , int size)
        {
            nodes[size] = node;
            while (size > 1 && nodes[size / 2].cost >= nodes[size].cost)
            {
                swap(ref nodes[size / 2], ref nodes[size]);
                size/= 2;
            }
        }
    }
}

    

