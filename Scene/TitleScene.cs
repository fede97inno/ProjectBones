using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;
namespace ProjectBones
{
    class TitleScene : Scene
    {
        protected Texture texture;
        protected Sprite sprite;

        protected string texturePath;
        protected KeyCode exitKey;

        public TitleScene(String t_Path, KeyCode exit = KeyCode.Return)
        {
            texturePath = t_Path;
            this.exitKey = exit;
        }

        public override void Start()
        {
            texture = new Texture(texturePath);
            sprite = new Sprite(Game.Win.OrthoWidth * 2, Game.Win.OrthoHeight);
            sprite.position = new Vector2(-8.0f,0.0f);

            base.Start();
        }
        public override void Input()
        {
            if (Game.Win.GetKey(exitKey))
            {
                isPlaying = false;
            }
        }
        
        public override void Draw()
        {
            sprite.DrawTexture(texture);
        }

        public override Scene OnExit()
        {
            texture = null;
            sprite = null;

            return base.OnExit();
        }
    }
}
