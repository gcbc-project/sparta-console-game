using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    internal class DungeonManager
    {
        public List<Dungeon> DungeonList { get; set; }

       

        public DungeonManager()
        {
            DungeonList = new List<Dungeon>();
          
        }


        public void Enter(Player player, int index)
        {
            DungeonList[index].Battle(player);
        }
 
    }
}
