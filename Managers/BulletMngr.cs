using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    static class BulletMngr
    {

        #region MK1 pre.Queue-List
        //private static Bullet[] bullets;
        //private static int maxBullets;
        //static BulletMngr()
        //{
        //    maxBullets = 10;
        //    bullets = new Bullet[maxBullets];
        //    for (int i = 0; i < maxBullets; i++)
        //    {
        //        bullets[i] = new Bullet();
        //    }
        //}
        #endregion

        private static Queue<BulletSupreme>[] bullets;

        #region MK1 Queue-List
        //static Queue<Bullet> playerBullets;             //da sparare
        //static List<Bullet> activePlayerBullets;        //già sparati

        //static Queue<EnemyBullet> enemyBullets;             
        //static List<EnemyBullet> activeEnemyBullets; 
        #endregion

        public static void Init()
        {
            int queueSize = 1;

            #region MK1 Queue-List
            //playerBullets = new Queue<Bullet>(queueSize);

            //for (int i = 0; i < queueSize; i++)
            //{
            //    playerBullets.Enqueue(new Bullet());
            //}

            //activePlayerBullets = new List<Bullet>();

            //enemyBullets = new Queue<EnemyBullet>(queueSize);

            //for (int i = 0; i < queueSize; i++)
            //{
            //    enemyBullets.Enqueue(new EnemyBullet());
            //}

            //activeEnemyBullets = new List<EnemyBullet>(); 
            #endregion

            #region MK2 Queue-List
            //bullets = new Queue<BulletSupreme>[(int)BulletType.LAST];

            //bullets[0] = new Queue<BulletSupreme>(queueSize);   //Player queue

            //for (int i = 0; i < queueSize; i++)
            //{
            //    bullets[0].Enqueue(new Bullet());
            //}

            //bullets[1] = new Queue<BulletSupreme>(queueSize);   //Enemy queue

            //for (int i = 0; i < queueSize; i++)
            //{
            //    bullets[1].Enqueue(new EnemyBullet());
            //}

            //activeBullets = new List<BulletSupreme>[(int)BulletType.LAST];

            //for (int i = 0; i < activeBullets.Length; i++)
            //{
            //    activeBullets[i] = new List<BulletSupreme>();
            //}
            #endregion

            bullets = new Queue<BulletSupreme>[(int)BulletType.LAST];

            Type[] bulletTypes = new Type[bullets.Length];

            //bulletTypes[0] = typeof(PlayerBullet);

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Queue<BulletSupreme>(queueSize);

                #region Activator Syntax
                //int pippo = (int)Activator.CreateInstance(typeof(int));       //Activator syntax 
                #endregion

                for (int j = 0; j < queueSize; j++)
                {
                    //BulletSupreme b = (BulletSupreme)Activator.CreateInstance(bulletTypes[i]);
                    //bullets[i].Enqueue(b);
                }

                #region MK1 BulletTypeSwitch
                //switch ((BulletType)i)
                //{
                //    case BulletType.PlayerBullet:
                //        for (int j = 0; j < queueSize; j++)
                //        {
                //            bullets[i].Enqueue(new PlayerBullet());
                //        }
                //        break;
                //    case BulletType.EnemyBullet:
                //        for (int j = 0; j < queueSize; j++)
                //        {
                //            bullets[i].Enqueue(new EnemyBullet());
                //        }
                //        break;
                //} 
                #endregion
            }
        }

        #region MK1 GetBullet
        //public static Bullet GetPlayerBullet()
        //{
        //    if (playerBullets.Count > 0)
        //    {
        //        Bullet bullet = playerBullets.Dequeue();
        //        activePlayerBullets.Add(bullet);

        //        return bullet;
        //    }

        //    return null;

        //}
        //public static EnemyBullet GetEnemyBullet()
        //{
        //    if (enemyBullets.Count > 0)
        //    {
        //        EnemyBullet bullet = enemyBullets.Dequeue();
        //        activeEnemyBullets.Add(bullet);

        //        return bullet;
        //    }

        //    return null;

        //} 
        //public static Bullet GetFreeBullet()
        //{
        //    for (int i = 0; i < maxBullets; i++)
        //    {
        //        if (!bullets[i].IsAlive)
        //        {
        //            return bullets[i];
        //        }
        //    }
        //    return null;
        //}
        #endregion

        #region MK2 Update-Draw
        //public static void Update()
        //{
        //    for (int i = 0; i < activeBullets.Length; i++)
        //    {
        //        for (int j = 0; j < activeBullets[i].Count; j++)
        //        {
        //            activeBullets[i][j].Update();
        //        }
        //    }
        //}

        //public static void Draw()
        //{
        //    for (int i = 0; i < activeBullets.Length; i++)
        //    {
        //        for (int j = 0; j < activeBullets[i].Count; j++)
        //        {
        //            activeBullets[i][j].Draw();
        //        }
        //    }
        //} 
        #endregion

        #region MK1 Update-Draw

        //public static void Update()
        //{
        //    for (int i = 0; i < maxBullets; i++)
        //    {
        //        if (bullets[i].IsAlive && bullets[i].SpritePos.X <= Game.Win.Width)
        //        {
        //            bullets[i].Update();
        //        }
        //        else
        //        {
        //            bullets[i].IsAlive = false;
        //        }
        //    }
        //} 
        //public static void Draw()
        //{
        //    for (int i = 0; i < maxBullets; i++)
        //    {
        //        if (bullets[i].IsAlive)
        //        {
        //            bullets[i].Draw();
        //        }
        //    }
        //}
        #endregion

        #region MK1 RestoreBullet
        //public static void RestorePlayerBullet(Bullet bullet)
        //{
        //    activePlayerBullets.Remove(bullet);
        //    playerBullets.Enqueue(bullet);
        //}
        //public static void RestoreEnemyBullet(EnemyBullet bullet)
        //{
        //    activeEnemyBullets.Remove(bullet);
        //    enemyBullets.Enqueue(bullet);
        //} 
        #endregion

        public static void RestoreBullet(BulletSupreme bullet)
        {
            bullet.Reset();
            bullet.IsActive = false;
            bullets[(int)bullet.Type].Enqueue(bullet);
        }
        public static BulletSupreme GetBullet(BulletType type)
        {
            int index = (int)type;
            if (bullets[index].Count > 0)
            {
                BulletSupreme bullet = bullets[index].Dequeue();
                bullet.IsActive = true;

                return bullet;
            }
            return null;
        }
        public static void ClearAll()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Clear();
            }
        }
    }
}
