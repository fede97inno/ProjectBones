using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class Animation : Component, I_Updatable
    {
        protected int numFrames;
        protected float frameDuration;
        protected bool isPlaying;
        protected int currentFrame;
        protected float elapsedTime;

        public bool Loop;
        public Vector2 Offset { get;protected set; }
        public int FrameWidth { get; protected set; }
        public int FrameHeight { get; protected set; }
        
        public Animation(GameObject owner, int numFrames, int frameWidth, int frameHeight, int framePerSecond, bool loop = true) : base(owner)
        {
            this.numFrames = numFrames;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;

            Loop = loop;

            this.frameDuration = 1.0f / framePerSecond;
            Offset = Vector2.Zero;

            UpdateMngr.AddItem(this);
        }

        public virtual void Play()
        {
            isPlaying = true;
        }

        public virtual void Restart()
        {
            currentFrame = 0;
            elapsedTime = 0;
            isPlaying = true;
        }

        public virtual void Stop()
        {
            currentFrame = 0;
            elapsedTime = 0;
            isPlaying = false;
        }

        public virtual void Pause()
        {
            isPlaying = false;
        }

        protected virtual void OnAnimationEnd()
        {
            isPlaying = false;
            GameObject.IsActive = false;
        }

        public void Update()
        {
            if (IsEnabled && isPlaying)
            {
                elapsedTime += Game.DeltaTime;

                if (elapsedTime >= frameDuration)
                {
                    currentFrame++;
                    elapsedTime = 0;

                    if (currentFrame >= numFrames)
                    {
                        if (Loop)
                        {
                            currentFrame = 0;
                        }
                        else
                        {
                            OnAnimationEnd();
                            return;
                        }
                    }

                    Offset = new Vector2(FrameWidth * currentFrame, Offset.Y);
                }
            }
        }
    }
}
