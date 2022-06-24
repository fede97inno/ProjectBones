using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using Aiv.Audio;
using OpenTK;

namespace ProjectBones
{
    static class Game
    {
        private static Window window;

        private static AudioDevice playerEar;
        private static AudioSource bgAudio;
        private static AudioClip bgAudioClip;

        public static Scene CurrentScene { get; private set; }

        static KeyboardController keyboardCtrl;
        static List<Controller> controllers;
        public static float DeltaTime { get { return window.DeltaTime; } }
        public static Window Win { get { return window; } }
        public static Vector2 ScreenCenter { get; private set; }

        public static float HalfDiagonalSquared { get { return ScreenCenter.LengthSquared; } }
        public static float UnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; }
        public static float OptimalUnitSize { get; private set; }    //108 pixel
        public static void Init()
        {
            window = new Window(768, 768, "ProjectBones");              //true dopo il nome setta il FullScreen
            Win.SetVSync(false);
            Win.SetDefaultViewportOrthographicSize(48);
            
            playerEar = new AudioDevice();
            bgAudio = new AudioSource();
            bgAudio.Volume = 0.06f;
            bgAudioClip = AudioMgr.AddClip("background", "Assets/Audio/Forest Whisper.wav");
            
            OptimalScreenHeight = 768;                             //best resolution
            UnitSize = window.Height / window.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / window.OrthoHeight;     //rimane 108 anche se cambio risoluzione perchè calcola in base a OptimalScreenHeight che è la risoluzione base scelta per il gioco

            ScreenCenter = new Vector2(window.OrthoWidth * 0.5f, window.OrthoHeight * 0.5f);

            #region Scene
            PlayScene playScene = new PlayScene();
            TitleScene titleScene = new TitleScene("Assets/Objects/backg.jpg");
            TitleScene endTitleScene = new TitleScene("Assets/Objects/backgr.jpg");
            titleScene.NextScene = playScene;
            playScene.NextScene = endTitleScene;
            CurrentScene = titleScene;
            #endregion

            #region Controllers
            List<KeyCode> keys = new List<KeyCode>();
            keys.Add(KeyCode.W);
            keys.Add(KeyCode.S);
            keys.Add(KeyCode.D);
            keys.Add(KeyCode.A);
            keys.Add(KeyCode.Space);

            KeysList keyList = new KeysList(keys);

            //Controllers

            keyboardCtrl = new KeyboardController(0, keyList);

            string[] joysticks = Window.Joysticks;
            controllers = new List<Controller>();

            for (int i = 0; i < joysticks.Length; i++)
            {
                Console.WriteLine(Win.JoystickDebug(i));
                Console.WriteLine(joysticks[i]);
                if (joysticks[i] != null && joysticks[i] != "Unmapped Controller")
                {
                    controllers.Add(new JoypadController(i));
                }
            }
            #endregion

            #region AudioSintax
            //AudioSource source = new AudioSource();                             //manda in play i suoni
            //AudioClip clip = new AudioClip("Assets/Sounds/whistle.ogg");        //clip da mandare in play

            //source.Play(clip); 
            #endregion
        }
        public static float PixelsToUnits(float pixelsSize) 
        {
            return pixelsSize / OptimalUnitSize;
        }
        public static Controller GetContoller(int index)
        {
            Controller ctrl = keyboardCtrl;

            if (index < controllers.Count)
            {
                ctrl = controllers[index];
            }

            return ctrl;
        }
        public static void Play()
        {
            CurrentScene.Start();
            bgAudio.Play(bgAudioClip);
            while (window.IsOpened)
            {
                if (!CurrentScene.isPlaying)
                {
                    Scene nextScene = CurrentScene.OnExit();

                    if (nextScene != null)
                    {
                        CurrentScene = nextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                //INPUT
                CurrentScene.Input();
                //UPDATE
                CurrentScene.Update();
                //DRAW
                CurrentScene.Draw();
                window.Update();
                Exit();
            }
        }

        public static void Exit()
        {
            if (Game.Win.GetKey(KeyCode.Esc))
            {
                Game.Win.Close();
            }
        }
    }
}
