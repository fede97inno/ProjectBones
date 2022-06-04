using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class GUIItem : GameObject
    {
        public bool IsSelected;
        protected GameObject owner;
        protected Vector2 offset;

        public GUIItem(Vector2 position, string textureName, GameObject owner, float w = 0, float h = 0) : base(textureName, DrawLayer.GUI, w, h)
        {
            this.owner = owner;
            sprite.position = position;
            //sprite.Camera = CameraMngr.GetCamera("GUI");
            offset = position - owner.Position;
        }

        public void SetColor(Vector4 color)
        {
            sprite.SetMultiplyTint(color);      //facciamo di solito in biano e nero per poi modificarla con i colori dopo
        }
    }
}
