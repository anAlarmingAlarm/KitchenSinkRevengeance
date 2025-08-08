using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace KitchenSinkRevengeance.Items.Accessories
{
    public class UnluckyHorseshoe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.value = Item.buyPrice(silver: 10);
            Item.rare = ItemRarityID.Gray;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<UnluckyHorseshoePlayer>().equipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LuckyHorseshoe)
                .AddIngredient(ItemID.RottenChunk)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LuckyHorseshoe)
                .AddIngredient(ItemID.Vertebrae)
                .Register();
        }
    }

    public class UnluckyHorseshoePlayer : ModPlayer
    {
        public bool equipped;

        public override void ResetEffects()
        {
            equipped = false;
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (equipped && Main.rand.NextBool(20))
            {
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " rolled a 1."), 69420, 0);
            }
        }
    }

}