using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class MoveToPointBehaviour : CameraBehaviour            //arriva a un punto non lo segue e ci mette un determinato tempo deciso da noi, non importa quanto sia grande la distanza da coprire ci mette
    {                                                       //comunque il determinato tempo deciso in partenza
        protected float counter;
        protected float duration;
        protected Vector2 cameraStartPosition;

        public MoveToPointBehaviour(Camera camera) : base(camera)
        {

        }

        public virtual void MoveTo(Vector2 point, float movementDuration)
        {
            duration = movementDuration;
            cameraStartPosition = camera.position;
            pointToFollow = point;
            counter = 0;
            blendFactor = 0;
        }

        public override void Update()
        {
            counter += Game.DeltaTime;

            if (counter >= duration)
            {
                counter = duration;
                CameraMngr.OnMovementEnd();
            }

            blendFactor = counter / duration;   //normalizzo il valore del contatore per sapere il tempo passato
            camera.position = Vector2.Lerp(cameraStartPosition, pointToFollow, blendFactor); //deve lerpare da dove è partita
        }
    }
}
