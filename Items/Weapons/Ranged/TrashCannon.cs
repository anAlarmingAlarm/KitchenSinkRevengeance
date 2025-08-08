using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using KitchenSinkRevengeance.Projectiles;

namespace KitchenSinkRevengeance.Items.Weapons.Ranged
{
    public class TrashCannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 48;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.DD2_BallistaTowerShot;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 42;
            Item.useTime = 72;
            Item.useAnimation = 72;
            Item.knockBack = 10f;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<TrashProjectile>();
            Item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TrashCan)
                .AddIngredient(ItemID.Wood, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Adjust gun position to correlate with hand
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, 4f);
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.velocity.X += (velocity.X < 0) ? 3f : -3f;
            player.velocity.Y += (velocity.Y < 0) ? 2f : -2f;

            return true;
		}

        // Make shots appear out of muzzle
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}
		}
    }
}
