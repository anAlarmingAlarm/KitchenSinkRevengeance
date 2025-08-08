using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace KitchenSinkRevengeance.Systems
{
    public class Keybinds : ModSystem
    {
        //public static ModKeybind ExampleBind { get; private set; }

        public override void Load()
        {
            //ExampleBind = KeybindLoader.RegisterKeybind(Mod, "Cycle Ammo Slots", "K");
        }

        public override void Unload()
        {
            //ExampleBind = null;
        }
    }

    public class KeybindPlayer : ModPlayer
    {

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            /*if (Keybinds.ExampleBind.JustPressed)
            {
                // do something
            }*/
        }
    }
}
