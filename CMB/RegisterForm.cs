using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using static CMB.BancoDeDados;

namespace CMB
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            //esconde as caracteres do campo senha
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;
            dateTimePickerNascimento.Format = DateTimePickerFormat.Custom;
            dateTimePickerNascimento.CustomFormat = "yyyy/MM/dd";
            var nascimento = dateTimePickerNascimento.Value.ToString("yyyy-MM-dd");
        }



        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            //aloca as informacoes preenchidas nas textbox para as variaveis.

            var nome = txtNome.Text;
            var nascimento = dateTimePickerNascimento.Text;
            var email = txtEmail.Text;
            var senha = txtSenha.Text;
            var confirmaSenha = txtConfirmarSenha.Text;
            var termosAceitos = checkBoxTermos.Checked;

            if (!termosAceitos || string.IsNullOrWhiteSpace(nome) || !email.Contains("@") || senha.Length < 5 || senha != confirmaSenha)
            {
                
                if (!termosAceitos)
                {
                    MessageBox.Show("Você deve aceitar os termos de uso para cadastrar-se.");
                }
                else if (string.IsNullOrWhiteSpace(nome))
                {
                    MessageBox.Show("O nome é obrigatório, preencha-o corretamente.");
                }
                else if (!email.Contains("@"))
                {
                    MessageBox.Show("O email é inválido. Por favor, insira um e-mail válido.");
                }
                else if (senha.Length < 5)
                {
                    MessageBox.Show("A senha deve ter no mínimo 5 caracteres.");
                }
                else if (senha != confirmaSenha)
                {
                    MessageBox.Show("A confirmação de senha não corresponde a sua senha.");
                }

                return;
            }

            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                //query para salvar as informacoes das varivais na tabela passageiros.
                var query = "INSERT INTO passageiros (nome, nascimento, email, senha) VALUES (@nome, @nascimento, @email, @senha)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@nascimento", nascimento);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@senha", senha);

                    var result = command.ExecuteNonQuery();


                    //condicional para verificar o funcionamento do cadastro.
                    if (result > 0)
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar usuário.");
                    }
                }
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual; //forca o form a loadar em uma posicao especifica
            this.Location = new Point(600, 200);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
