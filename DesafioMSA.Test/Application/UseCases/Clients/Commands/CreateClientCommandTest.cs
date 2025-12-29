using DesafioMSA.Application.Services;
using DesafioMSA.Application.UseCases.Clients.Commands;
using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using Moq;
using System.Linq.Expressions;

namespace DesafioMSA.Test.Application.UseCases.Clients.Commands
{
    public class CreateClientCommandTest
    {
        private readonly Mock<IReadRepository<Client>> _readRepository;
        private readonly Mock<IWriteRepository<Client>> _repository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CreateClientCommand.CreateClientCommandHandler _handler;
        public CreateClientCommandTest()
        {
            _readRepository = new Mock<IReadRepository<Client>>();
            _repository = new Mock<IWriteRepository<Client>>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _handler = new CreateClientCommand.CreateClientCommandHandler(
                    _repository.Object,
                    _readRepository.Object,
                    _unitOfWork.Object
                );
        }
        public static CreateClientCommand CreateValidCommand() => new()
        {
            FantasyName = "nome fantasia teste",
            Cnpj = "44.310.688/0001-81"
        };

        [Fact]
        public async Task DevePermitirCriarComDadosValidos()
        {
            //Arrange
            var command = CreateValidCommand();
            _readRepository
                .Setup(r => r.Any(It.IsAny<Expression<Func<Client, bool>>>(), default))
                .ReturnsAsync(false);
            //Act
            var response = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal("Cliente cadastrado com sucesso!", response.Message);

            _repository.Verify(r => r.Create(It.IsAny<Client>()), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task NaoDevePermitirCadastroDeCnpjDuplicados()
        {
            //Arrange
            var command = CreateValidCommand();
            _readRepository
                .Setup(r => r.Any(It.IsAny<Expression<Func<Client, bool>>>(), default))
                .ReturnsAsync(true);
            //Act
            var response = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Um erro ocorreu ao realizar operação!", response.Message);
            Assert.Contains("O Cnpj informado já está cadastrado!", response.Errors);

            _repository.Verify(r => r.Create(It.IsAny<Client>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}
