using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace Bounce
{
    [KnownType(typeof(eventData))]
    [KnownType(typeof(eventData_1))]
    [KnownType(typeof(eventData_2))]
    public class eventData
    {
        public int type;
        public int num;

        public float delay=0;
        
    }
}
