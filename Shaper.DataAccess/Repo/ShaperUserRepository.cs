using Microsoft.EntityFrameworkCore;
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
    public class ShaperUserRepository : Repository<ShaperUser>, IShaperUserRepository
    {
        private readonly AppDbContext _db;

        public ShaperUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async void Update(ShaperUser shaperUser)
        {
            _db.ShaperUsers.Update(shaperUser);
        }
    }
}
