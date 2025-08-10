using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Globals
{
    public class ReloadGlobalProjectile : GlobalProjectile
    {
        bool effectShot = false;

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.DamageType.Type == DamageClass.Ranged.Type;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.TryGetOwner(out Player owner) && owner.HeldItem != null)
            {
                switch (owner.HeldItem.type)
                {
                    case ItemID.FlintlockPistol:
                        projectile.penetrate += 1;
                        projectile.maxPenetrate += 1;
                        break;
                    case ItemID.Musket:
                        projectile.penetrate += 2;
                        projectile.maxPenetrate += 2;
                        break;
                }

                if (owner.HeldItem.TryGetGlobalItem(out ReloadGlobalItem globalItem))
                {
                    if (globalItem.IsEffectShot())
                    {
                        effectShot = true;
                        projectile.OriginalArmorPenetration += globalItem.GetArmorPenModifier();
                        projectile.penetrate += globalItem.GetPenetrationModifier();
                        projectile.maxPenetrate += globalItem.GetPenetrationModifier();
                    }
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effectShot && projectile.TryGetOwner(out Player owner))
            {
                if (Main.myPlayer == owner.whoAmI && owner.HeldItem != null && owner.HeldItem.TryGetGlobalItem(out ReloadGlobalItem globalItem))
                {
                    globalItem.InflictReloadDebuff(target);
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (effectShot && projectile.TryGetOwner(out Player owner))
            {
                if (Main.myPlayer == owner.whoAmI && owner.HeldItem != null && owner.HeldItem.TryGetGlobalItem(out ReloadGlobalItem globalItem))
                {
                    globalItem.InflictReloadDebuff(target);
                }
            }
        }
    }
}
