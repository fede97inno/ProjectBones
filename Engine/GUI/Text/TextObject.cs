using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ProjectBones
{
    class TextObject
    {
        protected List<TextChar> sprites;
        protected string text;
        protected bool isActive;
        protected Font font;
        protected int horiSpace;

        protected Vector2 position;
        public bool IsActive { get { return isActive; } set { isActive = value; UpdateCharStatus(value); } }
        public string Text { get { return text; } set { SetText(value); } }

        public TextObject(Vector2 position, string textString = "", Font font = null, int horiSpace = 0)
        {
            this.position = position;
            this.horiSpace = horiSpace;

            if (font == null)
            {
                font = FontMngr.GetFont();
            }

            this.font = font;

            sprites = new List<TextChar>();

            if (textString != "")
            {
                SetText(textString);
            }
        }

        private void SetText(string newText)
        {
            if (newText != text)
            {
                text = newText;
                int numChars = text.Length;
                float charX = position.X;
                float charY = position.Y;

                for (int i = 0; i < numChars; i++)
                {
                    char c = text[i];       //string as a char array

                    if (i > sprites.Count - 1)  //aggiungo se più lunga
                    {
                        TextChar tc = new TextChar(new Vector2(charX, charY), c, font);
                        tc.IsActive = true;
                        sprites.Add(tc);
                    }
                    else if (c != sprites[i].Character) //cambio se diversa
                    {
                        sprites[i].Character = c;
                    }

                    charX += horiSpace + sprites[i].HalfWidth * 2f;
                }

                if (sprites.Count > text.Length)
                {
                    int count = sprites.Count - text.Length;
                    int startCut = text.Length;
                    for (int j = startCut; j < sprites.Count; j++)
                    {
                        sprites[j].Destroy();
                    }
                    sprites.RemoveRange(startCut, count);
                }
            }
        }

        protected virtual void UpdateCharStatus(bool activeStatus)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].IsActive = activeStatus;
            }
        }
    }
}
