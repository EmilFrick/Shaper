namespace Shaper.API.RequestHandlers.IRequestHandlers
{
    public interface IRequestHandler
    {
        IShaperUserHandler ShaperUsers { get; }
        IOrderHandler Orders { get; }
    }
}
