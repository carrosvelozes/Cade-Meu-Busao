using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CMB.BancoDeDados;


namespace CMB
{

    public partial class LoginForm : Form
    {
        public string Email { get; private set; }


        public LoginForm()
        {
            InitializeComponent();

            //esconde as caracteres do campo senha
            txtSenha.UseSystemPasswordChar = true;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        //metodo pra obter o nome de usuario de acordo com o @email utilizado para login.
        private string ObterNomeUsuario(string email)
        {
            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                var query = "SELECT nome FROM passageiros WHERE email = @email";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    var nomeUsuario = command.ExecuteScalar()?.ToString();
                    return nomeUsuario;
                }
            }
        }


        //evento de clique do botao login
        private void btnLogin_Click(object sender, EventArgs e)
        {

            //variaveis para guardar email e senha de acordo com o que for preenchido nas textbox
            var email = txtEmail.Text;
            var senha = txtSenha.Text;

            //conexao ao banco por meio da classe BancoDeDados.cs
            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                //select que verifica se a conta existe na tabela passageiros
                var query = "SELECT COUNT(*) FROM passageiros WHERE email = @email AND senha = @senha";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@senha", senha);

                    var result = Convert.ToInt32(command.ExecuteScalar());

                    if (result > 0)
                    {
                        var nomeUsuario = ObterNomeUsuario(email); //funcao para obter o nome com base no email adicionada a variavel nomeUsuario
                        Email = email; //atribui o valor de email para a propriedade Email


                        Hide();
                        HomeForm homeForm = new HomeForm();
                        homeForm.UserName = nomeUsuario;
                        homeForm.Email = email; //aqui o email passa para o HomeForm pelos getters e setters
                        homeForm.ShowDialog();

                        string emailUsuario = homeForm.Email;
                        EnviarForm enviarForm = new EnviarForm();
                        enviarForm.Email = emailUsuario; //aqui o email passa para o EnviarForm pelos getters e setters

                        enviarForm.ShowDialog();
                        Show();

                    }
                    else
                    {
                        //retorno da condicional caso o usuario nao esteja cadastrado
                        MessageBox.Show("Usuário não cadastrado!");
                    }
                   
                }
            }
        }

        //abre o formulario de cadastro
        private void btnFormCadastro_Click_1(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();

            Hide();
            registerForm.ShowDialog();
            Show();
        }
    }
}
