using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class GameOverScene : Scene
    {
        protected Texture texture;
        protected Sprite sprite;

        protected string texturePath;

        public GameOverScene(String t_Path, Scene nextScene)
        {
            texturePath = t_Path;
            NextScene = nextScene;
        }

        public override void Start()
        {
            texture = new Texture(texturePath);
            sprite = new Sprite(Game.Win.Width, Game.Win.Height);

            base.Start();
        }
        public override void Input()
        {
            if (Game.Win.GetKey(KeyCode.Y))
            {
                OnExit();
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
