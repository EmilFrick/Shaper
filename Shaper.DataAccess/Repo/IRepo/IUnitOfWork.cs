using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IUnitOfWork
    {
        IShapeRepository Shapes{ get; }
        IColorRepository Colors{ get; }
        ITransparencyRepository Transparencies { get; }
        IProductRepository Products{ get; }
        IShoppingCartRepository ShoppingCarts{ get; }
        IOrderRepository Orders{ get; }

        Task SaveAsync();
    }
}
