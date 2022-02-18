using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Utility
{
    public static class ShaperHelper
    {
         public static string GetRGBA(this string hexVal, int transparency)
        {
            int r, g, b;
            var hex = hexVal.Substring(1, hexVal.Length - 1);

            if (hex.Length == 6)
            {
                //#RRGGBB
                r = int.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                g = int.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                b = int.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hex.Length == 3)
            {
                //#RGB
                r = int.Parse(hex[0].ToString() + hex[0].ToString(), NumberStyles.AllowHexSpecifier);
                g = int.Parse(hex[1].ToString() + hex[1].ToString(), NumberStyles.AllowHexSpecifier);
                b = int.Parse(hex[2].ToString() + hex[2].ToString(), NumberStyles.AllowHexSpecifier);
            }
            else
            {
                return "rgb(0,0,0);";
            }

            var a = GetTransparency(transparency);

            return $"rgba({r},{g},{b},{a.ToString().Replace(",", ".")});";
        }

        private static double GetTransparency(double val)
        {
            double transpVal = 100 - val;
            return transpVal * 0.01;
        }

        public static string GetShapeStyle(this string shape, string color)
        {
            switch (shape)
            {
                case "square":
                case "rectangle":
                case "circle":
                case "oval":
                    return $"background:{color}";

                case "triangledown":
                case "triangletopleft":
                case "triangletopright":
                    return $"border-top: 100px solid {color}";

                case "triangleleft":
                    return $"border-right: 100px solid {color}";

                case "triangle":
                case "triangleup":
                case "trianglebottomleft":
                case "trianglebottomright":
                case "trapezoid":
                    return $"border-bottom: 100px solid {color}";

                case "triangleright":
                    return $"border-left: 100px solid {color}";
                
                default:
                    return "";

            }
        }
    }
}
