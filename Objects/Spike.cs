using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Spike : GameObject
    {
        protected float counter;
        protected float timer;
        public bool IsOn = false;
        public Spike(Vector2 pos) : base("spikes", DrawLayer.MIDDLEGROUND, 16,16)
        {
            Position = pos;
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.IsCollisionAffected = true;
            RigidBody.Type = RigidBodyType.SPIKE;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);
            timer = RandomGen.GetRandomInt(2,5);
            counter = timer;
        }

        public override void Update()
        {
            if (IsActive)
            {
                counter -= Game.DeltaTime;

                if (counter <= 0)
                {
                    counter = timer;
                    IsOn = !IsOn;
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (IsOn)
            {
                if (collisionInfo.Collider is Player)
                {
                    ((Player)collisionInfo.Collider).RestartPosition();
                } 
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                if (!IsOn)
                {
                    sprite.DrawTexture(texture, 0, 0, 16, 16);
                }
                else
                {
                    sprite.DrawTexture(texture, 16, 0, 16, 16);
                }
            }
        }
    }
}
