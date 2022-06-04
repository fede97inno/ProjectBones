using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Node
    {
        public int X { get; }
        public int Y { get; }
        public int Cost { get; }
        public List<Node> Neighbours { get; }

        public Node(int x, int y, int cost)
        {
            X = x;
            Y = y;
            Cost = cost;
            Neighbours = new List<Node>();
        }

        public void AddNeighbour(Node neighbour)
        {
            Neighbours.Add(neighbour);
        }
        public void RemoveNeighbour(Node neighbour)
        {
            Neighbours.Remove(neighbour);
        }
    }
}
