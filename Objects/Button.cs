using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Button : GameObject
    {
        protected AudioSource audioSource;
        protected AudioClip audioClip;

        public bool IsPressed = false;

        public Button() : base("buttons", DrawLayer.BACKGROUND, 16,16)
        {
            RigidBody = new RigidBody(this);
            RigidBody.IsCollisionAffected = true;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);
            RigidBody.Type = RigidBodyType.BUTTON;
            audioSource = new AudioSource();
            audioSource.Volume = 2.0f;
            audioClip = AudioMgr.GetClip("wallMoving");
        }

        public override void Draw()
        {
            if (IsActive)
            {
                if (!IsPressed)
                {
                    sprite.DrawTexture(texture, 0, 0, 16, 16);
                }
                else
                {
                    sprite.DrawTexture(texture, 16, 0, 16, 16);
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Player)
            {
                IsPressed = true;
                audioSource.Play(audioClip);
            } 
        }
    }
}
