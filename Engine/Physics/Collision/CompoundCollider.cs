using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class CompoundCollider : Collider
    {
        public Collider BoundingCollider;
        protected List<Collider> colliders;

        public CompoundCollider(RigidBody owner, Collider boundingCollider) : base(owner)
        {
            BoundingCollider = boundingCollider;
            colliders = new List<Collider>();

            if (BoundingCollider is CircleCollider)
            {
                Offset = Vector2.Zero;
            }
            if (BoundingCollider is BoxCollider)
            {
                Offset = new Vector2(((BoxCollider)BoundingCollider).Width, (((BoxCollider)BoundingCollider).Height));
            }
        }

        public virtual void AddCollider(Collider collider)
        {
            colliders.Add(collider);
        }

        public virtual bool InnerCollidersCollide(Collider collider, ref Collision collisionInfo)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (collider.Collides(colliders[i], ref collisionInfo))
                {
                    return true;
                }
            }

            return false;
        }
        public override bool Collides(Collider collider, ref Collision collisionInfo)
        {
            return collider.Collides(BoundingCollider , ref collisionInfo) && InnerCollidersCollide(collider, ref collisionInfo);
        }
        public override bool Collides(CircleCollider collider, ref Collision collisionInfo)
        {
            return collider.Collides(BoundingCollider, ref collisionInfo) && InnerCollidersCollide(collider, ref collisionInfo);
        }
        public override bool Collides(BoxCollider collider, ref Collision collisionInfo)
        {
            return collider.Collides(BoundingCollider, ref collisionInfo) && InnerCollidersCollide(collider, ref collisionInfo);
        }
        public override bool Contains(Vector2 point, ref Collision collisionInfo)
        {
            throw new NotImplementedException();
        }

        public override bool Collides(CompoundCollider collider, ref Collision collisionInfo)
        {
            if (BoundingCollider.Collides(collider.BoundingCollider,ref collisionInfo))
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    for (int j = 0; j < collider.colliders.Count; j++)
                    {
                        if (colliders[i].Collides(collider.colliders[j], ref collisionInfo))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
