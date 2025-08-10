using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Projectiles;

public class FrostweaveProjectile : ModProjectile
{
    private float time = 0;
    private float maxDetectionRadius = 500f;
    private NPC TargetNPC = null;
    private float childProjectileSpeed = 15f;
    private float shootSpeed = 40;
    private int particleCount = 8;
    private float particleSpeed = 3f;
    
    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;

        Projectile.aiStyle = -1;
        Projectile.alpha = 100;
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = height = 16;
        return true;
    }

    public override void AI()
    {
        time++;

        // If the projectile is as old as the max animation time, kill the projectile.
        if (time >= Projectile.ai[0]) {
            Projectile.Kill();
            time = 0;
        }

        if (time % shootSpeed == 0)
        {
            // Spawn projectile, only from the owner
            if (Projectile.owner == Main.myPlayer) SpawnHail();
        }

        for (int i = 0; i < particleCount; i++)
        {
            SpawnDust();
        }
    }

    private void SpawnDust()
    {
        // How far ahead of the projectile's center to aim
        float predictionFactor = 16f;
        float radius = Main.rand.NextFloat(8, 48);
        float angle = Main.rand.NextFloat(MathHelper.TwoPi);
        
        Vector2 offset = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius;
        Vector2 spawnPos = Projectile.Center + offset;
        Vector2 predictedTarget = Projectile.Center + (Projectile.velocity * predictionFactor);

        Vector2 toTarget = predictedTarget - spawnPos;
        toTarget.Normalize();

        // Create dust
        Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Ice, toTarget * particleSpeed);
        dust.noGravity = true;
        dust.alpha = 125;
    }

    public override bool? CanHitNPC(NPC target)
    {
        return false;
    }

    private void SpawnHail()
    {
        
        if (TargetNPC == null) {
            TargetNPC = FindClosestNPC(maxDetectionRadius);
        }

        if (TargetNPC != null && !IsValidTarget(TargetNPC))
        {
            TargetNPC = null;
        }

        if (TargetNPC == null) return;
        Vector2 direction = Vector2.Normalize(TargetNPC.Center - Projectile.Center);
        Vector2 velocity = direction * childProjectileSpeed;
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, 
            ModContent.ProjectileType<FrostweaveHailProjectile>(), Projectile.damage, Projectile.knockBack, 
            Projectile.owner);
    }
    
    private NPC FindClosestNPC(float maxDetectDistance) {
        NPC closestNPC = null;

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs
        foreach (var target in Main.ActiveNPCs) {
            // Check if NPC able to be targeted. 
            if (IsValidTarget(target)) {
                // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }
        }

        return closestNPC;
    }
    
    public bool IsValidTarget(NPC target) {
        return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
    }
}