using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    class mapData
    {
        public string title;
        public int level;
        public string creator;

        public List<List<mapChip>> Layor;

        public void Load()
        {
            Layor = new List<List<mapChip>>();
            Layor.Add(new List<mapChip>());

        }

    }
}
