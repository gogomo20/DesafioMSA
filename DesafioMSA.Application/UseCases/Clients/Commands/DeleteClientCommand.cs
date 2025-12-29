using DesafioMSA.Application.MyMediator.Interfaces;
using DesafioMSA.Application.Responses;
using DesafioMSA.Application.Services;
using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Shared.Exceptions;

namespace DesafioMSA.Application.UseCases.Clients.Commands
{
    public class DeleteClientCommand : IRequest<GenericResponseNoData>
    {
        public long Id { get; set; }
        public class DeleteClienteCommandHandler : IRequestHandler<DeleteClientCommand, GenericResponseNoData>
        {
            private readonly IReadRepository<Client> _readRepository;
            private readonly IWriteRepository<Client> _repository;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteClienteCommandHandler(
                IWriteRepository<Client> repository,
                IReadRepository<Client> readRepository,
                IUnitOfWork unitOfWork)
            {
                _readRepository = readRepository;
                _repository = repository;
                _unitOfWork = unitOfWork;
            }
            public async Task<GenericResponseNoData> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
            {
                var response = new GenericResponseNoData();
                var client = await _readRepository.Get(command.Id);
                if (client is null) throw new NotFoundedExeption("O cliente informado não existe");
                await _repository.Delete(client);
                _unitOfWork.CommitAsync();
                response.Message = "Cliente excluído com sucesso!";
                return response;
            }
        }

    }
}
