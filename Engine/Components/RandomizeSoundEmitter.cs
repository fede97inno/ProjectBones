using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    class RandomizeSoundEmitter : Component
    {
        AudioSource source;
        List<AudioClip> clips;
        public RandomizeSoundEmitter(GameObject owner) : base(owner)
        {
            source = new AudioSource();
            clips = new List<AudioClip>();
        }

        public void AddClip(string name)
        {
            AudioClip clip = GfxMngr.GetClip(name);
            if (clip != null)
            {
                clips.Add(clip); 
            }
        }

        public void Play()
        {
            RandomizePitch();

            source.Play(GetRandomClip());
        }

        public void Play(float volume)
        {
            source.Volume = volume;

            Play();
        }

        public void RandomizePitch()
        {
            source.Pitch = RandomGen.GetRandomFloat() * 0.4f + 0.8f;        //0.8f - 1.2f
        }

        public AudioClip GetRandomClip()
        {
            return clips[RandomGen.GetRandomInt(0, clips.Count)];
        }
    }
}
