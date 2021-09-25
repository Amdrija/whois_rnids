using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;

namespace RNIDS.WHOIS.Application.UseCases
{
    public class TestUsecaseWithoutResponse : IUseCase<TestRequest>
    {
        public Task ExecuteAsync(TestRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}