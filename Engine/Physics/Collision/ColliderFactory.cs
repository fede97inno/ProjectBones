using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    static class ColliderFactory
    {
        public static Collider CreateCircleFor(GameObject obj, bool innerCircle = true)
        {
            float radius;

            if (innerCircle)
            {
                if (obj.HalfWidth < obj.HalfHeight)
                {
                    radius = obj.HalfWidth;
                }
                else
                {
                    radius = obj.HalfHeight;
                }
            }
            else
            {
                radius = (float)Math.Sqrt(obj.HalfWidth * obj.HalfWidth + obj.HalfHeight * obj.HalfHeight);
            }

            return new CircleCollider(obj.RigidBody, radius);
        }

        public static Collider CreateBoxFor(GameObject obj)
        {
            return new BoxCollider(obj.RigidBody, obj.Width, obj.Height);
        }
    }
}
