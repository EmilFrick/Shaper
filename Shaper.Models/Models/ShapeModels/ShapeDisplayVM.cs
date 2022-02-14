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
using Shaper.Utility.CustomValidations;

namespace Shaper.Models.Models.ShapeModels
{
    public class ShapeDisplayVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasFrame { get; set; }
        public double AddedValue { get; set; }
        public ShapeDisplayVM(Shape shape)
        {
            Id = shape.Id;
            Name = shape.Name;
            HasFrame = shape.HasFrame;
            AddedValue = shape.AddedValue;
        }

        public static IEnumerable<ShapeDisplayVM> ShapeDisplayVMs(IEnumerable<Shape> shapes)
        {
            List<ShapeDisplayVM> shapeDisplayVMs = new List<ShapeDisplayVM>();
            foreach (var shape in shapes)
            {
                var shapeVM = new ShapeDisplayVM(shape);
                shapeDisplayVMs.Add(shapeVM);
            }
            return shapeDisplayVMs;
        }
    }
}
