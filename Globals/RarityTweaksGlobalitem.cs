using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Globals
{
    public class RarityTweaksGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            switch (entity.type)
            {
                case ItemID.Shackle:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.BloodyTear:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.DeadMansSweater:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.LicenseCat:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.LicenseDog:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.LicenseBunny:
                    entity.rare = ItemRarityID.White;
                    break;
                case ItemID.Worm:
                    entity.rare = ItemRarityID.White;
                    break; 
                case ItemID.LifeCrystal:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.ManaCrystal:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.MilkCarton:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.ChickenNugget:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.BowlofSoup:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.EnchantedNightcrawler:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.AmberMosquito:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.LavaCharm:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.SlimeStaff:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.BladedGlove:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.BloodyMachete:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.DiamondStaff:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.Starfury:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.Swordfish:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.AbigailsFlower:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.FeralClaws:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.WhoopieCushion:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.FeralClaws:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.Harpoon:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.SpikyBall:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.AmberStaff:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.BatBat:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.PoisonedKnife:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.DivingHelmet:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.RocketBoots:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.SpectreBoots:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.Beenade:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.LightningBoots:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.Valor:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.Amazon:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.Amazon:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.BallistaRod:
                    entity.rare = ItemRarityID.Blue;
                    break;
                case ItemID.BeesKnees:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.JungleHat:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.JungleShirt:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.JunglePants:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.BeeHeadgear:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.BeeBreastplate:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.BeeGreaves:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.AnkletoftheWind:
                    entity.rare = ItemRarityID.Green;
                    break;
                case ItemID.FrostsparkBoots:
                    entity.rare = ItemRarityID.Orange;
                    break;
                case ItemID.BundleofBalloons:
                    entity.rare = ItemRarityID.Orange;
                    break;
                case ItemID.Hellstone:
                    entity.rare = ItemRarityID.Orange;
                    break;
                case ItemID.HellstoneBar:
                    entity.rare = ItemRarityID.Orange;
                    break;
                case ItemID.TerrasparkBoots:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.DarkShard:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.LightShard:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.UnicornHorn:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.PixieDust:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.EndlessQuiver:
                    entity.rare = ItemRarityID.LightRed;
                    break; 
                case ItemID.EndlessMusketPouch:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.Anchor:
                    entity.rare = ItemRarityID.LightRed;
                    break;
                case ItemID.LandMine:
                    entity.rare = ItemRarityID.Yellow;
                    break;
            }
        }
    }
}
