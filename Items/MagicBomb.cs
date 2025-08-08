using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Items
{
    public class MagicBomb : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bomb);

            Item.damage /= 2;
            Item.DamageType = DamageClass.Magic;
            Item.useTime /= 2;
            Item.useAnimation /= 2;
            Item.mana = 10;
            Item.consumable = false;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 6);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MagicDagger)
                .AddIngredient(ItemID.Bomb)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
