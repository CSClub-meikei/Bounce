using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    class SettingData
    {
         float _BGM_volume;
         float _Effect_volume;

        public float BGM_volume
        {
            get { return _BGM_volume; }
            set { _BGM_volume = value; }
        }
    }
}
