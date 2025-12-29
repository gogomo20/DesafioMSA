using DesafioMSA.Application.Services;
using DesafioMSA.Application.UseCases.Clients.Commands;
using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Shared;
using DesafioMSA.Domain.Shared.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DesafioMSA.Test.Application.UseCases.Clients.Commands
{
    public class UpdateClientCommandTest
    {
        private readonly Mock<IReadRepository<Client>> _readRepository;
        private readonly Mock<IWriteRepository<Client>> _repository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly UpdateClientCommand.UpdateClientCommandHandler _handler;
        public UpdateClientCommandTest()
        {
            _readRepository = new Mock<IReadRepository<Client>>();
            _repository = new Mock<IWriteRepository<Client>>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _handler = new UpdateClientCommand.UpdateClientCommandHandler(
                    _repository.Object,
                    _readRepository.Object,
                    _unitOfWork.Object
                );
        }

        public static UpdateClientCommand CreateValid(long id = 10) => new()
        {
            Id = id,
            FantasyName = "Teste atualizacao",
            Cnpj = "56.597.930/0001-29",
            Active = true,
        };

        [Fact]
        public async Task DevePermitirAtualizarComDadosValidos()
        {
            //Arrange
            var command = CreateValid();
            var cliente = new Client("Teste atualizacao", new Cnpj("70.438.324/0001-91"));
            _readRepository.Setup(rp => rp.Get(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cliente);
            _readRepository.Setup(rp => rp.Any(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsAsync(false);
            //Act
            var response = await _handler.Handle(command, default);
            //Assert
            Assert.True(response.Success);
            Assert.Equal("Cliente atualizado com sucesso!", response.Message);

            _repository.Verify(x => x.Update(It.IsAny<Client>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task NaoDeveAtualizarClientesInexistentes()
        {
            //Arrange
            var command = CreateValid();

            _readRepository.Setup(rp => rp.Any(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsAsync(false);
            //Act
            var exception = await Assert.ThrowsAsync<NotFoundedExeption>(() => _handler.Handle(command, default));

            //Assert
            Assert.Equal("O cliente informado não existe!", exception.Message);

            _repository.Verify(x => x.Update(It.IsAny<Client>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }
        [Fact]
        public async Task NaoDevePermitirAtualizarComCnpjDuplicado()
        {
            //Arrange
            var command = CreateValid();
            var cliente = new Client("Teste atualizacao", new Cnpj("70.438.324/0001-91"));
            _readRepository.Setup(rp => rp.Get(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cliente);
            _readRepository.Setup(rp => rp.Any(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsAsync(true);
            //Act
            var response = await _handler.Handle(command, default);
            //Assert
            Assert.False(response.Success);
            Assert.Equal("Erro ao realizara ação", response.Message);

            _repository.Verify(x => x.Update(It.IsAny<Client>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);

        }

    }
}
