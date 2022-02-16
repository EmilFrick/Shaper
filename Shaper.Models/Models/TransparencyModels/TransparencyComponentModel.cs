using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.TransparencyModels
{
    public  class TransparencyComponentModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int TransparencyValue { get; set; }

        public TransparencyComponentModel()
        {

        }
        public TransparencyComponentModel(Transparency transparency)
        {
            Name = transparency.Name;
            Description = transparency.Description;
            TransparencyValue = transparency.Value;
        }
    }
}
