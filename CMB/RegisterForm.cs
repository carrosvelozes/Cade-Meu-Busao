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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            var nome = txtNome.Text;
            var nascimento = txtNascimento.Text;
            var email = txtEmail.Text;
            var senha = txtSenha.Text;
            var confirmaSenha = txtConfirmarSenha.Text;
            var termosAceitos = checkBoxTermos.Checked;

            if (!termosAceitos || string.IsNullOrWhiteSpace(nome) || senha != confirmaSenha)
            {
                MessageBox.Show("Por favor, corrija as informações!");
                return;
            }

            var dbManager = new DatabaseManager();
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                var query = "INSERT INTO passageiros (nome, nascimento, email, senha) VALUES (@nome, @nascimento, @email, @senha)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@nascimento", nascimento);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@senha", senha);

                    var result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar usuário.");
                    }
                }
            }
        }
    }
}
