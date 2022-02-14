using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IColorRepository : IRepository<Color>
    {
        void Update(Color color);
        Task<Color> CheckDefaultColorAsync();
    }
}
