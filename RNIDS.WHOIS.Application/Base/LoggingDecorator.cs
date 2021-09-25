using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RNIDS.WHOIS.Application.Base
{
    public class LoggingDecorator<TRequest, TResponse> : IUseCase<TRequest, TResponse>
    {
        private readonly ILogger logger;
        private readonly IUseCase<TRequest, TResponse> useCase;

        public LoggingDecorator(IUseCase<TRequest, TResponse> useCase, ILogger logger)
        {
            this.logger = logger;
            this.useCase = useCase;
        }

        public async Task<TResponse> ExecuteAsync(TRequest request)
        {
            Guid methodId = Guid.NewGuid();
            string arguments = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            this.logger.LogInformation($"{this.useCase.GetType()?.Name} started. MethodId: {methodId} Arguments:{arguments}");
            try
            {
                return await this.useCase.ExecuteAsync(request);
            }
            catch (Exception e)
            {
                this.logger.LogWarning($"{e.GetType()?.Name} in {this.useCase.GetType()?.Name}. MethodId: {methodId} Exception:{e.Message}");
                throw;
            }
            finally
            {
                this.logger.LogInformation($"{this.useCase.GetType()?.Name} finished. MethodId: {methodId}");
            }
        }
    }
}