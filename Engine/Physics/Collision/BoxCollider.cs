using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Width { get { return halfWidth * 2f; } }
        public float Height { get { return halfHeight * 2f; } }
        public BoxCollider(RigidBody owner, float w, float h) : base(owner)
        {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;
        }

        public override bool Collides(Collider collider, ref Collision collisionInfo)
        {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider collider, ref Collision collisionInfo)
        {
            float deltaX = collider.Position.X - Math.Max(Position.X - halfWidth, Math.Min(collider.Position.X, Position.X + halfWidth));
            float deltaY = collider.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(collider.Position.Y, Position.Y + halfHeight));

            return (deltaX * deltaX + deltaY * deltaY <= (collider.Radius * collider.Radius));
        }

        public override bool Contains(Vector2 point, ref Collision collisionInfo)
        {
            return point.X >= Position.X - halfWidth &&
                   point.X <= Position.X + halfWidth &&
                   point.Y >= Position.Y - halfHeight &&
                   point.Y <= Position.Y + halfHeight;
        }

        public override bool Collides(BoxCollider collider, ref Collision collisionInfo)
        {
            #region SAT box collider
            //SAT Algorithm separated access theory

            //float deltaX = collider.Position.X - Position.X;
            //float deltaY = collider.Position.Y - Position.Y;

            //return (Math.Abs(deltaX)) <= halfWidth + collider.halfWidth &&
            //       (Math.Abs(deltaY)) <= halfHeight + collider.halfHeight; 
            #endregion

            Vector2 distance = collider.Position - Position;

            float deltaX = Math.Abs(distance.X) - (collider.halfWidth + halfWidth);     //calcolo il delta tra la distanza orizzontale più la somma delle metà larghezze

            if (deltaX > 0)                                                             //se il delta è maggiore di 0 vuol dire che non sto collidendo orizzontalmente
            {
                return false;
            }

            float deltaY = Math.Abs(distance.Y) - (collider.halfHeight + halfHeight);

            if (deltaY > 0)                                                             //se il deltaY è maggiore di 0 vuol dire che non sto collidendo verticalmente
            {
                return false;
            }
            
            //se passa i primi due controlli vuol dire che sto collidendo

            collisionInfo.Type = CollisionType.RECTSINTERSECTION;
            collisionInfo.Delta = new Vector2(-deltaX, -deltaY);                        //deltaX e deltaY saranno entrambi minori di 0 quindi vanno rimessi in positivo in quanto il delta deve essere positivo

            return true;
        }
        public override bool Collides(CompoundCollider collider, ref Collision collisionInfo)
        {
            return collider.Collides(this, ref collisionInfo);
        }
    }
}
