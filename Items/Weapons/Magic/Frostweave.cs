using KitchenSinkRevengeance.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Items.Weapons.Magic;

public class Frostweave : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 40;
        
        Item.damage = 15;
        Item.knockBack = 2;
        
        Item.DefaultToStaff(ModContent.ProjectileType<FrostweaveProjectile>(), 2.25f, 80, 10);
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item165;
        Item.useStyle = ItemUseStyleID.Shoot;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center) * 2f;
        Projectile.NewProjectile(source, player.Center, direction, type, 
            damage, knockback, player.whoAmI, 60*5);
        NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
}