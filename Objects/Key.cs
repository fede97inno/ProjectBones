using System;
using Aiv.Audio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Key : GameObject
    {
        protected AudioSource audioSource;
        protected AudioClip audioClip;
        
        public bool HasBeenPickedUp = false;
        public Key() : base("key", DrawLayer.PLAYGROUND, 16, 16)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.IsCollisionAffected = true;
            RigidBody.Type = RigidBodyType.KEY;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);

            audioSource = new AudioSource();
            audioClip = AudioMgr.GetClip("key");
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Player)
            {
                ((Player)collisionInfo.Collider).PickUpKey();
                IsActive = false;
                audioSource.Play(audioClip);
                HasBeenPickedUp = true;
            }
        }

        public override void Draw()
        {
                base.Draw();
        }
    }
}
