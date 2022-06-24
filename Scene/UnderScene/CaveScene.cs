using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class CaveScene : UnderScene
    {
        protected Key key;
        protected Wall wall;
        public CaveScene(Map map, Player player) : base()
        {
            this.map = map;
            this.player = player;
            tmxMap = new TmxMap("Assets/Maps/cave_map.tmx");
            tmxMap.DisableObjects();

            objects = new List<GameObject>();
            key = new Key();
            key.Position = new OpenTK.Vector2(17.5f, 3.5f);

            Button button = new Button();
            button.Position = new OpenTK.Vector2(7.5f,4.5f);

            wall = new Wall(button);
            wall.Position = new OpenTK.Vector2(16.5f, 14.5f);

            objects.Add(button);
            objects.Add(wall);
            objects.Add(key);

            foreach (GameObject obj in objects)
            {
                if (obj is Key || obj is Button)
                {
                    continue;
                }

                map.ToggleNode((int)obj.X, (int)obj.Y);
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
            if (key.HasBeenPickedUp)
            {
                objects.Remove(key);
            }
            if (wall.button.IsPressed)
            {
                objects.Remove(wall);
            }
        }

        public override void Update()
        {
            base.Update();
            CheckInactive();
        }

        private void CheckInactive()
        {
            foreach (var obj in objects)
            {
                if (obj.IsActive == false)
                {
                    if (obj is Wall && ((Wall)obj).ReActiveNode == false)
                    {
                        ((Wall)obj).ReActiveNode = true;
                        map.ToggleNode(16, 14);
                        //objects.Remove(obj);
                    }
                }
            }
        }
    }
}
