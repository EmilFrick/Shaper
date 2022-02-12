﻿using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers
{
    public class ShaperUserHandler : IShaperUserHandler
    {
        private readonly IUnitOfWork _db;

        public ShaperUserHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public Task<ShaperUser> FindShaperUserByIdentityAsync(string identity)
        {
            return _db.ShaperUsers.GetFirstOrDefaultAsync(x => x.IdentityId == identity);
        }
    }
}
