using DesafioMSA.Application.MyMediator.Interfaces;

namespace DesafioMSA.Application.MyMediator.Implementation
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider serviceProvider;
        public Mediator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            var handleType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handle = serviceProvider.GetService(handleType);
            if (handle is null)
                throw new InvalidOperationException($"Handle not found for {request.GetType().Name}");
            return await (Task<TResponse>)handleType
                .GetMethod("Handle")!
                .Invoke(handle, new object[] { request, cancellationToken })!;
        }
    }
}
