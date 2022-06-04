using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    abstract class Scene
    {
        public bool isPlaying { get; protected set; }
        public Scene NextScene;

        public Scene()
        {

        }

        public virtual void Start()
        {
            isPlaying = true;
        }

        public virtual Scene OnExit()
        {
            isPlaying = false;
            return NextScene;
        }

        public abstract void Input();
        public virtual void Update() { }
        public abstract void Draw();

    }
}
