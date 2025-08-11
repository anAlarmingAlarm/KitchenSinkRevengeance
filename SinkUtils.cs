using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace KitchenSinkRevengeance
{
    /// <summary>
    /// Utility functions
    /// </summary>
    public class SinkUtils
    {
        /// <summary>
        /// Utility function for projectile enemy targeting<br />
        /// Checks if target is valid and in range (if specified)<br />
        /// If not, finds nearest valid target in range<br />
        /// May return null if no valid targets are found<br /><br />
        /// Note that this will continue to target the same NPC until it either dies or exits its range, even if another NPC comes closer<br />
        /// To always get the nearest target, use <c>GetNearestTargetActive()</c>
        /// </summary>
        public static NPC GetNearestTarget(NPC oldTarget, Vector2 point, int maxDetectionRadius = int.MaxValue)
        {
            maxDetectionRadius *= maxDetectionRadius; // square up

            if (oldTarget != null && oldTarget.CanBeChasedBy() && oldTarget.Center.DistanceSQ(point) <= maxDetectionRadius)
            {
                return oldTarget;
            }

            NPC target = null;
            float len = maxDetectionRadius; // surely this doesn't horrifically backfire somehow
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy())
                {
                    float newLen = npc.DistanceSQ(point);
                    if (newLen < len)
                    {
                        target = npc;
                        len = newLen;
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// Utility function for projectile enemy targeting<br />
        /// Returns nearest valid target in range<br />
        /// May return null if no valid targets are found<br /><br />
        /// Note that this will check for a new target every time it's called<br />
        /// To only get a new target when the current one is invalid, use <c>GetNearestTarget()</c>
        /// </summary>
        public static NPC GetNearestTargetActive(Vector2 point, int maxDetectionRadius = int.MaxValue)
        {
            maxDetectionRadius *= maxDetectionRadius;

            NPC target = null;
            float len = maxDetectionRadius;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy())
                {
                    float newLen = npc.DistanceSQ(point);
                    if (newLen < len)
                    {
                        target = npc;
                        len = newLen;
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// Utility function for projectile enemy targeting<br />
        /// Checks if target is valid, in range (if specified), and with an unobstructed line of sight<br />
        /// If not, finds nearest valid target in range with an unobstructed line of sight<br />
        /// May return null if no valid targets are found<br /><br />
        /// Note that this will continue to target the same NPC until it either dies or exits its range, even if another NPC comes closer<br />
        /// To always get the nearest target, use <c>GetNearestTargetActiveInLoS()</c>
        /// </summary>
        public static NPC GetNearestTargetInLoS(NPC oldTarget, Vector2 center, Vector2 position, int projectileWidth, int maxDetectionRadius = int.MaxValue)
        {
            maxDetectionRadius *= maxDetectionRadius;

            if (oldTarget != null && oldTarget.CanBeChasedBy() && oldTarget.Center.DistanceSQ(center) <= maxDetectionRadius
                && Collision.CanHit(position, projectileWidth, projectileWidth, oldTarget.position, oldTarget.width, oldTarget.height))
            {
                return oldTarget;
            }

            NPC target = null;
            float len = maxDetectionRadius;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy() && Collision.CanHit(position, projectileWidth, projectileWidth, npc.position, npc.width, npc.height))
                {
                    float newLen = npc.DistanceSQ(center);
                    if (newLen < len)
                    {
                        target = npc;
                        len = newLen;
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// Utility function for projectile enemy targeting<br />
        /// Returns nearest valid target in range with an unobstructed line of sight<br />
        /// May return null if no valid targets are found<br /><br />
        /// Note that this will check for a new target every time it's called<br />
        /// To only get a new target when the current one is invalid, use <c>GetNearestTargetInLoS()</c>
        /// </summary>
        public static NPC GetNearestTargetInLoSActive(Vector2 center, Vector2 position, int projectileWidth, int maxDetectionRadius = int.MaxValue)
        {
            maxDetectionRadius *= maxDetectionRadius;

            NPC target = null;
            float len = maxDetectionRadius;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy() && Collision.CanHit(position, projectileWidth, projectileWidth, npc.position, npc.width, npc.height))
                {
                    float newLen = npc.DistanceSQ(center);
                    if (newLen < len)
                    {
                        target = npc;
                        len = newLen;
                    }
                }
            }
            return target;
        }
    }
}
