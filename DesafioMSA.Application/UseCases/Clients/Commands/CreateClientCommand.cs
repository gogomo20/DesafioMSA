using DesafioMSA.Application.MyMediator.Interfaces;
using DesafioMSA.Application.Responses;
using DesafioMSA.Application.Services;
using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Shared;
using DesafioMSA.Domain.Shared.Exceptions;

namespace DesafioMSA.Application.UseCases.Clients.Commands
{
    public class CreateClientCommand : BaseClientCommand, IRequest<GenericResponse<long>>
    {
        public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, GenericResponse<long>>
        {
            private readonly IWriteRepository<Client> _repository;
            private readonly IReadRepository<Client> _readRepository;
            private readonly IUnitOfWork _uow;
            public CreateClientCommandHandler(
                IWriteRepository<Client> repository,
                IReadRepository<Client> readRepository,
                IUnitOfWork uow)
            {
                _repository = repository;
                _readRepository = readRepository;
                _uow = uow;
            }

            public async Task<GenericResponse<long>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<long>();

                try
                {
                    var entity = new Client(request.FantasyName, new Cnpj(request.Cnpj));
                    if (await _readRepository.Any(x => x.Cnpj.Value == entity.Cnpj.Value)) throw new InvalidException("O Cnpj informado já está cadastrado!");
                    await _repository.Create(entity);
                    await _uow.CommitAsync();
                    response.Data = entity.Id;
                    response.Message = "Cliente cadastrado com sucesso!";
                }
                catch (Exception ex) {
                    switch (ex)
                    {
                        case InvalidException invalid:
                            response.Message = "Um erro ocorreu ao realizar operação!";
                            response.Errors = [invalid.Message];
                            break;
                        default:
                            throw;
                    }
                }

                return response;
            }
        }
    }
}
