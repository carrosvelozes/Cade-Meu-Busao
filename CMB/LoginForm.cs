using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CMB.BancoDeDados;

namespace CMB
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var senha = txtSenha.Text;

            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM passageiros WHERE email = @email AND senha = @senha";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@senha", senha);

                    var result = Convert.ToInt32(command.ExecuteScalar());

                    if (result > 0)
                    {
                        HomeForm homeForm = new HomeForm();

                        homeForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuário não cadastrado!");
                    }
                }
            }
        }

        private void btnFormCadastro_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();

            registerForm.Show();
        }

        private void btnFormCadastro_Click_1(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();

            registerForm.Show();
        }
    }
}
