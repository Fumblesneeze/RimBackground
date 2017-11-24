using Harmony;
using RimWorld;
using System;
using UnityEngine;
using UnityEngine.Video;
using Verse;

namespace AnimatedBG
{
    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(UI_BackgroundMain))]
    [HarmonyPatch(nameof(UI_BackgroundMain.BackgroundOnGUI))]
    internal class AnimatedBackground
    {
        public static Vector2 MainBackgroundSize = new Vector2(2000f, 1190f);
        //public static Texture MainAnimatedBackground;

        //private static VideoPlayer player;
        public static MovieTexture MainAnimatedBackground;

        private static bool Playing = false;
        private static WWW request;


        static AnimatedBackground()
        {
            var mod = Mod.GetModWithIdentifier("AnimatedBG") ?? Mod.GetModWithIdentifier("Background") ?? Mod.GetModWithIdentifier("869600389");

            var location = mod.RootDir.FullName;
            //var x = new WWW();
            //x.GetMovieTexture();
            //player = Current.Root.gameObject.AddComponent<VideoPlayer>();
            //player.errorReceived += (e, o) => { Log.Error(o); };
            ////We want to play from url
            //Log.Message($"Loading video {location}\\Textures\\UI\\AnimatedTitle.ogv");
            //player.source = VideoSource.Url;
            //player.url = "file:///" + $"{ location}\\Textures\\UI\\AnimatedTitle.ogv".Replace('\\', '/').ReplaceFirst("/", "//");
            ////player.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
            //player.isLooping = true;
            //player.playOnAwake = true;
            //player.enabled = true;
            //player.Prepare();
            request = new WWW($"file:///{location}/Textures/UI/AnimatedTitle.ogv");

        }

        [HarmonyPrefix]
        public static bool BackgroundOnGUI()
        {
            //if (!player.isPrepared)
            //{
            //    Log.Message($"Waiting for videoplay to prepare");
            //    return true;
            //}
            //if(!player.isPlaying)
            //{
            //    player.Play();
            //}

            //MainAnimatedBackground = player.texture;

            if (!request.isDone)
            {
                //Log.Message($"Waiting for Movie to Load ({request.progress})");
                return true;
            }
            if (!Playing)
            {
                MainAnimatedBackground = request.GetMovieTexture();
            }

            Rect position;
            if ((double)Screen.width <= (double)Screen.height * ((double)AnimatedBackground.MainBackgroundSize.x / (double)AnimatedBackground.MainBackgroundSize.y))
            {
                int height = Screen.height;
                float width = (float)Screen.height * (AnimatedBackground.MainBackgroundSize.x / AnimatedBackground.MainBackgroundSize.y);
                position = new Rect((float)(Screen.width / 2) - width / 2f, 0.0f, width, (float)height);
            }
            else
            {
                int width = Screen.width;
                float height = (float)Screen.width * (AnimatedBackground.MainBackgroundSize.y / AnimatedBackground.MainBackgroundSize.x);
                position = new Rect(0.0f, (float)(Screen.height / 2) - height / 2f, (float)width, height);
            }
            GUI.DrawTexture(position, MainAnimatedBackground, ScaleMode.ScaleToFit);

            if (!Playing && MainAnimatedBackground.isReadyToPlay)
            {
                MainAnimatedBackground.loop = true;
                MainAnimatedBackground.Play();
                Playing = true;
            }
            return false;
        }
    }
}
