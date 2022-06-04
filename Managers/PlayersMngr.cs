using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    static class PlayersMngr
    {
        private static List<Player> players;

        public static void Init()
        {
            players = new List<Player>();
        }

        public static void AddPlayer(Player player)
        {
            if (player != null)
            {
                players.Add(player); 
            }
        }

        public static Player GetPlayer(int id)
        {
            return players[id];
        }

        public static void RemovePlayer(int id)
        {
            players.RemoveAt(id);
        }

        public static void ClearAll()
        {
            players.Clear();
        }
    }
}
