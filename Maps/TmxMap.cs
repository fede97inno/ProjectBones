using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aiv.Fast2D;

namespace ProjectBones
{
    class TmxMap : I_Drawable
    {
        private string tmxFilePath;
        public DrawLayer Layer { get; }

        // Tileset
        TmxTileset tileset;
        // Tilelayer
        TmxTileLayer tileLayer;
        // MultiLayers
        TmxTileLayer[] tileLayers;



        public TmxMap(string filePath)
        {
            // Map Drawing Settings
            Layer = DrawLayer.BACKGROUND;

            DrawMngr.AddItem(this);

            // CREATE AND LOAD XML DOCUMENT FROM TMX MAP FILE
            tmxFilePath = filePath;

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(tmxFilePath);
            }
            catch(XmlException e)
            {
                Console.WriteLine("XML Exception: " + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }

            // PROCEED TO XML DOCUMENT NODES PARSING
            // Map Node and Attributes
            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            int mapCols = GetIntAttribute(mapNode, "width");
            int mapRows = GetIntAttribute(mapNode, "height");
            int mapTileW = GetIntAttribute(mapNode, "tilewidth");
            int mapTileH = GetIntAttribute(mapNode, "tileheight");

            // Tileset Node and Attributes
            XmlNode tilesetNode = mapNode.SelectSingleNode("tileset");
            int tilesetTileW = GetIntAttribute(tilesetNode, "tilewidth");
            int tilesetTileH = GetIntAttribute(tilesetNode, "tileheight");
            int spacing = GetIntAttribute(tilesetNode, "spacing");
            int margin = GetIntAttribute(tilesetNode, "margin");
            int tileCount = GetIntAttribute(tilesetNode, "tilecount");
            int tilesetCols = GetIntAttribute(tilesetNode, "columns");
            int tilesetRows = tileCount / tilesetCols;
            // Create Tileset from collected data
            tileset = new TmxTileset("tileset", tilesetCols, tilesetRows, tilesetTileW, tilesetTileH, spacing, margin);

            // MULTILAYER (TILES & TILEOBJECTS LAYERS)
            XmlNodeList layersNodes = mapNode.SelectNodes("layer");

            tileLayers = new TmxTileLayer[layersNodes.Count];

            for (int i = 0; i < layersNodes.Count; i++)
            {
                string layerName = GetStringAttribute(layersNodes[i], "name");

                if (layerName == "Layer_object")
                {
                    TmxTileObjectLayer tileObjectLayer = new TmxTileObjectLayer(layersNodes[i], tilesetNode, tileset);
                }
                else
                {
                    tileLayers[i] = new TmxTileLayer(layersNodes[i], tileset, mapCols, mapRows, mapTileW, mapTileH);
                }
            }
        }

        public static int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node, attrName));
        }
        public static float GetFloatAttribute(XmlNode node, string attrName)
        {
            return float.Parse(GetStringAttribute(node, attrName));
        }
        public static bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }

        public static string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }

        public void Draw()
        {
            for (int i = 0; i < tileLayers.Length; i++)
            {
                if (i == tileLayers.Length - 1)
                {
                    continue;
                }

                tileLayers[i].Draw();
            }
        }
    }
}
