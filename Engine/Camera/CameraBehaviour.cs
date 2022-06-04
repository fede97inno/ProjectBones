using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    enum CameraBehaviourType { FOLLOWTARGET, FOLLOWPOINT, MOVETOPOINT, LAST}
    abstract class CameraBehaviour
    {
        protected Camera camera;
        protected Vector2 pointToFollow;
        protected float blendFactor;

        public CameraBehaviour(Camera camera)
        {
            this.camera = camera;
        }

        public virtual void  Update()
        {
            camera.position = Vector2.Lerp(camera.position, pointToFollow, blendFactor);
        }
    }
}
