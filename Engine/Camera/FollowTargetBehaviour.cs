﻿using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class FollowTargetBehaviour : CameraBehaviour
    {
        public GameObject Target;
        public float CameraSpeed = 5.0f;
        public FollowTargetBehaviour(Camera camera, GameObject target) : base(camera)
        {
            Target = target;
        }

        public override void Update()
        {
            if (Target != null)
            {
                pointToFollow = Target.Position;
                blendFactor = Game.DeltaTime * CameraSpeed;

                base.Update();          
            }
        }
    }
}
