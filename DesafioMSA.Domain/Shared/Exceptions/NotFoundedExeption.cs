using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Domain.Shared.Exceptions
{
    public class NotFoundedExeption : Exception
    {
        public NotFoundedExeption(string Message): base(Message) { }
    }

}
