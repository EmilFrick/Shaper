using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShaperUser;
using Shaper.Models.ViewModels.ShoppingCartVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IShaperUserRepository: IRepository<ShaperUser>
    {
        Task<ShaperUser> GetCustomerAsync(ShaperUserDetails customerDetails);
    }
}
