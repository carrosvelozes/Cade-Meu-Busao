using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static CMB.BancoDeDados;

namespace CMB
{
    public partial class EnviarForm : Form
    {
        public string Email { get; set; }
        private MySqlConnection connection;
 

         public EnviarForm()
        {
            InitializeComponent();
            PopulateComboBox();
            
        }

        //recebe o email como string
        public EnviarForm(string email) : this()
        {
            Email = email;
        }


        //combo boxes que acessa as linhas de onibus, os tipos de problemas existentes e mostra para o usuario
        //fazendo com que o usuario possa selecionar uma linha e um problema para reportar
        private void PopulateComboBox()
        {

            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                //execao retornando um erro caso algum campo nao seja preenchido
                try
                {
                    connection.Open();

                    //preenche a combobox 1 com a coluna numero da tabela linhas_onibus
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

                    //preenche a combobox 2 com a coluna descricao da tabela problemas_tipo
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


        //ao clicar no botao os conteudos das comboboxes junto ao texto descrito na textrichbox preenchem a tabela problemas_reportados
        private void button1_Click(object sender, EventArgs e)
        {
            //alocando os campos a variaveis
            string linhaSelecionada = comboBox1.SelectedItem?.ToString();
            string problemDescription = richTextBox1.Text.Trim();
            string tipoProblemaSelecionado = comboBox2.SelectedItem?.ToString();
            string userEmail = txtEmail.Text.Trim(); //
            int userId = GetUserId(userEmail);


            //condicionais caso os campos estejam nulos
            if (string.IsNullOrEmpty(linhaSelecionada))
            {
                MessageBox.Show("Selecione uma linha de ônibus.");
                return;
            }

            if (string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Digite a descrição do problema.");
                return;
            }

            if (string.IsNullOrEmpty(tipoProblemaSelecionado))
            {
                MessageBox.Show("Selecione um tipo de problema.");
                return;
            }

            int busLineId = GetBusLineId(linhaSelecionada);
            int problemTypeId = GetProblemTypeId(tipoProblemaSelecionado);

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

            //recupera o tempo atual do computador
            DateTime currentDateTime = DateTime.Now;



            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {

                //execao retornando um erro caso algum campo nao seja preenchido
                try
                {
                    connection.Open();

                    //Insere os dados descricao, data_hora, tipo_problema_id, linha_id, usuario_id a tabela problemas_reportados
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


        //recupera o ID do usuario atraves do email do qual foi enviado o formulario,
        private int GetUserId(string userEmail)
        {
            int userId = 0;
            string query = "SELECT id FROM passageiros WHERE email = @userEmail";


            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                //execao retornando um erro caso o ID do usuario nao seja capturado
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


        
        //recupera o ID da linha selecionada na combobox e verifica o numero da linha de acordo com a opcao selecionada na combobox.
        private int GetBusLineId(string linhaSelecionada)
        {
            int busLineId = 0;
            string query = "SELECT id FROM linhas_onibus WHERE numero = @linhaSelecionada";


            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                //execao retornando um erro caso o ID da linha nao seja obtido
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@linhaSelecionada", linhaSelecionada);

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


        //recupera o ID do problema selecionado na combobox e verifica se a descricao do problema esta de acordo com a opcao selecionada na combobox.
        private int GetProblemTypeId(string tipoProblemaSelecionado)
        {
            int problemTypeId = 0;
            string query = "SELECT id FROM problemas_tipos WHERE descricao = @tipoProblemaSelecionado";


            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                //execao retornando um erro caso o ID do problema nao seja obtido
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tipoProblemaSelecionado", tipoProblemaSelecionado);

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


        //alocando email a textbox txtEmail que se encontra disabled para que o email utilizado para login seja o mesmo email que envia o feedback.
        private void EnviarForm_Load(object sender, EventArgs e)
        {
            txtEmail.Text = Email;

        }

    }
}
