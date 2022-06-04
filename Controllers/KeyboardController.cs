using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace ProjectBones
{
    class KeyboardController : Controller
    {
        protected KeysList keysConfig;

        public KeyboardController(int ctrlIndex, KeysList keys) : base(ctrlIndex)
        {
            keysConfig = keys;
        }

        public override float GetHorizontal()
        {
            float direction = 0.0f;

            if (Game.Win.GetKey(keysConfig.GetKey(KeyName.RIGHT)))
            {
                direction = 1.0f;
            }
            else if (Game.Win.GetKey(keysConfig.GetKey(KeyName.LEFT)))
            {
                direction = -1.0f;
            }

            return direction;
        }

        public override float GetVertical()
        {
            float direction = 0.0f;


            if (Game.Win.GetKey(keysConfig.GetKey(KeyName.DOWN)))
            {
                direction = 1.0f;
            }
            else if (Game.Win.GetKey(keysConfig.GetKey(KeyName.UP)))
            {
                direction = -1.0f;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Win.GetKey(keysConfig.GetKey(KeyName.FIRE));
        }

        public override bool IsJumpPressed()
        {
            return Game.Win.GetKey(keysConfig.GetKey(KeyName.JUMP));

        }

        public override bool NextWeapon()
        {
            return Game.Win.GetKey(KeyCode.Tab);
        }

        public override bool PrevWeapon()
        {
            return Game.Win.GetKey(KeyCode.Tab) && (Game.Win.GetKey(KeyCode.ShiftLeft) || Game.Win.GetKey(KeyCode.ShiftRight));
        }
    }
}
