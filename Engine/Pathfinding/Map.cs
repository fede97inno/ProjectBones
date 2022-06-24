using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Map
    {
        private Dictionary<Node, Node> cameFrom;                    //parents
        private Dictionary<Node, int> costSoFar;                    //distances
        private PriorityQueue frontier;                             //toVisit (nexts nodes to visit)

        private int width;
        private int height;
        private int[] cells;
        private Sprite sprite;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public Node[] Nodes { get; }

        public Map(int w, int h, int[] cells)
        {
            width = w;
            height = h;
            this.cells = cells;
            sprite = new Sprite(1, 1);

            Nodes = new Node[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] <= 0)
                {
                    continue;
                }

                int x = i % width;
                int y = i / width;
                Nodes[i] = new Node(x, y, cells[i]);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    if (Nodes[index] == null)
                    {
                        continue;
                    }

                    AddNeighbours(Nodes[index], x, y);
                }
            }
        }

        private void AddNeighbours(Node n, int x, int y)        //linka solo se il vicino è dentro la griglia 
        {
            CheckNeighbour(n, x, y - 1);                        //top
            CheckNeighbour(n, x, y + 1);                        //bottom
            CheckNeighbour(n, x - 1, y);                        //left
            CheckNeighbour(n, x + 1, y);                        //right
        }

        private void CheckNeighbour(Node n, int x, int y)
        {
            if (x < 0 || x >= width)
            {
                return;
            }

            if (y < 0 || y >= height)
            {
                return;
            }

            int index = y * width + x;
            Node neighbour = Nodes[index];

            if (neighbour != null)
            {
                n.AddNeighbour(neighbour);                     //aggiunge il node se non è fuori dallo schermo
            }
        }
        private void AddNode(int x, int y, int cost = 1)
        {
            int index = y * width + x;
            Node node = new Node(x, y, cost);
            Nodes[index] = node;
            AddNeighbours(node, x, y);

            foreach (Node n in node.Neighbours)
            {
                n.AddNeighbour(node);                           //we add the new node at his nieghbours
            }

            cells[index] = cost;
        }

        private void RemoveNode(int x, int y)
        {
            int index = y * width + x;
            Node node = GetNode(x, y);

            foreach (Node adjNode in node.Neighbours)
            {
                adjNode.RemoveNeighbour(node);
            }

            Nodes[index] = null;
            cells[index] = 0;
        }

        public void ToggleNode(int x, int y)
        {
            Node node = GetNode(x, y);

            if (node == null)
            {
                AddNode(x, y);
            }
            else
            {
                RemoveNode(x, y);
            }
        }

        public Node GetNode(int x, int y)
        {
            if ((x < 0 || x >= width) || (y < 0 || y >= height))
            {
                return null;
            }

            return Nodes[y * width + x];
        }

        public void AStar(Node start, Node end)
        {
            cameFrom = new Dictionary<Node, Node>();                                                //parents
            costSoFar = new Dictionary<Node, int>();                                                //distances
            frontier = new PriorityQueue();                                                         //toVisit

            cameFrom[start] = start;                                                                //we initialize on the starting Node start
            costSoFar[start] = 0;
            frontier.Enqueue(start, Heuristic(start, end));

            while (!frontier.IsEmpty)
            {
                Node currentNode = frontier.Dequeue();

                if (currentNode == end)
                {
                    return;
                }

                foreach (Node nextNode in currentNode.Neighbours)
                {
                    int newCost = costSoFar[currentNode] + nextNode.Cost;                           //parents costs

                    if (!costSoFar.ContainsKey(nextNode)|| costSoFar[nextNode] > newCost)           //if the old cost it's heigher than the new one (and it's not in the distances we don'r want to take 
                    {                                                                               //the same node two times)
                        cameFrom[nextNode] = currentNode;
                        costSoFar[nextNode] = newCost;
                        int priority = newCost + Heuristic(nextNode, end);
                        frontier.Enqueue(nextNode, priority);
                    }
                }
            }
        }

        private int Heuristic(Node start, Node end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }

        public List<Node> GetPath(int startX, int startY, int endX, int endY)
        {
            List<Node> path = new List<Node>();

            Node start = GetNode(startX, startY);
            Node end = GetNode(endX, endY);

            if (start == null || end == null)
            {
                return path;
            }

            AStar(start, end);

            if (!cameFrom.ContainsKey(end))             //if the dict of the parents doesn t contain the final node (for example he can't arrive at it cause the map changes) i exit the method
            {
                return path;
            }

            Node currentNode = end;

            while (currentNode != cameFrom[currentNode])        //while the last node it's not his parent
            {
                path.Add(currentNode);
                currentNode = cameFrom[currentNode];            //iterate parents dict
            }

            path.Reverse();

            return path;
        }

        public void Draw()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sprite.position = new OpenTK.Vector2(x, y);

                    if (GetNode(x,y) == null)
                    {
                        sprite.DrawColor(new OpenTK.Vector4(0, 0, 0, 1));
                    }
                    else
                    {
                        sprite.DrawColor(new OpenTK.Vector4(1, 0, 0, 1));
                    }
                }
            }
        }
    }
}
