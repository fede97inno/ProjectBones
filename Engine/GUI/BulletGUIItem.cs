using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class BulletGUIItem : GUIItem
    {
        protected int numBullets;
        protected TextObject numBulletsTxt;
        protected bool isInfinite;
        protected bool isAvailable;

        public bool IsAvailable
        {
            get { return isAvailable; }
            set
            {
                isAvailable = value;
                numBulletsTxt.IsActive = isAvailable; 
                if (isAvailable)
                {
                    SetColor(Vector4.One);
                }
                else
                {
                    SetColor(new Vector4(1.0f, 0.0f, 0.0f, 0.4f));
                }
            } 
        }
        public bool IsInfinite { get { return isInfinite; } set { isInfinite = value; numBulletsTxt.IsActive = !isInfinite; } }     //se è infinito nascondiamo il numero
        public int NumBullets
        {
            get { return numBullets; }
            set
            {
                numBullets = value; 

                if (numBullets <= 0)
                {
                    IsAvailable = false;
                }
                else
                {
                    numBulletsTxt.Text = numBullets.ToString();
                    
                    if (!IsAvailable)
                    {
                        IsAvailable = true;
                    }
                }
            } 
        }
        public BulletGUIItem(Vector2 position, string textureName, GameObject owner, int numBullets,float w = 0, float h = 0) : base(position, textureName, owner, w, h)
        {
            numBulletsTxt = new TextObject(new Vector2(position.X - 0.1f, position.Y));
            NumBullets = numBullets;
            IsActive = true;
        }

        public void DecrementBullets()
        {
            NumBullets--;
        }
    }
}
