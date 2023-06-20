using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CMB
{
    public partial class EnviarForm : Form
    {
        private MySqlConnection connection;
        private const string connectionString = "Server=localhost;Database=cadastro;Uid=root;Pwd=root;";
        public string EmailSalvo { get; set; }

        public EnviarForm()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Preencher ComboBox1 com os números das linhas de ônibus
                    string query1 = "SELECT numero FROM linhas_onibus";
                    using (MySqlCommand command = new MySqlCommand(query1, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString("numero"));
                            }
                        }
                    }

                    // Preencher ComboBox2 com os tipos de problemas existentes
                    string query2 = "SELECT descricao FROM problemas_tipos";
                    using (MySqlCommand command = new MySqlCommand(query2, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox2.Items.Add(reader.GetString("descricao"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao preencher as ComboBoxes: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedBusLine = comboBox1.SelectedItem?.ToString();
            string problemDescription = richTextBox1.Text.Trim();
            string selectedProblemType = comboBox2.SelectedItem?.ToString();
            string userEmail = txtEmail.Text.Trim(); // Supondo que haja um campo de email no formulário
            int userId = GetUserId(userEmail);

            if (string.IsNullOrEmpty(selectedBusLine))
            {
                MessageBox.Show("Selecione uma linha de ônibus.");
                return;
            }

            if (string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Digite a descrição do problema.");
                return;
            }

            if (string.IsNullOrEmpty(selectedProblemType))
            {
                MessageBox.Show("Selecione um tipo de problema.");
                return;
            }

            int busLineId = GetBusLineId(selectedBusLine);
            int problemTypeId = GetProblemTypeId(selectedProblemType);

            if (busLineId == 0)
            {
                MessageBox.Show("Linha de ônibus inválida.");
                return;
            }

            if (problemTypeId == 0)
            {
                MessageBox.Show("Tipo de problema inválido.");
                return;
            }

            DateTime currentDateTime = DateTime.Now;

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO problemas_reportados (descricao, data_hora, tipo_problema_id, linha_id, usuario_id) VALUES (@descricao, @dataHora, @tipoProblemaId, @linhaId, @usuarioId)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@descricao", problemDescription);
                        command.Parameters.AddWithValue("@dataHora", currentDateTime);
                        command.Parameters.AddWithValue("@tipoProblemaId", problemTypeId);
                        command.Parameters.AddWithValue("@linhaId", busLineId);
                        command.Parameters.AddWithValue("@usuarioId", userId);


                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Feedback enviado. Obrigado pela avaliação!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao enviar o feedback: " + ex.Message);
                }
            }
        }

        private int GetUserId(string userEmail)
        {
            int userId = 0;
            string query = "SELECT id FROM passageiros WHERE email = @userEmail";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userEmail", userEmail);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int id))
                        {
                            userId = id;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao obter o ID do usuário: " + ex.Message);
                }
            }

            return userId;
        }


        private int GetBusLineId(string selectedBusLine)
        {
            int busLineId = 0;
            string query = "SELECT id FROM linhas_onibus WHERE numero = @selectedBusLine";

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@selectedBusLine", selectedBusLine);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int id))
                        {
                            busLineId = id;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao obter o ID da linha de ônibus: " + ex.Message);
                }
            }

            return busLineId;
        }

        private int GetProblemTypeId(string selectedProblemType)
        {
            int problemTypeId = 0;
            string query = "SELECT id FROM problemas_tipos WHERE descricao = @selectedProblemType";

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@selectedProblemType", selectedProblemType);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int id))
                        {
                            problemTypeId = id;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao obter o ID do tipo de problema: " + ex.Message);
                }
            }

            return problemTypeId;
        }

        private void EnviarForm_Load(object sender, EventArgs e)
        {

        }
    }
}
