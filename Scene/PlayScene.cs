using OpenTK;
using Aiv.Fast2D;
using System;
using System.Xml;
using System.Collections.Generic;
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
        protected int currentPlayerIndex;

        protected TmxMap map;
        protected Map obstacleMap;

        public float PlayerTimer { get; protected set; }
        public float GroundY { get; protected set; }
        public float MinX { get; set; }
        public float MaxX { get; set; }
        public Map Map { get { return obstacleMap; } }
        public Player CurrentPlayer { get { return players[currentPlayerIndex]; } }
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

            //MinX = 1;
            // MaxX = cameraLimits.MaxX + 8;

            //CameraMngr.AddCamera("GUI", new Camera());          //blocked
           // CameraMngr.AddCamera("Bg_0", cameraSpeed: 0.9f);
            //CameraMngr.AddCamera("Bg_1", cameraSpeed: 0.95f);

            #region Font
            FontMngr.Init();

            Font stdFont = FontMngr.AddFont("stdFont", "Assets/Text/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMngr.AddFont("comics", "Assets/Text/comics.png", 10, 32, 61, 65);
            #endregion

            players = new List<Player>();
            tiles = new List<Tile>();

            player = new Player(Game.GetContoller(0));
            player.Position = new Vector2(10.0f, 10.0f);
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
            ////CameraMngr.Target = Player;

            //Enemy = new Enemy();
            //Enemy.Position = new Vector2(8, 4);


            BulletMngr.Init();
            //SpawnMngr.Init();
            //PowerUpsMngr.Init();

            map = new TmxMap("Assets/Maps/map_4.tmx");



            LoadTiledMap();
            base.Start();
        }

        private void LoadAssets()
        {
            GfxMngr.AddTexture("hero", "Assets/Actors/hero_idle_d.png");
            GfxMngr.AddTexture("frameBar", "Assets/Objects/loadingBar_frame.png");
            GfxMngr.AddTexture("progressBar", "Assets/Objects/loadingBar_bar.png");
            
            GfxMngr.AddTexture("tileset", "Assets/Maps/pixel_pack.png");
            
            //GfxMngr.AddTexture("tileset", "Assets/Maps/pixel_pack_but_better.png");
            //GfxMngr.AddTexture("weapons_frame", "Assets/weapons_GUI_frame.png");
            //GfxMngr.AddTexture("weapon_selection", "Assets/weapon_GUI_selection.png");
        }
        private void LoadTiledMap()
        {
            XmlDocument xmlMap = new XmlDocument();

            try
            {
                xmlMap.Load(@".\Assets\Maps\map_4.tmx");
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

                    obstacleMap = new Map(width, height, map);
                }
            }
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

            //SpawnMngr.Update();
            //PowerUpsMngr.Update();
            CameraMngr.Update();
            PhysicsMngr.CheckCollision();
        }

        public override void Draw()
        {
            DrawMngr.Draw();
            //DebugMngr.Draw();
        }

        public override Scene OnExit()
        {
            //BulletMngr.ClearAll();
            //SpawnMngr.ClearAll();
            PowerUpsMngr.ClearAll();
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
