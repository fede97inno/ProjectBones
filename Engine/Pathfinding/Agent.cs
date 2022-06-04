using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Agent
    {
        protected float speed;

        protected Actor owner;
        protected List<Node> path;
        protected Node current;
        protected Node target;

        protected Sprite pathSprite;
        protected Vector4 pathCol;

        public Node Target { get { return target; } set { target = value; } }
        public int X { get { return Convert.ToInt32(owner.Position.X); } }         //return the position but it cut the float value
        public int Y { get { return Convert.ToInt32(owner.Position.Y); } }         //return the position but it cut the float value

        public Agent(Actor owner)
        {
            this.owner = owner;
            target = null;

            pathSprite = new Sprite(0.25f, 0.25f);
            pathSprite.pivot = new Vector2(pathSprite.Width * 0.5f, pathSprite.Height * 0.5f);
            pathCol = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        public virtual void SetPath(List<Node> newPath)
        {
            path = newPath;

            if (target == null && path.Count > 0)                   //if target is null
            {
                target = path[0];                                   //we put target in the first node og the path
                path.RemoveAt(0);                                   //and we remove it
            }
            else if(path.Count > 0)
            {
                int dist = Math.Abs(path[0].X - target.X) + Math.Abs(path[0].Y - target.Y);

                if (dist > 1)                                       //we control if we are jumping node if the distance is haigher than 1 it means that we are moving diagonally and 
                {
                    path.Insert(0, current);                        //we force the movement readding the node 
                }
            }
        }

        public virtual void Update(float speed)
        {
            if (target != null)
            {
                Vector2 dest = new Vector2(target.X + 0.5f, target.Y + 0.5f);//without pivot
                Vector2 dir = dest - owner.Position;               //from position to destination
                float distance = dir.Length;

                if (distance < 0.05f)                               //when we are near the center of the node
                {
                    current = target;
                    owner.Position = dest;

                    if (path.Count == 0)
                    {
                        target = null;
                    }
                    else
                    {
                        target = path[0];
                        path.RemoveAt(0);
                    }
                }
                else
                {
                    owner.Position += dir.Normalized() * speed * Game.DeltaTime;
                    owner.Forward = dir.Normalized();
                }
            }
        }

        public void Draw()
        {
            DrawPath();
        }

        public void DrawPath()
        {
            if (path != null && path.Count > 0)
            {
                foreach (Node n in path)
                {
                    pathSprite.position = new Vector2(n.X +0.5f, n.Y + 0.5f);
                    pathSprite.DrawColor(pathCol);
                }
            }
        }
    }
}
