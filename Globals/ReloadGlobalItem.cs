using KitchenSinkRevengeance.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Globals
{
    public class ReloadGlobalItem : GlobalItem
    {
        public static readonly Dictionary<int, ReloadProperties> reloadProperties = new()
        {
            {ItemID.FlintlockPistol, new ReloadProperties()},
            {ItemID.Musket, new ReloadProperties(reloadTime: 180, reloadStyle: ReloadStyle.Hoist)},
            {ItemID.TheUndertaker, new ReloadProperties(capacity: 6, reloadEffect: ReloadEffect.Heal, effectPotency: [5, 5])},
            {ItemID.Boomstick, new ReloadProperties(capacity: 4, reloadEffect: ReloadEffect.MoreDakka, effectPotency: [2, 1, 4])},
            {ItemID.Handgun, new ReloadProperties(capacity: 12, reloadStyle: ReloadStyle.Mag)},
            {ItemID.Minishark, new ReloadProperties(capacity: 50, reloadEffect: ReloadEffect.ArmorPen, effectPotency: [8, 25], reloadStyle: ReloadStyle.Hoist)},
            {ItemID.Revolver, new ReloadProperties(capacity: 6, reloadEffect: ReloadEffect.Crit, effectPotency: [50, 1])},
            {ItemID.PhoenixBlaster, new ReloadProperties(capacity: 12, reloadEffect: ReloadEffect.Debuff, effectPotency: [BuffID.OnFire3, 1, 1, 240, 360, 0], reloadStyle: ReloadStyle.Mag)},
            {ItemID.QuadBarrelShotgun, new ReloadProperties(capacity: 8, consumption: 4, reloadEffect: ReloadEffect.ArmorPen, effectPotency: [10, 1])},
            {ItemID.ClockworkAssaultRifle, new ReloadProperties(capacity: 15, reloadEffect: ReloadEffect.Crit, effectPotency: [10, 3])},
            {ItemID.Gatligator, new ReloadProperties(capacity: 75, reloadStyle: ReloadStyle.Hoist)},
            {ItemID.OnyxBlaster, new ReloadProperties(capacity: 6, reloadStyle: ReloadStyle.ChargeUp)},
            {ItemID.Shotgun, new ReloadProperties(capacity: 4)},
            {ItemID.Megashark, new ReloadProperties(capacity: 100, reloadEffect: ReloadEffect.ArmorPen, effectPotency: [12, 50], reloadStyle: ReloadStyle.Hoist)},
            {ItemID.Uzi, new ReloadProperties(capacity: 75, reloadEffect: ReloadEffect.AttackSpeed, effectPotency: [2, 10], reloadStyle: ReloadStyle.Mag)},
            {ItemID.VenusMagnum, new ReloadProperties(capacity: 10, reloadEffect: ReloadEffect.ProjectileCursor, effectPotency: [ProjectileID.WoodenArrowFriendly, 1, 1, 70, 7, 12, 8])},
            {ItemID.ChainGun, new ReloadProperties(capacity: 120, reloadEffect: ReloadEffect.ProjectileCursor, effectPotency: [ProjectileID.WoodenArrowFriendly, 3, 5, 40, 4, 8, 15], reloadStyle: ReloadStyle.Hoist)},
            {ItemID.SniperRifle, new ReloadProperties(reloadStyle: ReloadStyle.Mag)},
            {ItemID.TacticalShotgun, new ReloadProperties(disableReload: true)},
            {ItemID.Xenopopper, new ReloadProperties(capacity: 10, reloadEffect: ReloadEffect.Buff, effectPotency: [BuffID.Shine, 1800, 1800])},
            {ItemID.SDMG, new ReloadProperties(capacity: 300, reloadEffect: ReloadEffect.ArmorPen, effectPotency: [30, 75])},
            {ItemID.VortexBeater, new ReloadProperties(capacity: 50, reloadEffect: ReloadEffect.Projectile, effectPotency: [ProjectileID.VortexBeaterRocket, 7, 9, 30, 2.5f, 8, 0, 180], reloadStyle: ReloadStyle.ChargeUp)},
            {ItemID.CandyCornRifle, new ReloadProperties(capacity: 30)},
            {ItemID.PewMaticHorn, new ReloadProperties(disableReload: true)},
            {ItemID.RedRyder, new ReloadProperties(capacity: 4, reloadStyle: ReloadStyle.Hoist)}
        };

        const float downtime = 0.1f; // how much downtime (portion of 1f) the system tries to give guns without pre-defined ReloadProperties

        int remainingAmmo = 69420;
        int reloadLeft = -1;
        int effectShotsLeft = 0;
        bool disableReload = false;
        ReloadProperties props;

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return (entity.DamageType.Type == DamageClass.Ranged.Type && entity.useAmmo == AmmoID.Bullet)
                || entity.type == ItemID.CandyCornRifle;
        }

        public override void SetDefaults(Item entity)
        {
            switch (entity.type)
            {
                case ItemID.FlintlockPistol:
                    entity.damage = 25;
                    break;
                case ItemID.Musket:
                    entity.damage = 54;
                    entity.useTime = 15;
                    entity.useAnimation = 15;
                    break;
                case ItemID.SniperRifle:
                    entity.damage = 215;
                    break;
            }

            props = reloadProperties[entity.type];
            if (props == null)
            {
                props = new(capacity: (int)Math.Round((60 / downtime - 60) / entity.useTime));
                reloadProperties[entity.type] = props;
            }
            remainingAmmo = props.capacity;
            disableReload = props.disableReload;
        }

        public override bool CanShoot(Item item, Player player)
        {
            return remainingAmmo > 0 || disableReload;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            return reloadLeft < 0 || disableReload;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (effectShotsLeft > 0 && props.reloadEffect == ReloadEffect.Damage)
            {
                damage += props.effectPotency[0] / 100;
            }
        }

        public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
        {
            if (effectShotsLeft > 0 && props.reloadEffect == ReloadEffect.Crit) {
                crit += props.effectPotency[0] / 100;
                Main.NewText("reee");
            }
        }

        public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
        {
            if (effectShotsLeft > 0 && props.reloadEffect == ReloadEffect.Knockback)
            {
                knockback += props.effectPotency[0] / 100;
            }
        }

        public override float UseSpeedMultiplier(Item item, Player player)
        {
            if (effectShotsLeft > 0 && props.reloadEffect == ReloadEffect.AttackSpeed)
            {
                return props.effectPotency[0];
            }
            return 1f;
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (remainingAmmo > 0 && !disableReload)
            {
                if (effectShotsLeft > 0)
                {
                    if (props.reloadEffect == ReloadEffect.MoreDakka && Main.myPlayer == player.whoAmI)
                    {
                        for (int i = 0; i < props.effectPotency[0]; i++)
                        {
                            Projectile.NewProjectile(source, position, velocity.RotateRandom(MathHelper.ToRadians(props.effectPotency[2])), type, damage, knockback, player.whoAmI);
                        }
                    }
                    effectShotsLeft--;
                }
                remainingAmmo -= props.consumption;
                if (remainingAmmo == 0 && reloadLeft < 0 && !disableReload)
                {
                    if (player.TryGetModPlayer(out ReloadPlayer reloadPlayer))
                    {
                        reloadLeft = reloadPlayer.CalculateReloadTime(props.reloadTime);
                    }
                    else
                    {
                        reloadLeft = props.reloadTime;
                    }
                    // should spawn reload animation projectile, play sound
                    // for now just print text
                    Main.NewText("reloaderererererer: " + props.reloadStyle);
                }
                return true;
            }
            return disableReload;
        }

        public override void HoldItem(Item item, Player player)
        {
            if (reloadLeft >= 0)
            {
                reloadLeft--;
                if (reloadLeft <= 0)
                {
                    reloadLeft = -1;
                    remainingAmmo = props.capacity;
                    Main.NewText("reloaded"); // debug

                    // Reload effects
                    switch (props.reloadEffect)
                    {
                        case ReloadEffect.None:
                            break;
                        case ReloadEffect.AttackSpeed:
                        case ReloadEffect.Crit:
                        case ReloadEffect.MoreDakka:
                            effectShotsLeft = (int)props.effectPotency[1];
                            break;
                        case ReloadEffect.ArmorPen:
                        case ReloadEffect.Debuff:
                            effectShotsLeft = (int)props.effectPotency[1] + 1;
                            break;
                        case ReloadEffect.Buff:
                            player.AddBuff((int)props.effectPotency[0], Main.rand.Next((int)props.effectPotency[1], (int)props.effectPotency[2] + 1));
                            break;
                        case ReloadEffect.Heal:
                            player.Heal(Main.rand.Next((int)props.effectPotency[0], (int)props.effectPotency[1] + 1));
                            break;
                        case ReloadEffect.Projectile:
                            if (Main.myPlayer == player.whoAmI)
                            {
                                int projectiles = Main.rand.Next((int)props.effectPotency[1], (int)props.effectPotency[2] + 1);
                                for (int i = 0; i < projectiles; i++)
                                {
                                    Projectile.NewProjectile(
                                        player.GetSource_ItemUse(item),
                                        player.Center,
                                        new Vector2(props.effectPotency[5], props.effectPotency[6]).RotateRandom(MathHelper.ToRadians(props.effectPotency[7])),
                                        (int)props.effectPotency[0],
                                        (int)props.effectPotency[3],
                                        (int)props.effectPotency[4],
                                        player.whoAmI
                                    );
                                }
                            }
                            break;
                        case ReloadEffect.ProjectileCursor:
                            if (Main.myPlayer == player.whoAmI)
                            {
                                int projectiles = Main.rand.Next((int)props.effectPotency[1], (int)props.effectPotency[2] + 1);
                                for (int i = 0; i < projectiles; i++)
                                {
                                    Projectile.NewProjectile(
                                        player.GetSource_ItemUse(item),
                                        player.Center,
                                        (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitY).RotateRandom(MathHelper.ToRadians(props.effectPotency[6])) * props.effectPotency[5],
                                        (int)props.effectPotency[0],
                                        (int)props.effectPotency[3],
                                        (int)props.effectPotency[4],
                                        player.whoAmI
                                    );
                                }
                            }
                            break;
                    }
                }
            }
        }

        public bool IsEffectShot()
        {
            if (effectShotsLeft > 0)
            {
                return true;
            }
            return false;
        }

        public int GetArmorPenModifier()
        {
            if (props.reloadEffect == ReloadEffect.ArmorPen)
            {
                return (int)props.effectPotency[0];
            }
            return 0;
        }

        public int GetPenetrationModifier()
        {
            if (props.reloadEffect == ReloadEffect.Penetrate)
            {
                return (int)props.effectPotency[0];
            }
            return 0;
        }

        public void InflictReloadDebuff(NPC npc)
        {
            if (props.reloadEffect == ReloadEffect.Debuff && Main.rand.NextBool((int)props.effectPotency[2]))
            {
                npc.AddBuff((int)props.effectPotency[0], Main.rand.Next((int)props.effectPotency[3], (int)props.effectPotency[4] + 1));
            }
        }

        public void InflictReloadDebuff(Player player)
        {
            if (props.reloadEffect == ReloadEffect.Debuff && props.effectPotency[5] <= 0 && Main.rand.NextBool((int)props.effectPotency[2]))
            {
                player.AddBuff((int)props.effectPotency[0], Main.rand.Next((int)props.effectPotency[3], (int)props.effectPotency[4] + 1));
            }
        }
    }

    public class ReloadProperties
    {
        /// <summary> If true, the reloading system is disabled for this weapon (it acts identically to how it does in vanilla) </summary>
        public readonly bool disableReload;

        /// <summary> Reload time in frames </summary>
        public readonly int reloadTime;

        /// <summary> Amount of bullets the gun can hold </summary>
        public readonly int capacity;

        /// <summary> Amount of loaded bullets consumed per shot </summary>
        public readonly int consumption;

        /// <summary> Animation used when reloading </summary>
        public readonly ReloadStyle reloadStyle = ReloadStyle.Default;

        /// <summary> DustIDs used for reload animation, if applicable<br />If multiple are specified, the effect will contain a random spread of them </summary>
        public readonly List<int> reloadDust;

        /// <summary> Effect on reloading </summary>
        public readonly ReloadEffect reloadEffect = ReloadEffect.None;

        /// <summary> The potency (or potencies) of <c>reloadEffect</c><br />What exactly this affects depends on the <c>reloadEffect</c><br />TO-DO: this setup is fucking awful and should be completely redone, probably after the rest of it is working </summary>
        public readonly List<float> effectPotency;

        /// <summary> Specify unique properties for this weapon's reload </summary>
        public ReloadProperties(bool disableReload = false, int reloadTime = 120, int capacity = 1, int consumption = 1, ReloadStyle reloadStyle = ReloadStyle.Default, int[] reloadDust = null, ReloadEffect reloadEffect = ReloadEffect.None, float[] effectPotency = null)
        {
            this.disableReload = disableReload;
            this.reloadTime = reloadTime;
            this.capacity = capacity;
            this.consumption = consumption;
            this.reloadStyle = reloadStyle;
            this.effectPotency = [];
            this.reloadEffect = reloadEffect;
            this.reloadDust = [];

            if (effectPotency != null)
            {
                foreach (float num in effectPotency)
                {
                    this.effectPotency.Add(num);
                }
            }

            if (reloadDust != null)
            {
                foreach (int num in reloadDust)
                {
                    this.reloadDust.Add(num);
                }
            }
        }
    }

    public enum ReloadStyle
    {
        ///<summary> Gun held slightly downwards </summary>
        Default,
        ///<summary> Gun lifted up and held a bit closer to the player </summary>
        Hoist,
        ///<summary> Gun lifted up, other hand flicks up and back down as if switching mags or inserting a shell </summary>
        Mag,
        ///<summary> Gun lifted up, vibrates slightly as particles float into it (if <c>reloadDust</c> is specified) </summary>
        ChargeUp
    }

    public enum ReloadEffect
    {
        // note: [x] here is shorthand for effectPotency[x]
        ///<summary> Does nothing </summary>
        None,
        ///<summary> Following [1] shots have [0] increased armor penetration </summary>
        ArmorPen,
        ///<summary> Following [1] shots have [0]x attack speed (multiplicative, not additive (?)) </summary>
        AttackSpeed,
        ///<summary> Following [1] shots have +[0]% damage </summary>
        Damage,
        ///<summary> Following [1] shots have +[0]% critical chance </summary>
        Crit,
        ///<summary> Following [1] shots have +[0]% knockback </summary>
        Knockback,
        ///<summary> Grants player [0] buff for [1] to [2] frames on reload </summary>
        Buff,
        ///<summary> Following [1] shots have a 1 in [2] chance to inflict BuffID [0] on enemies hit for [3] to [4] frames<br />Set [5] to a positive number to disable this effect in PvP </summary>
        Debuff,
        ///<summary> Heals for [0] to [1] life on reload </summary>
        Heal,
        ///<summary> Following [1] shots can penetrate through [0] more enemies </summary>
        Penetrate,
        ///<summary> Following [1] shots fire [0] more projectiles at no additional cost with a random deviation of up to [2] degrees in either direction </summary>
        MoreDakka,
        ///<summary> Fires [1] to [2] projectiles of [0] type on reload<br />They'll deal [3] damage and [4] knockback, with a velocity of ([5], [6]), and a random deviation of up to [7] degrees in either direction</summary>
        Projectile,
        ///<summary> Fires [1] to [2] projectiles of [0] type on reload towards the cursor<br />They'll deal [3] damage and [4] knockback, with a speed of [5] and a random deviation of up to [6] degrees in either direction</summary>
        ProjectileCursor
    }
}
