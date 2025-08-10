using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Projectiles;

public class FrostweaveHailProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.aiStyle = ProjAIStyleID.Arrow;

        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        AIType = ProjectileID.WoodenArrowFriendly;
    }
}