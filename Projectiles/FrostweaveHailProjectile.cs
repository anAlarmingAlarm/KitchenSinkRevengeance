using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Projectiles;

public class FrostweaveHailProjectile : ModProjectile
{
    private int particleCount = 4;
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.alpha = 255;

        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        Vector2 dustOrigin = Projectile.Center - new Vector2(1, 1);
        for (int i = 0; i < particleCount; i++)
        {
            // Spawn dust
            Dust.NewDust(dustOrigin, 2, 2, DustID.Ice);
        }
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = height = 4;
        return true;
    }
}