using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.MyMediator.Interfaces
{
    internal interface IRequestHandler<TRequest, TResponse> where TRequest :  IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
