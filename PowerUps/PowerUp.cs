using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProjectBones
{
    abstract class PowerUp : GameObject
    {
        protected Actor attachedActor;
        public PowerUp(string textureName) : base(textureName)
        {
            RigidBody = new RigidBody(this);
            //RigidBody.Type = RigidBodyType.POWERUP;
            RigidBody.IsCollisionAffected = true;

            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
           // RigidBody.AddCollisionType(RigidBodyType.PLAYER | RigidBodyType.ENEMY);

            DebugMngr.AddItem(RigidBody.Collider);
        }

        public override void Update()
        {
            if (IsActive)
            {
 
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            OnAttach((Actor)collisionInfo.Collider);
        }

        public virtual void OnAttach(Actor p)
        {
            attachedActor = p;
            IsActive = false;
        }
        public virtual void OnDetach()
        {
            if (attachedActor is Enemy)
            {
                ((Enemy)attachedActor).Target = null;
            }

            attachedActor = null;
            PowerUpsMngr.RestorePowerUp(this);
        }
    }
}
