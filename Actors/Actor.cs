using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    abstract class Actor : GameObject
    {
        protected BulletType bulletType;

        protected int energy;
        protected int maxEnergy;
        protected float maxSpeed;
        protected Vector2 shootOffset;
        protected ProgressBar energyBar;

        public Agent Agent;
        public bool IsGrounded { get { return true; } }
        public bool IsAlive { get { return energy > 0; } }
        public virtual int Energy { get { return energy; } set { energy = MathHelper.Clamp(value, 0, maxEnergy); } }
        public BulletSupreme LastShotBullet { get; protected set; }
        protected Actor(string textureName, float w = 0, float h = 0) : base(textureName, w: w, h: h)
        {
            maxEnergy = 100;
            RigidBody = new RigidBody(this);
            energyBar = new ProgressBar("frameBar", "progressBar", new Vector2(Game.PixelsToUnits(4.0f), Game.PixelsToUnits(4.0f)));
            energyBar.Position = new Vector2(1.0f, 0.75f);
            energyBar.IsActive = true;

            shootOffset = Vector2.Zero;
        }

        protected virtual void Shoot(Vector2 direction,float force)
        {

            BulletSupreme b = BulletMngr.GetBullet(bulletType);
            if (b != null)
            {
                b.IsActive = true;
                b.Shoot(sprite.position + shootOffset, direction, force);
            }
            
            LastShotBullet = b;
        }

        public virtual void AddDamage(int damage)
        {
            Energy -= damage;

            if (Energy <= 0)
            {
                OnDie();
            }
        }
        public virtual void AddHealth(int regen)
        {
            Energy += regen;

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
        abstract public void OnDie();

        public virtual void Reset()
        {
            Energy = maxEnergy;
        }
        public override void Update()
        {
            base.Update();

            //X = MathHelper.Clamp(Position.X, playScene.MinX, playScene.MaxX);
        }

        public override void OnCollide(Collision collisionInfo)
        {
            OnWallCollides(collisionInfo);
        }

        protected virtual void OnWallCollides(Collision collisionInfo)
        {
            if (collisionInfo.Delta.X < collisionInfo.Delta.Y)
            {
                // Horizontal Collision
                if (X < collisionInfo.Collider.X)
                {
                    // Collision from Left (inverse horizontal delta)
                    collisionInfo.Delta.X = -collisionInfo.Delta.X;
                }

                X += collisionInfo.Delta.X;
                RigidBody.Velocity.X = 0.0f;
            }
            else
            {
                // Vertical Collision
                if (Y < collisionInfo.Collider.Y)
                {
                    // Collision from Top
                    collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                    RigidBody.Velocity.Y = 0.0f;
                }
                else
                {
                    // Collision from Bottom
                    RigidBody.Velocity.Y = -RigidBody.Velocity.Y * 0.8f;
                }

                Y += collisionInfo.Delta.Y;
            }
        }
        #region OnPlatform MK1
        //public override void OnCollide(Collision collisionInfo)
        //{
        //    OnPlatformCollision(collisionInfo);
        //}

        //public virtual void OnPlatformCollision(Collision collisionInfo)
        //{
        //    if (collisionInfo.Delta.X < collisionInfo.Delta.Y)              //Horizontal collision
        //    {
        //        if (X < collisionInfo.Collider.Position.X)                  //collision from left ,horizontal 
        //        {
        //            collisionInfo.Delta.X = -collisionInfo.Delta.X;         //inverto il segno perchè devo spostare l'oggetto verso destra in quanto sta collidendo da sinistra
        //        }

        //        X += collisionInfo.Delta.X;
        //        RigidBody.Velocity.X = 0.0f;
        //    }
        //    else                                                            //Vertical collision
        //    {
        //        if (Y < collisionInfo.Collider.Position.Y)                  //collision from top
        //        {
        //            collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
        //            RigidBody.Velocity.Y = 0.0f;
        //        }
        //        else                                                        //collision from bottom
        //        {
        //            RigidBody.Velocity.Y = -RigidBody.Velocity.Y * 0.8f;    //bounce on collision from bottom
        //        }

        //        Y += collisionInfo.Delta.Y;
        //    }
        //} 
        #endregion
    }
}
