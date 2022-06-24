using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class HouseScene : UnderScene
    {
        public HouseScene(Map map, Player player) : base()
        {
            this.map = map;
            this.player = player;
            tmxMap = new TmxMap("Assets/Maps/house_map.tmx");
            tmxMap.DisableObjects();
            
            objects = new List<GameObject>();

            if (objects.Count > 0)
            {
                foreach (GameObject obj in objects)
                {
                    map.ToggleNode((int)obj.X, (int)obj.Y);
                } 
            }
        }
        public override void OnEnter()
        {
            base.OnEnter();
            EnableObjects();
        }

        public override void Draw()
        {
            tmxMap.Draw();
        }

        private void EnableObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].IsActive = true;
            }
        }

        private void DisableObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].IsActive = false;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            DisableObjects();
        }
    }
}
