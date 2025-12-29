using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Shared;
using DesafioMSA.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Test.Domain
{
    public class ClientTest
    {
        private static Cnpj CnpjValido() => new Cnpj("55.137.733/0001-64");

        [Fact]
        public void DeveCriarComDadosValidos()
        {
            //Arrange
            var nomeFantasia = "nome teste valido";
            var cnpj = CnpjValido();

            //Act
            var client = new Client(nomeFantasia, cnpj);

            //Assert
            Assert.Equal(nomeFantasia, client.FantasyName);
            Assert.Equal(cnpj, client.Cnpj);
            Assert.True(client.Active);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAceitarNomeFantasiaNuloOuVazio(string? nomeFantasia)
        {
            //Arrange
            var cnpj = CnpjValido();

            //Act
            var exception = Assert.Throws<InvalidException>(() => new Client(nomeFantasia, cnpj));

            //Assert
            Assert.Contains("O nome fantasia deve ser informado!", exception.Message);
        }
        [Fact]
        public void NaoDeveAceitarNomeFantasiaComMaisDe255Caracteres()
        {
            //Arrange
            var nomeFantasia = new string('a', 256);

            //Act
            var exception = Assert.Throws<InvalidException>(() => new Client(nomeFantasia, CnpjValido()));

            //Assert
            Assert.Contains("O nome fantasia deve possuir no máximo 255 caracteres!", exception.Message);
        }
        [Fact]
        public void DevePermitirAtualizarNomeFantasia()
        {
            //Arrange
            var cliente = new Client("valor antigo", CnpjValido());
            var nomeNovo = "Nome novo";
            //Act
            cliente.UpdateName(nomeNovo);

            //Assert
            Assert.Equal(nomeNovo, cliente.FantasyName);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAceitarAtualizarNomeVazioOuNulo(string? nomeNovo)
        {
            //Arrange
            var cliente = new Client("valor antigo", CnpjValido());

            //Act
            var exception = Assert.Throws<InvalidException>(() => cliente.UpdateName(nomeNovo));

            //Assert
            Assert.Contains("O nome fantasia deve ser informado!", exception.Message);
        }

        [Fact]
        public void NaoDeveAceitarAtualizarNomeComMaisDe255Caracteres()
        {
            //Arrange
            var cliente = new Client("Valor antigo", CnpjValido());
            var nomeNovo = new string('a', 256);
            //Act
            var exception = Assert.Throws<InvalidException>(() => cliente.UpdateName(nomeNovo));
            //Assert
            Assert.Contains("O nome fantasia deve possuir no máximo 255 caracteres!", exception.Message);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AtualizarOStatus(bool newStatus)
        {
            //Arrange
            var cliente = new Client("Valor antigo", CnpjValido());

            //Act
            cliente.UpdateStatus(newStatus);

            //Assert
            Assert.Equal(cliente.Active, newStatus);
        }
    }
}
