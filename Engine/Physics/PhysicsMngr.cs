using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProjectBones
{
    static class PhysicsMngr
    {
        static List<RigidBody> items;
        static Collision collisionInfo;

        public static float G = 9f;

        static PhysicsMngr()
        {
            items = new List<RigidBody>();
        }

        public static void AddItem(RigidBody item)
        {
            items.Add(item);
        }

        public static void RemoveItem(RigidBody item)
        {
            items.Remove(item);
        }
        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsActive)
                {
                    items[i].Update();
                }
            }
        }

        public static void CheckCollision()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (items[i].IsActive && items[i].IsCollisionAffected)
                {
                    for (int j = i + 1; j < items.Count; j++)
                    {
                        if (items[j].IsActive && items[j].IsCollisionAffected)
                        {
                            bool firstCheck = items[i].CollisionTypeMatches(items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches(items[i].Type);
                            
                            if ((firstCheck || secondCheck) && items[i].Collides(items[j], ref collisionInfo))
                            {
                                //On collision verified (between two GameObjects)
                                if (firstCheck)
                                {
                                    collisionInfo.Collider = items[j].GameObject;
                                    items[i].GameObject.OnCollide(collisionInfo); 
                                }
                                if (secondCheck)
                                {
                                    collisionInfo.Collider = items[i].GameObject;
                                    items[j].GameObject.OnCollide(collisionInfo); 
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
