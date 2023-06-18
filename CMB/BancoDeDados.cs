using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


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
