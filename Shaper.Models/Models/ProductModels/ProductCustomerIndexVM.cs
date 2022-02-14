using Microsoft.AspNetCore.Mvc.Rendering;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ProductModels
{
    public class ProductCustomerIndexVM
    {
        public IEnumerable<Product> Products { get; set; }
        public int? ColorIdFilter { get; set; }
        public List<SelectListItem> Colors { get; set; }


        public void SetListItems(IEnumerable<Color> colors)
        {
            Colors = colors.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();
        }
    }
}
