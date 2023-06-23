using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
            LinhaMaisPedidos();
            TotalRec();
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
                lblWelcome.Text = "Seja bem-vindo(a) de volta, " + UserName;
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
                    labelProblema1.Text = "Atraso: " + (problemasCount.ContainsKey(1) ? problemasCount[1].ToString() : "0");
                    labelProblema2.Text = "Lotacao: " + (problemasCount.ContainsKey(2) ? problemasCount[2].ToString() : "0");
                    labelProblema3.Text = "Seguranca: " + (problemasCount.ContainsKey(3) ? problemasCount[3].ToString() : "0");
                    labelProblema4.Text = "Qualidade: " + (problemasCount.ContainsKey(4) ? problemasCount[4].ToString() : "0");
                    labelProblema5.Text = "Outros: " + (problemasCount.ContainsKey(5) ? problemasCount[5].ToString() : "0");

                    List<DataPoint> dataPoints = new List<DataPoint>();

                    //configura o grafico para abrir no formato circular (pie).
                    chartTipos.Series.Clear();
                    chartTipos.Series.Add(new Series("Problemas"));
                    chartTipos.Series["Problemas"].ChartType = SeriesChartType.Pie;
                    chartTipos.Series["Problemas"].BorderColor = System.Drawing.Color.Black;
                    chartTipos.Series["Problemas"].BorderWidth = 1;
                    chartTipos.Series["Problemas"].BorderDashStyle = ChartDashStyle.Solid;
                    chartTipos.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
                    chartTipos.Legends[0].BackColor = System.Drawing.Color.Transparent;                   
                    chartTipos.Palette = ChartColorPalette.SemiTransparent;
                    chartTipos.Series["Problemas"].Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);
                    chartTipos.Legends[0].Font = new Font("Consolas", 14f);
                    chartTipos.Series["Problemas"]["PieLabelStyle"] = "Disabled";
                    //adiciona os pontos ao grafico
                    chartTipos.Series["Problemas"].Points.AddXY("Atraso", problemasCount.ContainsKey(1) ? problemasCount[1] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Lotação", problemasCount.ContainsKey(2) ? problemasCount[2] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Seguranca", problemasCount.ContainsKey(3) ? problemasCount[3] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Qualidade", problemasCount.ContainsKey(4) ? problemasCount[4] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Outros", problemasCount.ContainsKey(5) ? problemasCount[5] : 0);
                    chartTipos.Width = 450;
                    chartTipos.Height = 350;

                    //atualiza o grafico
                    chartTipos.Update();
                }
            }
        }

        //metodo para atualizar o grafico manualmente chamando a funcao ao clicar no botao
        private void AtualizarGrafico()
        {
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
                    labelProblema1.Text = "Atraso: " + (problemasCount.ContainsKey(1) ? problemasCount[1].ToString() : "0");
                    labelProblema2.Text = "Lotacao: " + (problemasCount.ContainsKey(2) ? problemasCount[2].ToString() : "0");
                    labelProblema3.Text = "Seguranca: " + (problemasCount.ContainsKey(3) ? problemasCount[3].ToString() : "0");
                    labelProblema4.Text = "Qualidade: " + (problemasCount.ContainsKey(4) ? problemasCount[4].ToString() : "0");
                    labelProblema5.Text = "Outros: " + (problemasCount.ContainsKey(5) ? problemasCount[5].ToString() : "0");

                    //limpa os pontos
                    chartTipos.Series["Problemas"].Points.Clear();

                    //adiciona os novos pontos
                    chartTipos.Series["Problemas"].Points.AddXY("Atraso", problemasCount.ContainsKey(1) ? problemasCount[1] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Lotação", problemasCount.ContainsKey(2) ? problemasCount[2] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Seguranca", problemasCount.ContainsKey(3) ? problemasCount[3] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Qualidade", problemasCount.ContainsKey(4) ? problemasCount[4] : 0);
                    chartTipos.Series["Problemas"].Points.AddXY("Outros", problemasCount.ContainsKey(5) ? problemasCount[5] : 0);

                    //atualiza
                    chartTipos.Update();
                }
            }
        }
        private void LinhaMaisPedidos()
        {
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                

                //consulta a tabela para a linha com maior numero de reclamacoes
                string consulta = "SELECT linhas_onibus.numero FROM linhas_onibus INNER JOIN problemas_reportados ON linhas_onibus.id = problemas_reportados.linha_id GROUP BY linhas_onibus.numero ORDER BY COUNT(problemas_reportados.descricao) DESC LIMIT 1";
                using (var command = new MySqlCommand(consulta, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string valor = reader.GetString(0);
                        labelLinhaMais.Text = valor;
                    }
                }
                
            }
        }
        
        private void TotalRec()
        {
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();


                //consulta a tabela para a linha com maior numero de reclamacoes
                string consulta3 = "SELECT COUNT(id) AS quantidade_reclamacoes FROM problemas_reportados ";
                using (var command1 = new MySqlCommand(consulta3, connection))
                {
                    MySqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        string valor3 = reader.GetString(0);
                        labelTotal.Text = valor3;
                    }
                }

            }
        }





        private void button3_Click(object sender, EventArgs e)
        {
            //chamada das funcoes para atualizar os graficos e informacoes da home
            AtualizarGrafico();
            LinhaMaisPedidos();
            TotalRec();
        }
    }
}
