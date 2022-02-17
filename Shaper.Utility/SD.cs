using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shaper.Utility
{
    public static class SD
    {
        public static Regex HexRx = new Regex(@"^#([0-9A-Fa-f]{3}){1,2}$");
        public enum UserType { Customer, Admin, Artist }
        public enum Shapes { square, rectangle, circle, oval, triangleup, triangledown, triangleleft,
                             triangleright, triangletopleft, triangletopright, trianglebottomleft,
                             trianglebottomright, trapezoid, cone, Default }
        public static IEnumerable<SelectListItem> UserTypeSelection()
        {
            return Enum.GetNames(typeof(UserType)).Select(i => new SelectListItem
            {
                Text = i.ToString(),
                Value = i.ToString()
            }).ToList();
        }
    }
}
