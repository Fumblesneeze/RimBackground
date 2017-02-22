using HugsLib.Source.Detour;
using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace AnimatedBG
{
    [StaticConstructorOnStartup]
    internal class AnimatedBackground : UIMenuBackground
    {
        public static Vector2 MainBackgroundSize = new Vector2(2000f, 1190f);
        //public static Texture MainAnimatedBackground = ContentFinder<MovieTexture>.Get("UI/AnimatedTitle", true);
        public static MovieTexture MainAnimatedBackground;

        private static bool Playing = false;
        private static WWW request;

        static AnimatedBackground()
        {
            var mod = Mod.GetModWithIdentifier("AnimatedBG") ?? Mod.GetModWithIdentifier("Background") ?? Mod.GetModWithIdentifier("869600389");

            var location = mod.RootDir.FullName;

            request = new WWW($"file:///{location}/Textures/UI/AnimatedTitle.ogv");
        }

        [DetourMethod(typeof(UI_BackgroundMain), nameof(UI_BackgroundMain.BackgroundOnGUI))]
        public override void BackgroundOnGUI()
        {
            if (!request.isDone)
            {
                Log.Message($"Waiting for Movie to Load ({request.progress})");
                return;
            }
            if(!Playing)
            {
                MainAnimatedBackground = request.movie;
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
        }
    }
}
