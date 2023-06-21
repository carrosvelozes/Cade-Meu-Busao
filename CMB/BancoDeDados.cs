using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static CMB.BancoDeDados;




/*classe criada para realizar conexoes com o retorno do connectionString,
entao todo metodo do qual realizamos uma query podemos chamar:

        var dbManager = new DatabaseManager();
        using (var connection = dbManager.GetConnection())
*/
namespace CMB
{
    public class BancoDeDados
    {
        public class DatabaseManager
        {
            private string connectionString = "Server=localhost;Database=cadastro;Uid=root;Pwd=root;";

            public MySqlConnection GetConnection()
            {
                return new MySqlConnection(connectionString);
            }
        }
    }
}
