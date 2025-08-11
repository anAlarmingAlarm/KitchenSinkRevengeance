using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Projectiles;

public class FrostweaveProjectile : ModProjectile
{
    private const int maxDetectionRadius = 32 * 16; // 16 units per tile
    private const float childProjectileSpeed = 15f;
    private const float shootSpeed = 40;
    private const int particleCount = 8;
    private const float particleSpeed = 3f;
    
    // Used for fade out animation near the end
    private const int fadeOutDuration = 60 * 1; // 1 second
    private const int lifetime = 60 * 5; // 5 seconds (total attacking time is this minus fadeOutDuration)
    private const int startAlpha = 100;

    private NPC TargetNPC = null;

    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;

        Projectile.alpha = startAlpha;
        Projectile.timeLeft = lifetime;
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = height = 16;
        return true;
    }

    public override void AI()
    {
        // Shoot periodically, and if the projectile is not in the fade out animation
        if (Projectile.timeLeft % shootSpeed == 0 && Projectile.timeLeft > fadeOutDuration)
        {
            // Spawn projectile, only from the owner
            if (Projectile.owner == Main.myPlayer) SpawnHail();
        }

        // Fade out animation
        if (Projectile.timeLeft <= fadeOutDuration)
        {
            // Projectile should start to fade out
            // Scaled so the projectile smoothly fades out to 255 over the fadeOutDuration
            Projectile.alpha += (255 - startAlpha) / fadeOutDuration;
        }

        for (int i = 0; i < particleCount; i++)
        {
            SpawnDust();
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.timeLeft > fadeOutDuration)
        {
            Projectile.timeLeft = fadeOutDuration;
            Projectile.velocity = Vector2.Zero;
            Projectile.extraUpdates += 2; // speed up ai to make the fadeout faster when hitting a tile
            return false;
        }
        return Projectile.timeLeft <= 0;
    }

    private void SpawnDust()
    {
        // How far ahead of the projectile's center to aim
        float predictionFactor = 18f;
        float radius = Main.rand.NextFloat(8, 48);
        float angle = Main.rand.NextFloat(MathHelper.TwoPi);

        Vector2 offset = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius;
        Vector2 spawnPos = Projectile.Center + offset;
        Vector2 predictedTarget = Projectile.Center + (Projectile.velocity * predictionFactor);

        Vector2 toTarget = predictedTarget - spawnPos;
        toTarget.Normalize();

        // Create dust with the parent's alpha
        Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Ice, toTarget * particleSpeed);
        dust.noGravity = true;
        dust.alpha = Projectile.alpha;
    }

    private void SpawnHail()
    {
        TargetNPC = SinkUtils.GetNearestTargetInLoS(TargetNPC, Projectile.Center, Projectile.position, 16, maxDetectionRadius);
        if (TargetNPC == null) return;

        Vector2 velocity = Vector2.Normalize(TargetNPC.Center - Projectile.Center) * childProjectileSpeed;
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
            ModContent.ProjectileType<FrostweaveHailProjectile>(), Projectile.damage, Projectile.knockBack,
            Projectile.owner);
    }

    public override bool? CanHitNPC(NPC target)
    {
        return false;
    }

    public override bool CanHitPlayer(Player target)
    {
        return false;
    }
}