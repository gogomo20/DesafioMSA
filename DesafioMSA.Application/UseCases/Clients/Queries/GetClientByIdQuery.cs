using DesafioMSA.Application.MyMediator.Interfaces;
using DesafioMSA.Application.Responses;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Repositories.Views;
using DesafioMSA.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.UseCases.Clients.Queries
{
    public class GetClientByIdQuery : IRequest<GenericResponse<ClientView>>
    {
        public long Id { get; set; }
        public class GetClientByIdQueryHandle : IRequestHandler<GetClientByIdQuery, GenericResponse<ClientView>>
        {
            private readonly IClientRepository _repository;
            public GetClientByIdQueryHandle(IClientRepository repository)
            {
                _repository = repository;
            }
            public async Task<GenericResponse<ClientView>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<ClientView>();
                var cliente = await _repository.GetClientAsync(request.Id);
                if (cliente is null) throw new NotFoundedExeption("O cliente informado não existe!");
                response.Message = "Consulta realizada com sucesso!";
                response.Data = cliente;
                return response;
            }
        }

    }
}
