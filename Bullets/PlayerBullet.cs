using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    class PlayerBullet : BulletSupreme
    {
        public PlayerBullet() : base("bullet")
        {
            Type = BulletType.PlayerBullet;
            RigidBody.Type = RigidBodyType.PLAYERBULLET;
            //RigidBody.AddCollisionType(RigidBodyType.ENEMY | RigidBodyType.PLAYER | RigidBodyType.TILE);
            
            sprite.scale = new Vector2(1.25f);

            shootSound = new SoundEmitter(this, "shoot");
            components.Add(ComponentType.SOUNDEMITTER, shootSound);
        }
    }
}
