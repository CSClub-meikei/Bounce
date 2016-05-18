using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace Bounce
{
    public class mapData
    {
        public string title;
        public int level;
        public string creator;
        public Point start;

        public List<List<mapChip>> Layor;

        public void Load(Game1 game, Screen screen)
        {
            if (Layor == null)
            {
                Layor = new List<List<mapChip>>();

                Layor.Add(new List<mapChip>());
            }
            
            foreach (List<mapChip> layor in Layor) foreach (mapChip chip in layor) { chip.reLoad(game, screen); }
        }

    }
}
