using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using SnutteBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo
{
    public class ShapeRepository : Repository<Shape>, IShapeRepository
    {
        private readonly AppDbContext _db;

        public ShapeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Shape shape)
        {
            _db.Shapes.Update(shape);
        }
    }
}
