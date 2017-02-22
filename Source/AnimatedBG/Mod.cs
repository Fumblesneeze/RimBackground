using HugsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace AnimatedBG
{
    public class Mod : ModBase
    {

        public override string ModIdentifier { get; } = "AnimatedBG";

        public override void Initialize()
        {
        }

        static List<ModMetaData> ModList { get; } = (List<ModMetaData>)ModLister.AllInstalledMods;

        public static ModMetaData GetModWithIdentifier(string identifier)
        {
            for (int index = 0; index < ModList.Count; ++index)
            {
                if (ModList[index].Identifier == identifier)
                    return ModList[index];
            }
            return (ModMetaData)null;
        }
    }
}
