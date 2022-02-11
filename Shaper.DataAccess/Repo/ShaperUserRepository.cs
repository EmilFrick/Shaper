using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShaperUser;
using Shaper.Models.ViewModels.ShoppingCartVM;
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

        public async Task<ShaperUser> GetCustomerAsync(ShaperUserDetails userDetails)
        {
            ShaperUser user = await GetFirstOrDefaultAsync(x=>x.IdentityId == userDetails.IdentityId);
            if (user is null)
            {
                user = userDetails.GetEntity();
                await _db.ShaperUsers.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            return user;
        }
    }
}
