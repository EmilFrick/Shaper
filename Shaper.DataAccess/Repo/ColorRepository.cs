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

        public void Update(Color color)
        {
            _db.Colors.Update(color);
        }
    }
}
