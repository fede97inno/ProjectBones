using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Door : GameObject
    {
        public bool IsOpen = false;
        public Door(Vector2 pos) : base("house_door", DrawLayer.PLAYGROUND, 16, 16)
        {
            IsActive = true;
            Position = pos;
            RigidBody = new RigidBody(this);
            RigidBody.IsCollisionAffected = true;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.DOOR;
        }

        public override void Draw()
        {
            if (IsActive)
            {
                if (IsOpen)
                {
                    sprite.DrawTexture(texture, 0, 0, 16, 16);
                }
                else
                {
                    sprite.DrawTexture(texture, 16, 0, 16, 16);
                }
            }
        }

        public override void Update()
        {
            Vector2 dist = ((PlayScene)Game.CurrentScene).Players[0].Position - Position;
            float distance = dist.Length;
            if (distance <= 1)
            {
                if (((PlayScene)Game.CurrentScene).Players[0].CanOpen)
                {
                    IsOpen = true;
                }
            }
        }
    }
}
