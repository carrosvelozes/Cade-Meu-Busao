using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static CMB.BancoDeDados;

namespace CMB
{
    public partial class ConsultarForm : Form
    {

        public ConsultarForm()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        //comboBox que seleciona a linha de onibus
        private void PopulateComboBox()
        {
            string query = "SELECT numero FROM linhas_onibus";

            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString("numero"));
                        }
                    }
                }
            }
        }


        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBusLine = comboBox1.SelectedItem.ToString();

            //querys de innerjoin que selecionam dados sobre os problemas reportados da linha selecionada na combobox
            string query1 = "SELECT * FROM linhas_onibus WHERE numero = @selectedBusLine";
            string query2 = "SELECT COUNT(*) AS TotalProblems, pt.descricao AS MostReportedProblem " +
                            "FROM problemas_reportados pr " +
                            "INNER JOIN linhas_onibus lo ON pr.linha_id = lo.id " +
                            "INNER JOIN problemas_tipos pt ON pr.tipo_problema_id = pt.id " +
                            "WHERE lo.numero = @selectedBusLine " +
                            "GROUP BY pt.descricao " +
                            "ORDER BY COUNT(*) DESC " +
                            "LIMIT 1";
            string query3 = "SELECT problemas_reportados.descricao, problemas_reportados.data_hora, problemas_tipos.descricao " +
                            "FROM linhas_onibus " +
                            "INNER JOIN problemas_reportados ON linhas_onibus.id = problemas_reportados.linha_id " +
                            "INNER JOIN problemas_tipos ON problemas_reportados.tipo_problema_id = problemas_tipos.id " +
                            "WHERE linhas_onibus.numero = @selectedBusLine";

            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@selectedBusLine", selectedBusLine);
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader.GetString("numero");
                            textBox2.Text = reader.GetString("nome");
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@selectedBusLine", selectedBusLine);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalProblems = reader.GetInt32("TotalProblems");
                            textBox3.Text = totalProblems.ToString();
                            textBox4.Text = reader.GetString("MostReportedProblem");
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand(query3, connection))
                {
                    command.Parameters.AddWithValue("@selectedBusLine", selectedBusLine);
                    MySqlDataReader reader = command.ExecuteReader();

                    //limpa as linhas e colunas do DataGridView para que as informacoes nao se acumulem a cada vez que uma linha eh consultada
                    dataGridView.Columns.Clear();
                    dataGridView.Rows.Clear();

                    //adiciona as colunas ao DataGridView
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataGridView.Columns.Add(reader.GetName(i), reader.GetName(i));
                    }

                    //adiciona as linhas ao DataGridView
                    while (reader.Read())
                    {
                        object[] row = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dataGridView.Rows.Add(row);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void ConsultarForm_Load(object sender, EventArgs e)
        {

        }

    }
}
