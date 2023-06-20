using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CMB
{
    public partial class ConsultarForm : Form
    {
        private string connectionString = "Server=localhost;Database=cadastro;Uid=root;Pwd=root;";

        public ConsultarForm()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            string query = "SELECT numero FROM linhas_onibus";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
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

            // Query to get line name and count of reported issues
            string query1 = "SELECT * FROM linhas_onibus WHERE numero = @selectedBusLine";
            string query2 = "SELECT COUNT(*) AS TotalProblems, pt.descricao AS MostReportedProblem FROM problemas_reportados pr INNER JOIN linhas_onibus lo ON pr.linha_id = lo.id INNER JOIN problemas_tipos pt ON pr.tipo_problema_id = pt.id WHERE lo.numero = @selectedBusLine GROUP BY pt.descricao";
            string query3 = "Select problemas_reportados.descricao, problemas_reportados.data_hora, problemas_tipos.descricao\r\nFrom linhas_onibus inner join problemas_reportados \r\n\ton linhas_onibus.id = problemas_reportados.linha_id inner join problemas_tipos\r\n\ton problemas_reportados.tipo_problema_id = problemas_tipos.id\r\n    Where linhas_onibus.numero = @selectedBusLine;";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                            textBox3.Text = reader["TotalProblems"].ToString();
                            textBox4.Text = reader.GetString("MostReportedProblem");
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand(query3, connection))
                {
                    command.Parameters.AddWithValue("@selectedBusLine", selectedBusLine);
                    MySqlDataReader reader = command.ExecuteReader();
                    
                        dataGridView.Rows.Clear(); // Limpa as colunas existentes, se houver

                        // Adiciona as colunas ao DataGridView
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataGridView.Columns.Add(reader.GetName(i), reader.GetName(i));
                        }

                        // Adiciona as linhas ao DataGridView
                        while (reader.Read())
                        {
                            object[] row = new object[reader.FieldCount];
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];
                            }
                            dataGridView.Rows.Add(row);
                            dataGridView.Rows.Clear();

                        int rowIndex = dataGridView.Rows.Add(row); // Obtém o índice da linha recém-adicionada

                            dataGridView.Rows.Add(row);

                            if (dataGridView.Rows.Count == 1) // Verifica se é a primeira tabela
                            {
                                dataGridView.Columns[0].FillWeight = 70; // Ajuste o valor do FillWeight conforme necessário
                            }

                            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Configura o preenchimento automático para as demais colunas
                    }
                    reader.Close();
                    connection.Close();                   
                }

            }
        }



        private void ConsultarForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        // ... Outros métodos e eventos
    }
}
