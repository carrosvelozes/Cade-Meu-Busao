using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static CMB.BancoDeDados;

namespace CMB
{
    public partial class ConsultarForm : Form
    {
        private readonly DatabaseManager dbManager;

        public ConsultarForm()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            string query = "SELECT numero FROM linhas_onibus";

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
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
            string linhaSelecionada = comboBox1.SelectedItem.ToString();

            string query1 = "SELECT * FROM linhas_onibus WHERE numero = @linhaSelecionada";

            string query2 = "SELECT COUNT(*) AS TotalProblemas, problemas_tipos.descricao AS ProblemasMaisReportados " +
                            "FROM problemas_reportados " +
                            "INNER JOIN linhas_onibus ON problemas_reportados.linha_id = linhas_onibus.id " +
                            "INNER JOIN problemas_tipos ON problemas_reportados.tipo_problema_id = problemas_tipos.id " +
                            "WHERE linhas_onibus.numero = @linhaSelecionada " +
                            "GROUP BY problemas_tipos.descricao " +
                            "ORDER BY COUNT(*) DESC " +
                            "LIMIT 1";

            string query3 = "SELECT problemas_reportados.descricao AS Feedback, problemas_reportados.data_hora AS Data, problemas_tipos.descricao AS Tipo " +
                            "FROM linhas_onibus " +
                            "INNER JOIN problemas_reportados ON linhas_onibus.id = problemas_reportados.linha_id " +
                            "INNER JOIN problemas_tipos ON problemas_reportados.tipo_problema_id = problemas_tipos.id " +
                            "WHERE linhas_onibus.numero = @linhaSelecionada";

            string query4 = "SELECT veiculos.placa " +
                            "FROM veiculos " +
                            "JOIN linhas_onibus ON veiculos.linha_id = linhas_onibus.id " +
                            "WHERE linhas_onibus.numero = @linhaSelecionada";

            string query5 = "SELECT nomes_motoristas.nome " +
                            "FROM motoristas " +
                            "JOIN linhas_onibus ON motoristas.linha_id = linhas_onibus.id " +
                            "JOIN nomes_motoristas ON motoristas.nome_id = nomes_motoristas.id " +
                            "WHERE linhas_onibus.numero = @linhaSelecionada";

            string query6 = "SELECT paradas.nome " +
                 "FROM paradas " +
                 "JOIN linhas_onibus ON linhas_onibus.id = linhas_onibus.id " +
                 "WHERE linhas_onibus.numero = @linhaSelecionada " +
                 "LIMIT 1;";


            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader.GetString("numero");
                            textBox2.Text = reader.GetString("nome");
                        }
                    }
                }

                using (var command = new MySqlCommand(query4, connection))
                {
                    command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox5.Text = reader.GetString("placa");
                        }
                    }
                }

                using (var command = new MySqlCommand(query6, connection))
                {
                    command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

                    using (var reader = command.ExecuteReader())
                    {
                        // Limpe o conteúdo atual da TextBox7
                        textBox7.Text = "";

                        while (reader.Read())
                        {
                            string nomeParada = reader.GetString("nome");
                            textBox7.Text += nomeParada + Environment.NewLine; // Adicione um novo nome de parada em uma nova linha
                        }
                    }
                }


                using (var command = new MySqlCommand(query5, connection))
                {
                    command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox6.Text = reader.GetString("nome");
                        }
                    }
                }

                using (var command = new MySqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalProblemas = reader.GetInt32("TotalProblemas");
                            textBox3.Text = totalProblemas.ToString();
                            textBox4.Text = reader.GetString("ProblemasMaisReportados");
                        }
                    }
                }

                using (var command = new MySqlCommand(query3, connection))
                {
                    command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

                    using (var reader = command.ExecuteReader())
                    {
                        dataGridView.Columns.Clear();
                        dataGridView.Rows.Clear();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataGridView.Columns.Add(reader.GetName(i), reader.GetName(i));
                        }

                        while (reader.Read())
                        {
                            object[] row = new object[reader.FieldCount];

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.GetName(i) == "Data" && reader[i] is DateTime)
                                {
                                    row[i] = ((DateTime)reader[i]).ToString("yyyy-MM-dd"); // Formato da data desejado
                                }
                                else
                                {
                                    row[i] = reader[i];
                                }
                            }

                            dataGridView.Rows.Add(row);
                        }

                        //define os campos do datagridview para fill
                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                }

                connection.Close();
            }
        }

    }
}
