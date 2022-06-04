﻿using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class SoundEmitter : Component
    {
        protected AudioSource source;
        protected AudioClip clip;

        public float Volume { get { return source.Volume; } set { source.Volume = value; } }
        public float Pitch { get { return source.Pitch; } set { source.Pitch = value; } }

        public SoundEmitter(GameObject owner, string clipName) : base(owner)
        {
            source = new AudioSource();
            clip = GfxMngr.GetClip(clipName);
        }

        public void Play()
        {
            source.Play(clip);
        }

        public void Play(float volume, float pitch = 1.0f, AudioClip clipToPlay = null)
        {
            source.Volume = volume;
            source.Pitch = pitch;

            source.Play(clipToPlay != null ? clipToPlay : clip);
        }
    }
}
