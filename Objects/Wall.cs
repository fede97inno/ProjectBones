using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Wall : GameObject
    {
        protected float accumulator;


        public bool ReActiveNode = false;
        public Button button;

        public Wall(Button but) : base("wall", DrawLayer.PLAYGROUND, 16,16)
        {
            button = but;

        }

        public override void Update()
        {
            if (button.IsPressed == true)
            {
                X += Game.DeltaTime;
                accumulator += Game.DeltaTime;
                if (accumulator > 1)
                {
                    IsActive = false;
                }
            }
        }
    }
}
