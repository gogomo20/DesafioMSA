using DesafioMSA.Domain.Shared;
using DesafioMSA.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DesafioMSA.Domain.Entities
{
    public class Client : BaseEntity
    {
        public virtual string FantasyName { get; protected set; }
        public virtual Cnpj Cnpj { get; protected set; }
        public virtual bool Active { get; protected set; }
        public Client(
                string? fantasyName,
                Cnpj cnpj 
            )
        {
            if (string.IsNullOrEmpty(fantasyName)) throw new InvalidException("O nome fantasia deve ser informado!");
            if (fantasyName.Length > 255) throw new InvalidException("O nome fantasia deve possuir no máximo 255 caracteres!");

            FantasyName = fantasyName;
            Cnpj = cnpj;
            Active = true;
        }
        public virtual void UpdateName(string? fantasyName)
        {
            if (string.IsNullOrEmpty(fantasyName)) throw new InvalidException("O nome fantasia deve ser informado!");
            if (fantasyName.Length > 255) throw new InvalidException("O nome fantasia deve possuir no máximo 255 caracteres!");
            FantasyName = fantasyName;
        }
        public virtual void UpdateCnpj(Cnpj cnpj)
        {
            Cnpj = cnpj;
        }
        public virtual void UpdateStatus(bool newStatus)
        {
            Active = newStatus;
        }

        //Este metodo precisa estar aqui para o Nhibernate poder montar a classe
        protected Client() { }
    }
}
