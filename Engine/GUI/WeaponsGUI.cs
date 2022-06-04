﻿using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class WeaponsGUI : GameObject
    {
        protected BulletGUIItem[] weapons;
        protected string[] textureNames = { "bullet_ico", "rocket_ico" };

        protected int selectedWeapon;
        protected Sprite selection;
        protected Texture selectionTexture;
        protected float itemWidth;

        public int SelectedWeapon 
        { 
            get { return selectedWeapon; } 
            protected set { selectedWeapon = value; selection.position = weapons[selectedWeapon].Position; } 
        }

        public WeaponsGUI(Vector2 position) : base("weapons_frame", DrawLayer.GUI)
        {
            sprite.pivot = Vector2.Zero;
            sprite.position = position;
            //sprite.Camera = CameraMngr.GetCamera("GUI");

            weapons = new BulletGUIItem[textureNames.Length];

            selectionTexture = GfxMngr.GetTexture("weapon_selection");
            itemWidth = Game.PixelsToUnits(selectionTexture.Width);      //è la stessa della selectionTexture
            selection = new Sprite(itemWidth, itemWidth);
            selection.pivot = new Vector2(itemWidth * 0.5f);
            selection.Camera = sprite.Camera;

            float itemPosY = position.Y + HalfHeight;
            float itemsHorizontalDistance = Game.PixelsToUnits(7);
            //float firstItemPosX = itemsHorizontalDistance;

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i] = new BulletGUIItem(new Vector2(position.X + itemsHorizontalDistance + itemWidth * 0.5f + i * itemWidth, itemPosY), textureNames[i], this, 2);
            }

            SelectedWeapon = 0;

            weapons[0].IsSelected = true;
            weapons[0].IsAvailable = true;
            weapons[0].IsInfinite = true;
        }

        public override void Draw()
        {
            base.Draw();
            selection.DrawTexture(selectionTexture);
        }

        public BulletType NextWeapon(int direction = 1)
        {
            int currentWeapon = selectedWeapon;

            do
            {
                selectedWeapon += direction;
                
                if (selectedWeapon >= weapons.Length)
                {
                    selectedWeapon = 0;
                }
                else if (selectedWeapon < 0)
                {
                    selectedWeapon = weapons.Length - 1;
                }

            } while (!weapons[selectedWeapon].IsAvailable && selectedWeapon != currentWeapon);

            SelectedWeapon = selectedWeapon;

            return (BulletType)selectedWeapon;
        }

        public BulletType DecrementBullets()
        {
            if (!weapons[selectedWeapon].IsInfinite)
            {
                weapons[selectedWeapon].DecrementBullets();

                if (!weapons[selectedWeapon].IsAvailable)
                {
                    NextWeapon();
                }
            }

            return (BulletType)selectedWeapon;
        }

        public void AddBullets(BulletType type, int amount)
        {
            if (!weapons[(int)type].IsInfinite)
            {
                weapons[(int)type].NumBullets += amount; 
            }
        }
    }
}
