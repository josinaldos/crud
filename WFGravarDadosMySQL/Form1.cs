using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WFGravarDadosMySQL
{
    public partial class Form1 : Form
    {

        MySqlConnection Conexao;
        string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                Conexao = new MySqlConnection(data_source);

                string sql = "INSERT INTO contato (nome,email,telefone)" +
                    "VALUES " +
                    "(' " + j.Text + "','" + txtEmail.Text + "' ,'" + txtTelefone.Text + "')";

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                comando.ExecuteReader();
                MessageBox.Show("Cadastro inserido com sucesso!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);

                string q = "  '% " + txt_buscar.Text + " %' ";
                //SELECT * FROM CONTATO WHERE NOME %JOAOZINHO% 

                string sql = "SELECT * FROM contato WHERE nome LIKE "+q+"OR email LIKE"+q;

                MySqlCommand comando
                     = new MySqlCommand(sql, Conexao);

                Conexao.Open();

                //ler os dados
                MySqlDataReader reader = comando.ExecuteReader();
                //limpa lista
                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                     };
                    //criando uma linha
                    var linhaListView = new ListViewItem(row);
                    //adicionando a linha no lst_contatos
                    lst_contatos.Items.Add(linhaListView);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }

        }
    }
}
