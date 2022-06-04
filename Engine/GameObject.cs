using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    class GameObject : I_Updatable, I_Drawable
    {
        protected Sprite sprite;
        protected Texture texture;
        protected Dictionary<ComponentType, Component> components;

        public RigidBody RigidBody;
        public bool IsActive;
        public Vector2 Forward
        {
            get { return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)); }
            set { sprite.Rotation = (float)Math.Atan2(value.Y, value.X); }
        }
        public DrawLayer Layer { get; protected set; }
        public virtual Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }
        public virtual float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public virtual float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }
        public float HalfWidth { get { return sprite.Width * 0.5f; } protected set { } }
        public float HalfHeight { get { return sprite.Height * 0.5f; } protected set { } }
        public float Width { get { return sprite.Width; } }
        public float Height { get { return sprite.Height; } }
        public virtual Camera Camera { get { return sprite.Camera; } set { sprite.Camera = value; } }
        public GameObject(string textureName, DrawLayer layer = DrawLayer.PLAYGROUND,float w = 0, float h = 0)
        {
            texture = GfxMngr.GetTexture(textureName);

            float spriteW = w != 0 ? Game.PixelsToUnits(w) : Game.PixelsToUnits(texture.Width);
            float spriteH = h != 0 ? Game.PixelsToUnits(h) : Game.PixelsToUnits(texture.Height);

            sprite = new Sprite(spriteW, spriteH);

            //HalfWidth = (int)(texture.Width * 0.5f);
            //HalfHeight = (int)(texture.Height * 0.5f);

            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            
            Layer = layer;

            components = new Dictionary<ComponentType, Component>();

            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
        }

        public virtual void Update()
        {

        }
 
        public virtual void OnCollide(Collision collisionInfo)
        {
        }
        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture);
            }
        }

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;

            UpdateMngr.RemoveItem(this);
            DrawMngr.RemoveItem(this);

            if (RigidBody != null)
            {
                RigidBody.Destroy();
            }
        }

        public void Scale(Vector2 scale)
        {
            sprite.scale = scale;
        }

        public Component GetComponent(ComponentType type) 
        {
            if (components.ContainsKey(type))
            {
                return components[type];
            }

            return null;
        }
    }
}
