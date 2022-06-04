using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    class EnemyBullet : BulletSupreme
    {
        public EnemyBullet() : base("bullet")
        {
            Type = BulletType.EnemyBullet;
            RigidBody.Type = RigidBodyType.ENEMYBULLET;
            RigidBody.AddCollisionType(RigidBodyType.PLAYERBULLET | RigidBodyType.PLAYER);
            sprite.SetAdditiveTint(150.0f, 60.0f, 20.0f, 0.0f);
        }
        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Player)
            {
                Player player = ((Player)collisionInfo.Collider);

                player.AddDamage(damage);
            }

            BulletMngr.RestoreBullet(this);
        }
    }
}
