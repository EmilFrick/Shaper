namespace Shaper.Web.ApiService.IService
{
    public interface IShaperApiService
    {
        IColorApiService ColorApi { get; }
        IShapeApiService ShapeApi { get; }
        ITransparencyApiService TransparencyApi { get; }
        IProductApiService ProductApi { get; }
        IShoppingCartApiService ShoppingApi { get; }
        IOrderApiService OrderApi { get; }
    }
}
