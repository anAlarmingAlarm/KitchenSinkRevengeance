using KitchenSinkRevengeance.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Items.Weapons.Magic;

public class Frostweave : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 40;
        
        Item.damage = 12;
        Item.knockBack = 1;
        
        Item.DefaultToStaff(ModContent.ProjectileType<FrostweaveProjectile>(), 2.5f, 80, 10);
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item8;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 20)
            .AddIngredient(ItemID.Silk, 5)
            .AddIngredient(ItemID.FallenStar)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}