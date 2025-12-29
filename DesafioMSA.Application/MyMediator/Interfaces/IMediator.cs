using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.MyMediator.Interfaces
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken); 
    }
}
