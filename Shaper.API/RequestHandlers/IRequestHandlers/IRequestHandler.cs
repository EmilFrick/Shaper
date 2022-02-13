namespace Shaper.API.RequestHandlers.IRequestHandlers
{
    public interface IRequestHandler
    {
        IOrderHandler Orders { get; }
        IShoppingCartHandler ShoppingCarts { get; }

    }
}
