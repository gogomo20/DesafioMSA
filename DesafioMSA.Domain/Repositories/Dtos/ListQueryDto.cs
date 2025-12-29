using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace DesafioMSA.Domain.Repositories.Dtos
{
    public class ListQueryDto<T>
    {
        public string OrderBy { get; init; } = "Id";
        public bool Ascending { get; init; }    
        public int Size { get; init; }
        public int Page { get; init; }
        public ICollection<Expression<Func<T, bool>>> WhereFunctions { get; private set; } = [];
        public void Where(Expression<Func<T, bool>> expression)
        {
            WhereFunctions.Append(expression);
        }
    }
}
