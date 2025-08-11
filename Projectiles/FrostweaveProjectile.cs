using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Projectiles;

public class FrostweaveProjectile : ModProjectile
{
    private int time = 0;
    private int maxDetectionRadius = 32 * 16; // 16 units per tile
    private NPC TargetNPC = null;
    private float childProjectileSpeed = 15f;
    private float shootSpeed = 40;
    private int particleCount = 8;
    private float particleSpeed = 3f;
    
    // Used for fade out animation near the end
    private readonly int fadeOutDuration = 60 * 1;
    private readonly int lifetime = 60 * 5;
    private readonly int startAlpha = 100;
    
    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;

        Projectile.aiStyle = -1;
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
        time++;
        // Shoot periodically, and if the projectile is not in the fade out animation
        if (time % shootSpeed == 0 && time < lifetime - fadeOutDuration)
        {
            // Spawn projectile, only from the owner
            if (Projectile.owner == Main.myPlayer) SpawnHail();
        }

        for (int i = 0; i < particleCount; i++)
        {
            SpawnDust();
        }

        // Fade out animation
        if (Projectile.timeLeft < fadeOutDuration)
        {
            // Projectile should start to fade out
            // Scaled so the projectile smoothly fades out to 255 over the fadeOutDuration
            Projectile.alpha += (255 - startAlpha) / fadeOutDuration;
        }
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

    public override bool? CanHitNPC(NPC target)
    {
        return false;
    }

    public override bool CanHitPlayer(Player target)
    {
        return false;
    }

    private void SpawnHail()
    {
        TargetNPC = SinkUtils.GetNearestTargetInLoS(TargetNPC, Projectile.Center, Projectile.position, Projectile.width, maxDetectionRadius);
        if (TargetNPC == null) return;
       
        Vector2 velocity = Vector2.Normalize(TargetNPC.Center - Projectile.Center) * childProjectileSpeed;
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, 
            ModContent.ProjectileType<FrostweaveHailProjectile>(), Projectile.damage, Projectile.knockBack, 
            Projectile.owner);
    }
}