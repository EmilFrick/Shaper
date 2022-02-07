using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.TransparencyVM
{
    public class TransparencyDisplayVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }
        public double AddedValue { get; set; }
        public TransparencyDisplayVM(Transparency transparency)
        {
            Id = transparency.Id;
            Name = transparency.Name;
            Description = transparency.Description;
            Value = transparency.Value;
            AddedValue = transparency.AddedValue;
        }

        public static IEnumerable<TransparencyDisplayVM> TransparencyDisplayVMs(IEnumerable<Transparency> transparencies)
        {
            List<TransparencyDisplayVM> transparencyDisplayVMs = new List<TransparencyDisplayVM>();
            foreach (var transparency in transparencies)
            {
                var transsparencyVM = new TransparencyDisplayVM(transparency);
                transparencyDisplayVMs.Add(transsparencyVM);
            }
            return transparencyDisplayVMs;
        }
    }
}
