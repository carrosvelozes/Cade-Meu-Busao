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

            //esconde as caracteres do campo senha
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            //aloca as informacoes preenchidas nas textbox para as variaveis.

            var nome = txtNome.Text;
            var nascimento = txtNascimento.Text;
            var email = txtEmail.Text;
            var senha = txtSenha.Text;
            var confirmaSenha = txtConfirmarSenha.Text;
            var termosAceitos = checkBoxTermos.Checked;

            if (!termosAceitos || string.IsNullOrWhiteSpace(nome) || senha != confirmaSenha)
            {
                //verificacao se a checkbox foi marcada e tambem se as senhas coincidem.

                MessageBox.Show("Por favor, corrija as informações!");
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

        }
    }
}
