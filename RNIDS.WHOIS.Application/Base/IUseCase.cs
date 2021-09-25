using System.Threading.Tasks;

namespace RNIDS.WHOIS.Application.Base
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
    
    public interface IUseCase<TRequest>
    {
        Task ExecuteAsync(TRequest request);
    }
}