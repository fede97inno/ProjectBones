using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Groundable : GameObject
    {
        public bool IsGrounded
        {
            get { return !RigidBody.IsGravityAffected; }        //is gravityaffected only when it is in air
            set { RigidBody.IsGravityAffected = !value; }
        }
        public Groundable(string textureName, DrawLayer layer = DrawLayer.PLAYGROUND, float w = 0, float h = 0) : base(textureName, layer, w, h)
        {
            RigidBody = new RigidBody(this);
        }

        public void OnGrounded()
        {
            IsGrounded = true;
            RigidBody.Velocity.Y = 0.0f;
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Groundable)
            {
                if (collisionInfo.Delta.X < collisionInfo.Delta.Y)  //horiziontal collision
                {
                    if (X < collisionInfo.Collider.X) //from left
                    {
                        collisionInfo.Delta.X = -collisionInfo.Delta.X;
                    }

                    X += collisionInfo.Delta.X;
                    RigidBody.Velocity.X = 0.0f;
                }
                else //vertical collision
                {
                    if (!IsGrounded && ((Groundable)collisionInfo.Collider).IsGrounded)     //se non sono già fermo io nè l'altro oggetto
                    {
                        if (Y < collisionInfo.Collider.Y) //from top
                        {
                            collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                            OnGrounded();
                            Y += collisionInfo.Delta.Y;
                        }
                    }
                }
            }
        }

        public override void Update()
        {
            if (IsActive)
            {

            }
        }
    }
}
