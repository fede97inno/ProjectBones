using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    static class PowerUpsMngr
    {
        static List<PowerUp> items;
        static float nextSpawn;

        public static List<PowerUp> ActivePowerUps { get; private set; }

        public static void Init()
        {
            ActivePowerUps = new List<PowerUp>();
            items = new List<PowerUp>();

            nextSpawn = RandomGen.GetRandomFloat() * 8.0f + 3.0f;
        }

        public static void Update()
        {
            nextSpawn -= Game.DeltaTime;

            if (nextSpawn <= 0)
            {
                SpawnPowerUp();
                nextSpawn = RandomGen.GetRandomFloat() * 8.0f + 3.0f;
            }
        }

        public static void SpawnPowerUp()
        {
            if (items.Count > 0)
            {
                int randomIndex = RandomGen.GetRandomInt(0, items.Count);

                PowerUp randPowerUp = items[randomIndex];
                items.RemoveAt(randomIndex);
                ActivePowerUps.Add(randPowerUp);
                randPowerUp.Position = new Vector2(RandomGen.GetRandomInt((int)(randPowerUp.HalfWidth) + 1, (int)(Game.Win.OrthoWidth - randPowerUp.HalfWidth - 1)),
                                                   RandomGen.GetRandomInt((int)randPowerUp.HalfHeight + 1, (int)(Game.Win.OrthoHeight - randPowerUp.HalfHeight - 1)));
                randPowerUp.IsActive = true;
            }
        }

        public static void RestorePowerUp(PowerUp pUp)
        {
            pUp.IsActive = false;
            ActivePowerUps.Remove(pUp);
            items.Add(pUp);
        }

        public static void ClearAll()
        {
            items.Clear();
        }
    }
}
