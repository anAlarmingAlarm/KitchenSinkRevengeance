using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace KitchenSinkRevengeance.Projectiles
{
    public class TrashProjectile : ModProjectile
    {
        int bounces = 1;

        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            Projectile.timeLeft = 3600;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = Main.rand.Next(10) + 20; i > 0; i--)
            {
                Vector2 dustPos = new(Projectile.position.X + (Main.rand.Next(10) - 5) * 0.15f, Projectile.position.Y + (Main.rand.Next(10) - 5) * 0.15f);
                Dust.NewDust(dustPos, 12, 12, DustID.MarblePot, (dustPos.X - Projectile.position.X) * 0.4f * Main.rand.Next(2), (dustPos.Y - Projectile.position.Y) * 0.4f * Main.rand.Next(2));
            }
            if (bounces <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                bounces--;
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity.X *= 0.75f;
                Projectile.damage = Projectile.damage * 4 / 5;
            }
            return false;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.995f;
            Projectile.velocity.Y += 0.15f;
            Projectile.rotation += (Projectile.velocity.X < 0f) ? -0.165f : 0.165f;

            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

            if (Main.rand.NextBool(4)) {
                Vector2 dustPos = new(Projectile.position.X + (Main.rand.Next(10) - 5) * 0.15f, Projectile.position.Y + (Main.rand.Next(10) - 5) * 0.15f);
                Dust.NewDust(dustPos, 12, 12, DustID.MarblePot, (dustPos.X - Projectile.position.X) * 0.4f * Main.rand.Next(2), (dustPos.Y - Projectile.position.Y) * 0.4f * Main.rand.Next(2));
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = Main.rand.Next(30) + 120; i > 0; i--)
            {
                Vector2 dustPos = new(Projectile.position.X + (Main.rand.Next(10) - 5) * 0.15f, Projectile.position.Y + (Main.rand.Next(10) - 5) * 0.15f);
                Dust.NewDust(dustPos, 12, 12, DustID.MarblePot, (dustPos.X - Projectile.position.X) * 0.4f * Main.rand.Next(2), (dustPos.Y - Projectile.position.Y) * 0.4f * Main.rand.Next(2));
            }
        }
    }
}