using KitchenSinkRevengeance.Globals;
using System;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Players
{
    public class ReloadPlayer : ModPlayer
    {
        bool reloading = false;
        public float reloadSpeed = 1f;

        public void ReloadStart(ReloadProperties props)
        {
            // does nothing for now, may be used for reload effects in the future
            reloading = true;
        }

        public void ReloadEnd(ReloadProperties props)
        {
            // same as above
            reloading = false;
        }

        public int CalculateReloadTime(int reloadTime)
        {
            return (int)Math.Round(reloadTime / reloadSpeed);
        }

        public override void ResetEffects()
        {
            reloadSpeed = 1f;
        }
    }
}
