using DesafioMSA.Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace DesafioMSA.Domain.Shared
{
    public class Cnpj : IEquatable<Cnpj>
    {
        public string Value { get; }
        //Este metodo precisa estar aqui pois o NHibernate precisa de um contrutor padrão
        protected Cnpj() { }
        public Cnpj(string? value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidException("O CNPJ informado é invalido");
            value = SomenteNumeros(value);
            if (!CnpjEhValido(value))
                throw new InvalidException("O CNPJ informado é invalido");
            Value = value;
        }

        private static string SomenteNumeros(string value) => Regex.Replace(value, "[^0-9]", "");

        public static bool CnpjEhValido(string cnpj)
        {
            if (cnpj.Length != 14)
                return false;
            if (new string(cnpj[0], cnpj.Length) == cnpj)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cnpj);
        }
        public bool Equals(Cnpj other) =>
            other != null && Value == other.Value;
        public override string ToString() =>
            Convert.ToUInt64(Value).ToString(@"00\.000\.000\/0000\-00");
        public override int GetHashCode() =>
            Value.GetHashCode();

        public static bool operator ==(Cnpj? left, Cnpj? right) =>
            EqualityComparer<Cnpj>.Default.Equals(left, right);

        public static bool operator !=(Cnpj? left, Cnpj? right) =>
            !(left == right);
    }
}
