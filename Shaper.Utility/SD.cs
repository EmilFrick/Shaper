using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shaper.Utility
{
    public static class SD
    {
        public static Regex HexRx = new Regex(@"^#([0-9A-Fa-f]{3}){1,2}$"); 
    }
}
