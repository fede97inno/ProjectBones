using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    enum ComponentType { SOUNDEMITTER, RANDOMIZESOUNDEMITTER, ANIMATION, LAST}
    abstract class Component
    {
        protected bool isEnabled;

        public GameObject GameObject { get; protected set; }

        public bool IsEnabled { get { return isEnabled && GameObject.IsActive; } set { isEnabled = value; } }

        public Component(GameObject owner)
        {
            GameObject = owner;
        }
    }
}
