using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Globals
{
    public class RarityTweaksGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            switch (entity.type)
            {
                case ItemID.Shackle:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.LavaCharm:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.AnkletoftheWind:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.AnkletoftheWind:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.FeralClaws:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.WhoopieCushion:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.FeralClaws:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.Harpoon:
                    entity.rare = ItemRarityID.Blue;
                    break;
 
            }
        }
    }
}
