using OpenTK;
using Aiv.Fast2D;
using System;
using System.Xml;
using System.Collections.Generic;
using Aiv.Audio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class PlayScene : Scene
    {
        static protected Player player;
        protected static List<Player> players;
        protected static List<Tile> tiles;
        protected int alivePlayers;

        protected AudioSource audioSource;
        protected AudioClip enterClip;
        protected AudioClip exitClip;
        
        protected UnderScene currentScene;
        protected CityScene city;
        protected CaveScene cave;
        protected HouseScene house;

        protected TmxMap map;
        protected Map obstacleMap;

        protected TmxMap caveMap;
        protected Map caveObstacleMap;

        protected TmxMap houseMap;
        protected Map houseObstacleMap;

        protected int currentMapIndex;

        protected TmxMap currentMap;
        protected Map currentObstacleMap;

        public float PlayerTimer { get; protected set; }
        public float GroundY { get; protected set; }
        public float MinX { get; set; }
        public float MaxX { get; set; }
        //public Map Map { get { return currentObstacleMap; } }
        public Map Map { get { return currentScene.Map; } }
        public List<Player> Players { get { return players; } }

        #region Test TextChar
        //static TextChar w; 
        #endregion

        public PlayScene() : base()
        {

        }

        public override void Start()
        {
            LoadAssets();

            CameraLimits cameraLimits = new CameraLimits(Game.Win.OrthoWidth * 0.835f, Game.Win.OrthoWidth * 0.5f, Game.Win.OrthoHeight * 0.5f, Game.Win.OrthoHeight * 0.5f);
            CameraMngr.Init(null, cameraLimits);

            audioSource = new AudioSource();
            enterClip = AudioMgr.GetClip("enter");
            exitClip = AudioMgr.GetClip("exit");
            #region Font
            FontMngr.Init();

            Font stdFont = FontMngr.AddFont("stdFont", "Assets/Text/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMngr.AddFont("comics", "Assets/Text/comics.png", 10, 32, 61, 65);
            #endregion

            players = new List<Player>();
            tiles = new List<Tile>();

            player = new Player(Game.GetContoller(0));
            player.Position = new Vector2(10.5f, 10.5f);
            CameraMngr.SetTarget(player);

            Controller controller = Game.GetContoller(1);

            if (controller is KeyboardController)           //controllo se ho un altro controller se non c'è uso la tastiera
            {
                List<KeyCode> keys = new List<KeyCode>();
                keys.Add(KeyCode.Up);
                keys.Add(KeyCode.Down);
                keys.Add(KeyCode.Right);
                keys.Add(KeyCode.Left);
                keys.Add(KeyCode.Return);

                KeysList keyList = new KeysList(keys);
                controller = new KeyboardController(1, keyList);
            }


            players.Add(player);

            alivePlayers = players.Count;


            BulletMngr.Init();

            //FirstSceneMap
            obstacleMap = LoadTiledMap(@".\Assets\Maps\map_6.tmx");
            city = new CityScene(obstacleMap, player);
            currentScene = city;

            //SecondSceneMap
            caveObstacleMap = LoadTiledMap(@".\Assets\Maps\cave_map.tmx");
            cave = new CaveScene(caveObstacleMap, player);

            houseObstacleMap = LoadTiledMap(@".\Assets\Maps\house_map.tmx");
            house = new HouseScene(houseObstacleMap, player);

            currentMapIndex = 0;
            currentMap = map;
            currentObstacleMap = obstacleMap; 

            base.Start();
        }

        private void LoadAssets()
        {
            GfxMngr.AddTexture("hero", "Assets/Actors/hero_walk_atlas.png");
            GfxMngr.AddTexture("dog", "Assets/Actors/doggo_walk_atlas.png");

            GfxMngr.AddTexture("key", "Assets/Objects/key.png");
            GfxMngr.AddTexture("spikes", "Assets/Objects/spike.png");
            GfxMngr.AddTexture("buttons", "Assets/Objects/buttons.png");
            GfxMngr.AddTexture("wall", "Assets/Objects/wall.png");
            GfxMngr.AddTexture("door", "Assets/Objects/door.png");
            GfxMngr.AddTexture("house_door", "Assets/Objects/house_door.png");
            GfxMngr.AddTexture("cave_door", "Assets/Objects/cave_door.png");

            GfxMngr.AddTexture("frameBar", "Assets/Objects/loadingBar_frame.png");
            GfxMngr.AddTexture("progressBar", "Assets/Objects/loadingBar_bar.png");
            
            GfxMngr.AddTexture("tileset", "Assets/Maps/pixel_pack.png");

            AudioMgr.AddClip("key", "Assets/Audio/achieved.ogg");
            AudioMgr.AddClip("enter", "Assets/Audio/stair_down.ogg");
            AudioMgr.AddClip("exit", "Assets/Audio/stair_up.ogg");
            AudioMgr.AddClip("wallMoving", "Assets/Audio/wall.wav");
            AudioMgr.AddClip("openDoor", "Assets/Audio/Key Jiggle.wav");
            AudioMgr.AddClip("noKey", "Assets/Audio/hurt.ogg");
        }
        private Map LoadTiledMap(string filePath)
        {
            Map maped = null;

            XmlDocument xmlMap = new XmlDocument();

            try
            {
                xmlMap.Load(filePath);
            }
            catch (XmlException e)
            {
                Console.WriteLine("XML EXCEPTION : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION : " + e.Message);
            }

            XmlNode rootNode = xmlMap.SelectSingleNode("map");
            XmlNodeList layersNodes = rootNode.SelectNodes("layer");
            XmlNode layerNode;

            for (int i = 0; i < layersNodes.Count; i++)
            {
                string layerName = GetStringAttribute(layersNodes[i], "name");

                if (layerName == "Layer_object")
                {
                    layerNode = layersNodes[i];
                    XmlNode dataNode = layerNode.SelectSingleNode("data");

                    string csv = dataNode.InnerText;                      //legge in una stringa quello che c'è dentro un nodo
                    csv.Replace("\r\n", "").Replace("\n", "").Replace(" ", "");
                    string[] ids = csv.Split(',');
                    int[] intIds = new int[ids.Length];

                    for (int q = 0; q < ids.Length; q++)
                    {
                        intIds[q] = int.Parse(ids[q]);
                    }
                    int[] map = new int[ids.Length];

                    for (int j = 0; j < ids.Length; j++)
                    {
                        if (intIds[j] >= 1)
                        {
                            map[j] = 0;
                        }
                        else
                        {
                            map[j] = 1;
                        }
                    }

                    int width = int.Parse(layerNode.Attributes.GetNamedItem("width").Value);
                    int height = int.Parse(layerNode.Attributes.GetNamedItem("height").Value);

                    maped = new Map(width, height, map);
                }
            }
            return maped;
        }
        public override void Input()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].IsAlive)
                {
                    players[i].Input();
                }
            }
        }

        public override void Update()
        {
            PhysicsMngr.Update();
            UpdateMngr.Update();

            CameraMngr.Update();
            PhysicsMngr.CheckCollision();

            currentScene.Update();

            if (currentScene is CityScene)
            {
                if (player.Position == new Vector2(48.5f, 13.5f))
                {
                    player.Position = new Vector2(10.5f, 18.5f);
                    player.ResetPath(new Vector2(10.5f, 18.5f));
                    audioSource.Play(enterClip);
                    currentScene.OnExit();
                    currentScene = cave;
                    currentScene.OnEnter();
                }

                if (player.Position == new Vector2(4.5f, 14.5f))
                {
                    player.Position = new Vector2(7.5f, 13.5f);
                    player.ResetPath(new Vector2(7.5f, 13.5f));
                    audioSource.Play(enterClip);
                    currentScene.OnExit();
                    currentScene = house;
                    currentScene.OnEnter();
                }
            }
            else if (currentScene is CaveScene)
            {
                if (player.Position == new Vector2(10.5f, 19.5f))
                {
                    player.Position = new Vector2(48.5f, 14.5f);
                    player.ResetPath(new Vector2(48.5f, 14.5f));
                    audioSource.Play(exitClip);
                    currentScene.OnExit();
                    currentScene = city;
                    currentScene.OnEnter();
                }
            }
            else if (currentScene is HouseScene)
            {
                if (player.Position == new Vector2(7.5f, 14.5f))
                {
                    player.Position = new Vector2(4.5f, 15.5f);
                    player.ResetPath(new Vector2(48.5f, 22.5f));
                    audioSource.Play(exitClip);
                    currentScene.OnExit();
                    currentScene = city;
                    currentScene.OnEnter();
                }

                if (player.Position.X > 9 && player.Position.Y < 7)
                {
                    isPlaying = false;
                }
            }
        }

        public override void Draw()
        {
            currentScene.Draw();
            DrawMngr.Draw();
        }
        public void SetMap(TmxMap tmx, Map map)
        {
            currentMap = tmx;
            currentObstacleMap = map;
        }
        public override Scene OnExit()
        {
            CameraMngr.ClearAll();
            UpdateMngr.ClearAll();
            PhysicsMngr.ClearAll();
            DrawMngr.ClearAll();
            GfxMngr.ClearAll();
            FontMngr.ClearAll();
            DebugMngr.ClearAll();

            return base.OnExit();
        }
        public static string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }
        public virtual void OnPlayerDies()
        {
            alivePlayers--;
            if (alivePlayers <= 0)
            {
                isPlaying = false;
            }
        }
    }
}
