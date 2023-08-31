namespace Lib;

public interface IUseCaseInvoker
{
    TResponse Invoke<TResponse>(object request) where TResponse : IResponse;
}
