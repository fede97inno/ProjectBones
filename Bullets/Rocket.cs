using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Rocket : BulletSupreme
    {
        protected float startEngineAngle;
        protected bool engineIsOn;
        protected AudioClip engineSound;
        public Rocket() : base("rocket")
        {
            Type = BulletType.Rocket;
            RigidBody.Type = RigidBodyType.PLAYERBULLET;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER | RigidBodyType.TILE);
            maxSpeed = 12.0f;

            damage = 30;
            
            startEngineAngle = -0.174533f;

            shootSound = new SoundEmitter(this, "missileWhistle");
            engineSound = GfxMngr.GetClip("missileStartEngine"); 

            components.Add(ComponentType.SOUNDEMITTER, shootSound); //Questo pattern in components accetta solo un tipo di ComponentType
        }

        public override void Update()
        {
            base.Update();

            if (IsActive)
            {
                if (!engineIsOn && (sprite.Rotation > startEngineAngle || sprite.Rotation < -Math.PI - startEngineAngle))
                {
                    shootSound.Play(1.0f, clipToPlay:engineSound);
                    engineIsOn = true;
                    RigidBody.Velocity.X = Math.Sign(RigidBody.Velocity.X) * 18.0f;
                }
            }
        }

        public override void Reset()
        {
            base.Reset();
            engineIsOn = false;
        }
    }
}
