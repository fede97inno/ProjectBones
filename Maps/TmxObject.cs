﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class TmxObject : GameObject
    {
        int id;

        int xOff, yOff;


        public TmxObject(int id, int offsetX, int offsetY, float w, float h, bool solid) : base("tileset", DrawLayer.MIDDLEGROUND, w, h)
        {
            this.id = id;
            xOff = offsetX;
            yOff = offsetY;
            sprite.pivot = new OpenTK.Vector2(0,0);
            if(solid)
            {
                RigidBody = new RigidBody(this);
                RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
                RigidBody.Collider.Offset = new OpenTK.Vector2(HalfWidth, HalfHeight);
                RigidBody.Type = RigidBodyType.TILE;
            }
            DebugMngr.AddItem(RigidBody.Collider);
            IsActive = true;
        }

        public override void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, 16, 16);
            }
        }
    }
}