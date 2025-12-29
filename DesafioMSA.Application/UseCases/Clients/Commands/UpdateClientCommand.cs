using DesafioMSA.Application.MyMediator.Interfaces;
using DesafioMSA.Application.Responses;
using DesafioMSA.Application.Services;
using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Shared.Exceptions;
using DesafioMSA.Domain.Shared;
using System.Text.Json.Serialization;
using System.Net.Http.Json;

namespace DesafioMSA.Application.UseCases.Clients.Commands
{
    public class UpdateClientCommand : BaseClientCommand, IRequest<GenericResponse<long>>
    {
        [JsonIgnore]
        public long Id { get; set; }
        public bool? Active { get; set; }
        public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, GenericResponse<long>>
        {
            private readonly IWriteRepository<Client> _repository;
            private readonly IReadRepository<Client> _readRepository;
            private readonly IUnitOfWork _unitOfWork;
            public UpdateClientCommandHandler(
                IWriteRepository<Client> repository,
                IReadRepository<Client> readRepository,
                IUnitOfWork unitOfWork
                )
            {
                _repository = repository;
                _readRepository = readRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<GenericResponse<long>> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<long>();
                try
                {
                    var client = await _readRepository.Get(command.Id, cancellationToken);
                    if (client is null) throw new NotFoundedExeption("O cliente informado não existe!");
                    if (await _readRepository.Any(x => x.Id != command.Id && x.Cnpj.Value == client.Cnpj.Value))
                        throw new InvalidException("O Cnpj informado já está cadastrado!");
                    client.UpdateName(command.FantasyName);
                    client.UpdateCnpj(new Cnpj(command.Cnpj));
                    if (command.Active.HasValue)
                        client.UpdateStatus((bool)command.Active!);
                    await _repository.Update(client);
                    await _unitOfWork.CommitAsync();
                    response.Data = client.Id;
                    response.Message = "Cliente atualizado com sucesso!";
                }
                catch (Exception ex)
                {
                    switch (ex)
                    {
                        case InvalidException _:
                            response.Message = "Erro ao realizara ação";
                            response.Errors = [ex.Message];
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
