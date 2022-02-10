using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ColorVM;
using SnutteBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private readonly AppDbContext _db;

        public ColorRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Color> CheckDefaultColorAsync()
        {
            var defaultColor = _db.Colors.FirstOrDefault(x => x.Name == "Default");
            if (defaultColor is null)
            {
                defaultColor = new Color
                {
                    Name = "Default",
                    Hex = "#000000",
                    AddedValue = 0,
                };
                await _db.Colors.AddAsync(defaultColor);
                await _db.SaveChangesAsync();
            }
            return defaultColor;
        }


        public async void Update(Color color)
        {
            _db.Colors.Update(color);
        }
    }
}
