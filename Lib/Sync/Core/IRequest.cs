namespace Lib;

public interface IRequest<out TResponse> where TResponse : IResponse
{

}
