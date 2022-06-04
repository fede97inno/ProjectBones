using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProjectBones
{
    enum RigidBodyType { PLAYER = 1, PLAYERBULLET = 2, ENEMY = 4, ENEMYBULLET = 8, TILE = 16, POWERUP = 32, LAST}
    class RigidBody
    {
        public GameObject GameObject;
        public Collider Collider;
        public bool IsGravityAffected = false;
        public bool IsCollisionAffected = true;
        public float Friction = 0;
        public Vector2 Velocity;
        
        public RigidBodyType Type;

        protected uint collisionMask;
        public Vector2 Position { get { return GameObject.Position; } }
        public bool IsActive { get { return GameObject.IsActive; } }
        
        public RigidBody(GameObject owner)
        {
            GameObject = owner;
           
            PhysicsMngr.AddItem(this);
        }
        public virtual void Update()
        {
            if (IsGravityAffected)
            {
                Velocity.Y += PhysicsMngr.G * Game.DeltaTime;   //accelerati verso il basso
            }

            if (GameObject is Actor)
            {
                if (((Actor)GameObject).IsGrounded)
                {
                    ApplyFriction();
                } 
            }
           

            GameObject.Position += Velocity * Game.DeltaTime;
        }
        public bool Collides(RigidBody other, ref Collision collisionInfo)
        {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }
        public void AddCollisionType(RigidBodyType type)
        {
            collisionMask |= (uint)type;
        }
        public void AddCollisionType(uint type)
        {
            collisionMask |= type;
        }
        protected void ApplyFriction()  //attrito
        {
            if (Friction > 0 && Velocity != Vector2.Zero)   //it can be frictioned
            {
                float fAmount = Friction * Game.DeltaTime;
                float newVelocityLenght = Velocity.Length - fAmount;

                if (newVelocityLenght < 0)
                {
                    Velocity = Vector2.Zero;
                    return;
                }

                Velocity = Velocity.Normalized() * newVelocityLenght;
            }
        }
        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionMask) != 0;
        }

        public void Destroy()
        {
            GameObject = null;
            Collider = null;
            PhysicsMngr.RemoveItem(this);
        }
    }
}
