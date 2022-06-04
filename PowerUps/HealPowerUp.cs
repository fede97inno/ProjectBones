using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class HealPowerUp : PowerUp
    {
        private int healthRestored = 50;
        public HealPowerUp() : base("healPowerUp")
        {

        }

        public override void OnAttach(Actor p)
        {
            base.OnAttach(p);
            attachedActor.AddHealth(healthRestored);
            OnDetach();
        }
    }
}
