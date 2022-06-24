using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class CityScene : UnderScene
    {
        protected Door door;
        public CityScene(Map map, Player player) : base()
        {
            this.map = map;
            this.player = player;
            tmxMap = new TmxMap("Assets/Maps/map_6.tmx");
            objects = new List<GameObject>();

            door = new Door(new OpenTK.Vector2(4.5f, 14.5f));
            objects.Add(door);

            foreach (GameObject obj in objects)
            {
                map.ToggleNode((int)obj.X, (int)obj.Y);
            }
        }

        public override void Update()
        {
            if (door.IsOpen == true)
            {
                map.ToggleNode(4,14);
            }
        }
        public override void OnEnter()
        {
            base.OnEnter();
            EnableObjects();
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
        public override void Draw()
        {
            tmxMap.Draw();
        }
    }
}
