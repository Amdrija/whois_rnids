using System;
using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;

namespace RNIDS.WHOIS.Application.UseCases
{
    public class TestUseCase : IUseCase<TestRequest, TestResponse>
    {
        public Task<TestResponse> ExecuteAsync(TestRequest request)
        {
            throw new InvalidOperationException("Test");
            return Task.FromResult(new TestResponse() {Result = request.Test.ToLower()});
        }
    }
}