using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace ProjectBones
{
    class ProgressBar : GameObject
    {
        protected Vector2 barOffset;

        protected Texture barTexture;
        protected Sprite barSprite;

        protected float barWidth;

        public override Vector2 Position { get => base.Position; 
                                           set { base.Position = value; barSprite.position = value + barOffset; } }
        public override Camera Camera { get => base.Camera; set { base.Camera = value; barSprite.Camera = value; } }
        public ProgressBar(string frameTextureName, string barTextureName, Vector2 innerBarOffset) : base(frameTextureName)
        {
            sprite.pivot = Vector2.Zero;
            IsActive = true;
            
            barOffset = innerBarOffset;
            Layer = DrawLayer.GUI;
            barTexture = GfxMngr.GetTexture(barTextureName);
            barSprite = new Sprite(Game.PixelsToUnits(barTexture.Width), Game.PixelsToUnits(barTexture.Height));

            barWidth = barTexture.Width;

            //Camera = CameraMngr.GetCamera("GUI");
        }

        public void Scale(float scale)
        {
            scale = MathHelper.Clamp(scale, 0, 1);

            barSprite.scale.X = scale;
            barWidth = barTexture.Width * scale;

            barSprite.SetMultiplyTint((1-scale) * 50, scale * 2, scale *0.5f, 1);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
                barSprite.DrawTexture(barTexture,0,0, (int)barWidth, barTexture.Height);        //la porzione da ritagliare deve essere in pixel in quanto l'immagine stessa è in pixels
            }
        }
    }
}
