using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Utility
{
    public static class HexHelper
    {
        public static string GetRGBA(this string colorHex, int transparencyValue)
        {
            string r, g, b;
            var hex = colorHex;

            if (hex.Length == 4)
            {
                r = "0x" + hex[1] + hex[1];
                g = "0x" + hex[2] + hex[2];
                b = "0x" + hex[3] + hex[3];
            }
            else if (hex.Length == 7)
            {
                r = "0x" + hex[1] + hex[2];
                g = "0x" + hex[3] + hex[4];
                b = "0x" + hex[5] + hex[6];
            }
            else
            {
                return "rgb(0,0,0);";
            }

            var a = GetTransparency(transparencyValue);

            return $"rgba({r},{g},{b},{a});";
        }

        private static double GetTransparency(double val)
        {
            double transpVal = 100 - val;
            return transpVal * 0.01;
        }
    }
}
