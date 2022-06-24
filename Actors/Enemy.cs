using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace ProjectBones
{
    class Enemy : Actor
    {
        protected float chaseRadius;        //radius whithin he sees player 
        protected float attackRadius;       //radius whithin he attack player must be bigger than chaseRadius

        protected ProgressBar energyBar;
        protected Vector2 pointToReach;
        protected float halfConeAngle = MathHelper.DegreesToRadians(40);
        protected float limitFuzzyValue = 0.8f;
        protected float force = 0.0f;
        public Player Rival;
        public PowerUp Target;

        private Texture iconTexture;
        private Sprite icon;
        public override int Energy
        {
            get { return base.Energy; }
            set
            {
                base.Energy = value;
                energyBar.Scale((float)value / (float)maxEnergy);
            }
        }
        public float ChaseSpeed { get; private set; }       //speed for idle walking
        public float WalkSpeed { get; private set; }        //speed when chasing player
        public int MaxEnergy { get { return maxEnergy; } }
        public Enemy() : base("enemy_0")
        {
            bulletType = BulletType.EnemyBullet;

            //RigidBody.Type = RigidBodyType.ENEMY;
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);

            chaseRadius = 5.0f;
            attackRadius = 3.0f;

            WalkSpeed = 1.5f;
            ChaseSpeed = WalkSpeed * 2;

            #region Bars
            energyBar = new ProgressBar("frameBar", "progressBar", new Vector2(Game.PixelsToUnits(4.0f), Game.PixelsToUnits(4.0f)));
            energyBar.Position = new Vector2(15.5f, 0.75f);
            #endregion

            #region Icon
            iconTexture = new Texture("Assets/enemy_0.png");
            icon = new Sprite(Game.PixelsToUnits(iconTexture.Width), Game.PixelsToUnits(iconTexture.Height));
            icon.pivot = new Vector2(icon.Width * 0.5f, icon.Height * 0.5f);
            icon.scale = new Vector2(0.50f);
            icon.Rotation = (float)MathHelper.PiOver2;
            icon.position = new Vector2(energyBar.Position.X - 0.5f, energyBar.Position.Y);
            #endregion

           // fsm = new StateMachine();

            //fsm.AddState(StateEnum.WALK, new State_Walk(this));
            //fsm.AddState(StateEnum.CHASE, new State_Chase(this));
            //fsm.AddState(StateEnum.ATTACK, new State_Attack(this));
            //fsm.AddState(StateEnum.RECHARGE, new State_Recharge(this));
            //fsm.GoTo(StateEnum.WALK);

            IsActive = true;
            Reset();
        }
        public void ComputeRandomPoint()
        {
            float randX = RandomGen.GetRandomFloat() * (Game.Win.OrthoWidth - 2) + 1;       //devo arrivare da 1 alla fine
            float randY = RandomGen.GetRandomFloat() * (Game.Win.OrthoHeight - 2) + 1;

            pointToReach = new Vector2(randX, randY);
        }

        //public bool CanSeePlayer()
        //{
        //    Player player = ((PlayScene)Game.CurrentScene).Player;
        //    Vector2 dist = player.Position - Position;
        //    if (dist.LengthSquared < chaseRadius * chaseRadius)
        //    {
        //        //check for cone angle

        //        float playerAngle = (float)Math.Acos(MathHelper.Clamp(Vector2.Dot(Forward, dist.Normalized()), -1, 1)); //clamp to avoid approximation dot error, dot return radiant
        //        return playerAngle <= halfConeAngle;
        //    }

        //    return false;
        //}
        public List<Player> GetVisiblePlayer()
        {
            List<Player> players = ((PlayScene)Game.CurrentScene).Players;
            List<Player> visiblePlayer = new List<Player>();

            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].IsAlive)
                {
                    continue;
                }

                Vector2 dist = players[i].Position - Position;

                if (dist.LengthSquared < chaseRadius * chaseRadius)
                {
                    float playerAngle = (float)Math.Acos(MathHelper.Clamp(Vector2.Dot(Forward, dist.Normalized()), -1, 1));

                    if (playerAngle <= halfConeAngle)
                    {
                        visiblePlayer.Add(players[i]);
                    }
                }
            }

            return visiblePlayer;
        }
        public List<PowerUp> GetVisiblePowerUps()
        {
            List<PowerUp> powerUps = PowerUpsMngr.ActivePowerUps;
            List<PowerUp> visibilePowerUps = new List<PowerUp>();

            if (PowerUpsMngr.ActivePowerUps != null)
            {
                for (int i = 0; i < powerUps.Count; i++)
                {
                    Vector2 dist = powerUps[i].Position - Position;

                    if (dist.LengthSquared < chaseRadius * chaseRadius)
                    {
                        float heartAngle = (float)Math.Acos(MathHelper.Clamp(Vector2.Dot(Forward, dist.Normalized()), -1, 1));

                        if (heartAngle <= halfConeAngle)
                        {
                            visibilePowerUps.Add(powerUps[i]);
                        }
                    }
                }

                return visibilePowerUps;
            }
            else
            {
                return null;
            }
        }

        public PowerUp GetBestPowerUp()
        {
            List<PowerUp> visiblePowerUps = GetVisiblePowerUps();
            float[] distances = new float[visiblePowerUps.Count];
            int index = 0;

            if (visiblePowerUps.Count > 1)
            {
                float nearest = 0;

                for (int i = 0; i < visiblePowerUps.Count; i++)
                {
                   distances[i] = (visiblePowerUps[i].Position - Position).LengthSquared;   //doppio cuore prob
                }

                nearest = float.MaxValue;

                for (int i = 0; i < distances.Length; i++)
                {
                    if (distances[i] <= nearest)
                    {
                        nearest = distances[i];
                        index = i;
                    }
                }

                return visiblePowerUps[index];
            }
            else if (visiblePowerUps.Count > 0)
            {
                return visiblePowerUps[index];
            }
            else
            {
                return null;
            }
        }

        public bool IsBetterToHeal()
        {
            Target = GetBestPowerUp();

            if (Rival != null && Target != null)
            {
                float fuzzySum = 0.0f;

                Vector2 distFromPUp = Target.Position - Position;
                Vector2 distFromPlayer = Rival.Position - Position;

                float fuzzyDist;
                float fuzzyEnergy;
                float fuzzyDistPlayer;
                float fuzzyDelta;

                fuzzyDist = 1 - (distFromPUp.LengthSquared / (chaseRadius * chaseRadius));
                fuzzyEnergy = 1 - (energy / maxEnergy);
                fuzzyDistPlayer = distFromPlayer.LengthSquared / (chaseRadius * chaseRadius);
                fuzzyDelta = 1 - ((energy / maxEnergy) - (Rival.Energy / Rival.MaxEnergy));

                fuzzySum = (fuzzyDist + fuzzyEnergy + fuzzyDistPlayer + fuzzyDelta) * 0.25f;

                Console.WriteLine(fuzzySum);

                if (fuzzySum > limitFuzzyValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public Player GetBestPlayerToFight()
        {
            List<Player> rivals = GetVisiblePlayer();

            float bestValues = 0.0f;
            int index = 0;

            if (rivals.Count > 1)
            {
                for (int i = 0; i < rivals.Count; i++)
                {
                    float fuzzyValues;
                    Vector2 dist = rivals[i].Position - Position;

                    fuzzyValues = 1 - rivals[i].Energy / rivals[i].MaxEnergy;

                    fuzzyValues += 1 - dist.LengthSquared / (chaseRadius * chaseRadius);

                    float playerAngle = (float)Math.Acos(MathHelper.Clamp(Vector2.Dot(Forward, dist.Normalized()), -1, 1));
                    fuzzyValues += 1 - playerAngle / (float)Math.PI;

                    if (fuzzyValues > bestValues)
                    {
                        bestValues = fuzzyValues;
                        index = i;
                    }
                }

                return rivals[index];
            }
            else if (rivals.Count > 0)
            {
                return rivals[index];
            }
            else
            {
                return null;
            }
        }

        public bool CanAttackPlayer()
        {
            if (Rival == null || !Rival.IsAlive)
            {
                return false;
            }

            return (Rival.Position - Position).LengthSquared < attackRadius * attackRadius;
        }
        public void HeadToPlayer()
        {
            if (Rival != null)
            {
                Vector2 direction = Rival.Position - Position;
                RigidBody.Velocity = direction.Normalized() * ChaseSpeed;
            }
        }
        public void HeadToPowerUp()
        {
            if (Target != null)
            {
                Vector2 direction = Target.Position - Position;
                RigidBody.Velocity = direction.Normalized() * ChaseSpeed;
            }
        }
        public void HeadToPoint()
        {
            Vector2 direction = pointToReach - Position;

            if (direction.LengthSquared <= 0.01f)
            {
                ComputeRandomPoint();
            }

            RigidBody.Velocity = direction.Normalized() * WalkSpeed;
        }
        public void ShootPlayer()
        {
        }
        public void LookPlayer()
        {
            if (Rival != null)
            {
                Vector2 direction = (Rival.Position - Position).Normalized();
                Forward = direction;
            }
        }
        public override void Update()
        {
            if (IsActive)
            {

            }
        }
        public override void Draw()
        {
            base.Draw();
            icon.DrawTexture(iconTexture);
        }
        public override void OnDie()
        {
            IsActive = false;
        }
    }
}
