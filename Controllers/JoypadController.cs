using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace ProjectBones
{
    class JoypadController : Controller
    {
        public JoypadController(int ctrlIndex) : base(ctrlIndex)
        {

        }

        public override float GetHorizontal()
        {
            float direction; // non devo inizializzarlo in quanto già il joystick lo setta a zero se non lo muovo

            return direction = Game.Win.JoystickAxisLeft(index).X;
        }

        public override float GetVertical()
        {
            float direction;
            if (Game.Win.JoystickUp(index))
            {
                direction = -1;
            }
            else if (Game.Win.JoystickDown(index))
            {
                direction = 1;
            }
            else
            {
                direction = Game.Win.JoystickAxisLeft(index).Y;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Win.JoystickX(index);
        }

        public override bool IsJumpPressed()
        {
            return Game.Win.JoystickA(index);
        }

        public override bool NextWeapon()
        {
            return Game.Win.JoystickShoulderRight(index);
        }

        public override bool PrevWeapon()
        {
            return Game.Win.JoystickShoulderLeft(index);
        }
    }
}
