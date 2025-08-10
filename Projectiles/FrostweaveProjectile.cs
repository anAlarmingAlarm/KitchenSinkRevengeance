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
    private float shootSpeed = 50;
    
    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;

        Projectile.aiStyle = -1;
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