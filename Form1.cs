using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace agenda_auça_04_08
{
    public partial class Form1 : Form
    {
        string MySqlClientString =
           
            "server=localhost;Port=3306;" +
            "User Id = root; Database = base_teste;" +
            "SSL Mode=0";

        public Form1()
        {
            InitializeComponent();
        }

        public void limpar()
        {
            txt_id.Clear();
            txt_nome.Clear();
            txt_endereco.Clear();
            txt_email.Clear();
            txt_id.Focus();
        }

        private void btn_carregar_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection(MySqlClientString);
            conn.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM cadastro ORDER BY Id", conn);
            da.Fill(dt);
            dgv.DataSource = dt;
        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(MySqlClientString);
                conn.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("select * from cadastro " + "where Id = ' " + txt_id.Text + " ' ", conn);
                da.Fill(dt);
                txt_nome.Text = dt.Rows[0].Field<string>("Nome");
                txt_endereco.Text = dt.Rows[0].Field<string>("Endereco");
                txt_email.Text = dt.Rows[0].Field<string>("Email");

            }
            catch
            {
                limpar();
                MessageBox.Show("Cliente não encontrado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btn_inserir_Click(object sender, EventArgs e)
        {

            string sql = "INSERT INTO cadastro(Id,Nome,Endereco,Email)"
                + "VALUES('" + txt_id.Text + "','"
                + txt_nome.Text + "','" + txt_endereco.Text + "','" + txt_email.Text + "'");
            MySqlConnection conn = new MySqlConnection(MySqlClientString);
            MySqlCommand cmd = new MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            limpar();
            btn_carregar_Click(btn_carregar, e);
            
        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE cadastro set(Nome='"+txt_nome.Text+"',Endereco='"+txt_endereco.Text+"',Email='"+txt_email.Text + "'"\);
            MySqlConnection conn = new MySqlConnection(MySqlClientString);
            MySqlCommand cmd = new MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            limpar();
            MessageBox.Show("Alterado com sucesso", "Alerta");
            
        }
    }
}
