using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    enum AnimationPlayer { DOWN, RIGHT, LEFT, UP, IDLE, LAST }
    class Player : Actor
    {
        private bool isFirePressed;

        private ProgressBar forceBar;
        protected TextObject playerName;
        protected int playerID;
        protected int score = 0;
        protected float jumpSpeed = -8;
        protected Controller controller;
        protected float inc = 1;
        protected bool wasPressed = false;
        protected float maxForce = 1.0f;
        protected float shootForce;
        protected bool hasKey = false;
        protected AudioSource audioSource;
        protected AudioClip audioClip;
        protected AudioClip hasNotKeyClip;

        protected List<Animation> animationList;
        protected Animation idle;
        protected AnimationPlayer currentAnimationSet;
        protected Animation currentAnimation;

        protected bool isClicked = false;
        protected bool isArrived = true;
        protected Vector2 dest;

        protected bool camoClicked = false;
        protected bool isCamo = false;

        protected bool openClicked = false;

        public bool CanOpen = false;
        public bool IsCamo { get { return isCamo; } }
        public bool IsFalling { get { return RigidBody.Velocity.Y > 0; } }
        public int PlayerID { get { return playerID; } }
        public int MaxEnergy { get { return maxEnergy; } }
        public override int Energy
        {
            get { return base.Energy; }
            set
            {
                base.Energy = value;
                energyBar.Scale((float)value / (float)maxEnergy);
            }
        }
        public Player(Controller ctrl, int id = 0) : base("hero", 16, 16)
        {
            IsActive = true;
            maxSpeed = 10;
            isFirePressed = false;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.PLAYER;
            RigidBody.AddCollisionType(RigidBodyType.DOOR);
            RigidBody.Friction = 20;
            RigidBody.IsMovingWithAStar = true;
            controller = ctrl;
            playerID = id;

            Agent = new Agent(this);
            audioSource = new AudioSource();
            audioClip = AudioMgr.GetClip("openDoor");
            hasNotKeyClip = AudioMgr.GetClip("noKey");
            #region Bars

            //Vector2 playerNamePos = energyBar.Position + new Vector2(0, Game.PixelsToUnits(-25));
            //playerName = new TextObject(playerNamePos, $"Player {playerID + 1}", FontMngr.GetFont());
            //playerName.IsActive = false;
            //Vector2 scoreNamePos = energyBar.Position + new Vector2(0, 25);
            //scoreText = new TextObject(scoreNamePos, "", FontMngr.GetFont());
            //scoreText.IsActive = false;

            //forceBar = new ProgressBar("frameBar", "progressBar", new Vector2(Game.PixelsToUnits(4.0f), Game.PixelsToUnits(4.0f)));
            //forceBar.Camera = sprite.Camera;
            //forceBar.Position = new Vector2(Position.X, Position.Y + 0.2f);
            //forceBar.IsActive = false;
            #endregion
            //energyBar.Position = new Vector2(id * 3 + 0.2f, 0.5f);
            energyBar.IsActive = false;
            //playerName = new TextObject(new Vector2(energyBar.Position.X, 0.25f), $"Player {id + 1}");

            idle = new Animation(this, 1, 16, 16, 15, Vector2.Zero);
            animationList = new List<Animation>();

            for (int i = 0; i < (int)AnimationPlayer.LAST - 1; i++)
            {
                animationList.Add(new Animation(this, 4, 16, 16, 10, new Vector2(0, i * 16), true));
                animationList[i].IsEnabled = true;
            }

            animationList.Add(idle);
            animationList[(int)AnimationPlayer.IDLE].IsEnabled = true;
            currentAnimationSet = AnimationPlayer.IDLE;
            currentAnimation = animationList[(int)currentAnimationSet];
            currentAnimation.IsEnabled = true;

            currentAnimation.Play();
            Reset();

            DebugMngr.AddItem(RigidBody.Collider);
        }

        public void Input()
        {
            #region INPUT MK1
            //if (Game.Win.GetKey(KeyCode.W) && sprite.position.Y - sprite.Height * 0.5f >= 0)
            //{
            //    RigidBody.Velocity.Y = -maxSpeed;
            //}
            //else if (Game.Win.GetKey(KeyCode.S) && sprite.position.Y + sprite.Height * 0.5f <= Game.Win.Height)
            //{
            //    RigidBody.Velocity.Y = maxSpeed;
            //}
            //else
            //{
            //    RigidBody.Velocity.Y = 0;
            //}

            //if (Game.Win.GetKey(KeyCode.A) && sprite.position.X - sprite.Width * 0.5f >= 0)
            //{
            //    RigidBody.Velocity.X = -maxSpeed;
            //}
            //else if (Game.Win.GetKey(KeyCode.D) && sprite.position.X + sprite.Width * 0.5f <= Game.Win.Width)
            //{
            //    RigidBody.Velocity.X = maxSpeed;
            //}
            //else
            //{
            //    RigidBody.Velocity.X = 0;
            //}


            //if (Game.Win.GetKey(KeyCode.Space))
            //{
            //    if (spacePressed)
            //    {
            //        return;
            //    }
            //    spacePressed = true;
            //    Shoot();
            //}
            //else
            //{
            //    spacePressed = false;
            //} 
            #endregion

            float horiDirection = controller.GetHorizontal();
            float vertDirection = controller.GetVertical();

            #region Keyboard
            //if (horiDirection != 0 && !isFirePressed)
            //{
            //    RigidBody.Velocity.X = horiDirection * maxSpeed;
            //}
            //else
            //{
            //    RigidBody.Velocity.X = 0;
            //}

            //if (vertDirection != 0)
            //{
            //    RigidBody.Velocity.Y = vertDirection * maxSpeed;
            //}
            //else
            //{
            //    RigidBody.Velocity.Y = 0;
            //} 
            #endregion

            //Vector2 mousePos = Game.Win.MousePosition;
            //if (Game.Win.MouseLeft)
            //{
            //    if (!isClicked)
            //    {
            //        isClicked = true;
            //        isArrived = false;
            //        dest = mousePos;
            //    }
            //}
            //else
            //{
            //    isClicked = false;
            //}

            if (Game.Win.MouseLeft)
            {
                if (!isClicked)
                {
                    isClicked = false;
                    Vector2 mousePos = Game.Win.MousePosition;
                    Vector2 mouseAbsolutePosition = CameraMngr.MainCamera.position - CameraMngr.MainCamera.pivot + Game.Win.MousePosition;

                    List<Node> path = ((PlayScene)Game.CurrentScene).Map.GetPath((int)Position.X, (int)Position.Y, (int)mouseAbsolutePosition.X, (int)mouseAbsolutePosition.Y);
                    Agent.SetPath(path);
                }
            }
            else
            {
                isClicked = false;
            }

            if (Game.Win.GetKey(KeyCode.Tab))
            {
                if (!camoClicked)
                {
                    camoClicked = true;

                    if (!isCamo)
                    {
                        texture = GfxMngr.GetTexture("dog");
                        isCamo = true;
                    }
                    else
                    {
                        texture = GfxMngr.GetTexture("hero");
                        isCamo = false;
                    }
                }
            }
            else
            {
                camoClicked = false;
            }

            if (Game.Win.GetKey(KeyCode.E))
            {
                if (hasKey)
                {
                    if (!openClicked)
                    {
                        CanOpen = true;
                        openClicked = true;
                        audioSource.Play(audioClip);
                    }
                }
                else
                {
                    if (!openClicked)
                    {
                        openClicked = true;
                        audioSource.Play(hasNotKeyClip);
                    }
                }
            }
            else
            {
                CanOpen = false;
                openClicked = false;
            }
        }


        public void ResetPath(Vector2 pos)
        {
            List<Node> path = ((PlayScene)Game.CurrentScene).Map.GetPath((int)Position.X, (int)Position.Y, (int)pos.X, (int)pos.Y);
            Agent.SetPath(path);
        }
        public override void Update()
        {
            Agent.Update(maxSpeed, ref currentAnimationSet);
            UpdateAnimation();
            currentAnimation.Play();

            #region debug
            //if (Math.Sign(Forward.X) == 1 && (Math.Sign(Forward.Y) == 1 || Math.Sign(Forward.Y) == 0))
            //{
            //    Console.WriteLine("RIGHT");
            //}
            //else if (Math.Sign(Forward.X) == 1 && Math.Sign(Forward.Y) == -1)
            //{
            //    Console.WriteLine("UP");
            //}
            //else if (Math.Sign(Forward.X) == -1 && Math.Sign(Forward.Y) == 1)
            //{
            //    Console.WriteLine("DOWN");
            //}
            //else if (Math.Sign(Forward.X) == -1 && Math.Sign(Forward.Y) == -1)
            //{
            //    Console.WriteLine("LEFT");
            //}
            //Console.WriteLine(Forward.X + " " + Forward.Y); 
            #endregion
            #region Ground
            //float groundY = ((PlayScene)Game.CurrentScene).GroundY;

            //if (Position.Y + HalfHeight > groundY)
            //{
            //    sprite.position.Y = groundY - HalfHeight;
            //    RigidBody.Velocity.Y = 0;
            //}
            #endregion
        }
        public void UpdateAnimation()
        {
            currentAnimation = animationList[(int)currentAnimationSet];
        }
        public void ChargeShoot()
        {

        }
        public void StopCharging()
        {

        }

        public override void Draw()
        {
            Agent.Draw();
            //sprite.DrawTexture(texture, 0, 0, 16, 16);
            sprite.DrawTexture(texture, (int)currentAnimation.Offset.X, (int)currentAnimation.Offset.Y, currentAnimation.FrameWidth, currentAnimation.FrameWidth);
        }

        public void PickUpKey()
        {
            hasKey = true;
        }

        public void RestartPosition()
        {

        }

        public override void OnDie()
        {
            IsActive = false;
            ((PlayScene)Game.CurrentScene).OnPlayerDies();
        }
    }
}
