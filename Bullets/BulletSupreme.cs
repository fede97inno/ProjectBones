using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    enum BulletType { PlayerBullet, Rocket, EnemyBullet,LAST}

    abstract class BulletSupreme : GameObject
    {
        protected int damage = 25;
        protected float maxSpeed = 14.0f;
        protected SoundEmitter shootSound;

        public BulletType Type { get; protected set; }

        public BulletSupreme(string texturePath,int spriteWidth = 0, int spriteHeight = 0) : base(texturePath, DrawLayer.FOREGROUND)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            RigidBody.IsGravityAffected = true;
        }

        public override void Update()
        {
            if (IsActive)
            {
                Vector2 cameraDist = Position - CameraMngr.MainCamera.position;
                Forward = RigidBody.Velocity;
                if (cameraDist.LengthSquared > Game.HalfDiagonalSquared)
                {
                    BulletMngr.RestoreBullet(this);
                }  
            }
        }

        public virtual void Reset()
        {
        }
        public override void OnCollide(Collision collisionInfo)
        {

            if (collisionInfo.Collider is Player)
            {
                Player player = ((Player)collisionInfo.Collider);

                player.AddDamage(damage);
            }

            if (collisionInfo.Collider is Tile)
            {
                Tile tile = ((Tile)collisionInfo.Collider);

                tile.IsActive = false;
            }

            BulletMngr.RestoreBullet(this);
        }
        public void Shoot(Vector2 position, Vector2 shootDir, float force)
        {
            sprite.position = position;
            RigidBody.Velocity = shootDir * maxSpeed * force;
            Forward = shootDir;

            shootSound.Play(force);
        }
    }
}
