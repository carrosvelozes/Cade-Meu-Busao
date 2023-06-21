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

        public string UserName { get; set; }
        public string Email { get; set; }

        public HomeForm()
        {
            InitializeComponent();
        }


        //abre o EnviarForm
        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarForm consultar = new ConsultarForm();

            Hide();
            consultar.ShowDialog();
            Show();
        }

        //abre o ConsultarForm
        private void button2_Click(object sender, EventArgs e)
        {
            EnviarForm enviarForm = new EnviarForm(Email); //passa o email como argumento
            Hide();
            enviarForm.ShowDialog();
            Show();
        }


        //ao carregar o HomeForm, o UserName recuperado na tabela passageiros de acordo com o email inseridono LoginForm, 
        //eh salvo e apresentado na lblWelcome junto ao texto de boas vindas.
        private void HomeForm_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(UserName))
            {
                lblWelcome.Text = "Seja bem-vindo(a), " + UserName;
            }

            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                //consulta a tabela problemas_reportados e obtem a contagem de problemas para cada problema em problemas_tipos
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

                    //escreve nas labels a contagem de problemas_reportados para cada problemas_tipos
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
