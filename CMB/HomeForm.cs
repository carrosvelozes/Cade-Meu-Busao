using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static CMB.BancoDeDados;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CMB
{
    public partial class HomeForm : Form
    {
        private string connectionString = "Server=localhost;Database=cadastro;Uid=root;Pwd=root;";

        public HomeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarForm consultar = new ConsultarForm();
            consultar.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnviarForm enviarForm = new EnviarForm();
            enviarForm.Show();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Consultar a tabela problemas_reportados e obter a contagem de problemas para cada problemas_tipos
                var query = "SELECT tipo_problema_id, COUNT(*) AS total FROM problemas_reportados WHERE tipo_problema_id IN (1, 2, 3, 4, 5) GROUP BY tipo_problema_id";
                using (var command = new MySqlCommand(query, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();

                    Dictionary<int, int> problemasCount = new Dictionary<int, int>();

                    while (reader.Read())
                    {
                        int tipoProblemaId = Convert.ToInt32(reader["tipo_problema_id"]);
                        int contagem = Convert.ToInt32(reader["total"]);

                        problemasCount.Add(tipoProblemaId, contagem);
                    }

                    reader.Close();

                    // Atualizar as labels com a contagem de problemas_reportados para cada problemas_tipos
                    labelProblema1.Text = "Quantidade de onibus atrasados: " + (problemasCount.ContainsKey(1) ? problemasCount[1].ToString() : "0");
                    labelProblema2.Text = "Quantidade de onibus lotados: " + (problemasCount.ContainsKey(2) ? problemasCount[2].ToString() : "0");
                    labelProblema3.Text = "Quantidade de onibus assaltados: " + (problemasCount.ContainsKey(3) ? problemasCount[3].ToString() : "0");
                    labelProblema4.Text = "Quantidade de onibus acidentados: " + (problemasCount.ContainsKey(4) ? problemasCount[4].ToString() : "0");
                    labelProblema5.Text = "Quantidade de onibus quebrados: " + (problemasCount.ContainsKey(5) ? problemasCount[5].ToString() : "0");
                }
            }
        }

    }
}
