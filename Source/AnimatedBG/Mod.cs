using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace AnimatedBG
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            var harmony = HarmonyInstance.Create("Fumblesneeze.AnimatedBG");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
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
