using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class UnderScene 
    {
        protected Map map;
        protected TmxMap tmxMap;
        protected Player player;
        protected List<GameObject> objects;

        public Map Map { get { return map; } }
        public List<GameObject> Objects { get { return objects; } }

        public UnderScene()
        {

        }

        public virtual void Update() { }
        public virtual void OnEnter()
        {
            tmxMap.EnableObjects();
        }
        public virtual void OnExit()
        {
            tmxMap.DisableObjects();
        }
        public virtual void Draw() { }
    }
}
