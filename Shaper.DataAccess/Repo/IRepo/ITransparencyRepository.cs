using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface ITransparencyRepository : IRepository<Transparency>
    {
        void Update(Transparency transparency);
        Task<Transparency> CheckDefaultTransparencyAsync();
    }
}
