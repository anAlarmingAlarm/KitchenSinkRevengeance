using Microsoft.Xna.Framework;
using Terraria;

namespace KitchenSinkRevengeance
{
    /// <summary>
    /// Utility functions
    /// </summary>
    public class SinkUtils
    {
        /// <summary>
        /// Get the nearest player to a point<br />
        /// If <c>alive</c> is true, limits search to living players
        /// </summary>
        public static Player GetNearestPlayer(Vector2 point, bool alive = false)
        {
            Player player = null;
            float len = 0;
            foreach (Player newPlayer in Main.ActivePlayers)
            {
                if (alive && newPlayer.dead) continue;

                float newLen = newPlayer.DistanceSQ(point);
                if (newLen > len)
                {
                    player = newPlayer;
                    len = newLen;
                }
            }
            return player;
        }

        /// <summary>
        /// Get the nearest player to a point from an array of players<br />
        /// If <c>alive</c> is true, limits search to living players
        /// </summary>
        public static Player GetNearestPlayer(Vector2 point, Player[] players, bool alive = false)
        {
            Player player = null;
            float len = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (!player.active || (alive && player.dead)) continue;

                float newLen = players[i].DistanceSQ(point);
                if (newLen > len)
                {
                    player = players[i];
                    len = newLen;
                }
            }
            return player;
        }

        /// <summary>
        /// Get the nearest living enemy to a point<br />
        /// If <c>valid</c> is true, limits search to enemies that can be targetted<br />
        /// If <c>valid</c> is false, search can find any enemy even if they can't be hurt<br />
        /// If <c>index</c> > -1 and <c>valid</c> is true, will check for immune frames from the player corresponding to that index
        /// </summary>
        public static NPC GetNearestEnemy(Vector2 point, bool valid = false, int index = -1)
        {
            NPC npc = null;
            float len = 0;
            foreach (NPC newNpc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.CountsAsACritter || npc.life <= 0) continue;
                if (valid && (npc.dontTakeDamage || npc.immortal || (index >= 0 && npc.immune[index] > 0))) continue;

                float newLen = newNpc.DistanceSQ(point);
                if (newLen > len)
                {
                    npc = newNpc;
                    len = newLen;
                }
            }
            return npc;
        }
    }
}
