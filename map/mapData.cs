using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace Bounce
{
    [Serializable()]
    public class mapData
    {

        public int texSet = 0;
        public string title;
        public int level;
        public string creator;

        

        public Point start=new Point(640,360);

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
        public mapData Clone(Game1 game, Screen screen)
        {
            mapData map = new mapData();
            map.Layor = new List<List<mapChip>>();
            int i = 0;
            foreach (List<mapChip> layor in Layor)
            {
                map.Layor.Add(new List<mapChip>());
                foreach (mapChip chip in layor)
                {
                    map.Layor[i].Add(chip);
                }
                i++;
            }

                map.title = title;
            map.level = level;
            map.creator = creator;
            map.start = start;
            map.Load(game,screen);
            return map;
        }
    }
}
