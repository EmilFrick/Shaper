﻿using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShoppingCartVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
        Task<ShoppingCart> GetDetailedShoppingCart(string id);

    }
}
