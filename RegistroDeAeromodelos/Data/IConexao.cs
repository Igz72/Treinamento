using System;
using System.Data.Common;

namespace RegistroDeAeromodelos.Data
{
    public interface IConexao
    {
        void ExecutarComando(string sql);
        void LerTabela(string sql, Func<DbDataReader, bool> lambda);
    }
}
