using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;
namespace ProjectBones
{
    static class DebugMngr
    {
        static List<Collider> items;
        static List<Sprite> sprites;

        static DebugMngr()
        {
            items = new List<Collider>();
            sprites = new List<Sprite>();
        }

        public static void AddItem(Collider c)
        {
            items.Add(c);

            if (c is CircleCollider)
            {
                sprites.Add(new Sprite(((CircleCollider)c).Radius * 2, ((CircleCollider)c).Radius * 2));
            }
            else if (c is BoxCollider)
            {
                sprites.Add(new Sprite(((BoxCollider)c).Width, ((BoxCollider)c).Height));
            }
            else
            {
                sprites.Add(new Sprite(c.RigidBody.GameObject.HalfWidth * 2, c.RigidBody.GameObject.HalfHeight * 2));
            }
        }

        public static void RemoveItem(Collider c)
        {
            int index = items.IndexOf(c);
            items.RemoveAt(index);
            sprites.RemoveAt(index);
        }

        public static void ClearAll()
        {
            items.Clear();
            sprites.Clear();
        }

        public static void Draw()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].RigidBody.IsActive)
                {
                    Vector4 col = new Vector4();

                    if (items[i] is CircleCollider)
                    {
                        col = new Vector4(255.0f, 0.0f, 0.0f, 255.0f);
                    }
                    else
                    {
                        col = new Vector4(0.0f, 255.0f, 0.0f, 255.0f);
                    }

                    sprites[i].position = items[i].Position - new Vector2(items[i].RigidBody.GameObject.HalfWidth ,
                                                                          items[i].RigidBody.GameObject.HalfHeight );

                    sprites[i].DrawWireframe(col);
                }
            }
        }
    }
}
