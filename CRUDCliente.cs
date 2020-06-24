using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MetroCRUD
{
    public partial class CRUDCliente : MetroFramework.Forms.MetroForm
    {
        SqlConnection con = new SqlConnection("Data Source=aula2020.database.windows.net;Initial Catalog=DBMetroui;User ID=tds02;Password=@nuvem2020;Connect Timeout=60;Encrypt=True;MultipleActiveResultSets=true;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public CRUDCliente()
        {
            InitializeComponent();
        }

        public void CarregaDGVCliente()
        {
            String query = "SELECT * FROM Cliente";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable cliente = new DataTable();
            da.Fill(cliente);
            metroGrid1.DataSource = cliente;
            con.Close();
        }

        private void MbtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MbtnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Inserir";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", MtxtNome.Text);
                cmd.Parameters.AddWithValue("@celular", MtxtCelular.Text);
                cmd.Parameters.AddWithValue("@email", MtxtEmail.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Cliente cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtxtNome.Text = "";
                MtxtCelular.Text = "";
                MtxtEmail.Text = "";
                CarregaDGVCliente();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void MbtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Atualizar";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.MtxtId.Text);
                cmd.Parameters.AddWithValue("@nome", this.MtxtNome.Text);
                cmd.Parameters.AddWithValue("@celular", this.MtxtCelular.Text);
                cmd.Parameters.AddWithValue("@email", this.MtxtEmail.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Cliente Atualizado com sucesso!", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtxtNome.Text = "";
                MtxtCelular.Text = "";
                MtxtEmail.Text = "";
                CarregaDGVCliente();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void MbtnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Excluir";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.MtxtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro apagado com sucesso!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                MtxtNome.Text = "";
                MtxtCelular.Text = "";
                MtxtEmail.Text = "";
                MtxtId.Text = "";
                CarregaDGVCliente();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void MbtnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Localizar";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", MtxtId.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    MtxtNome.Text = rd["nome"].ToString();
                    MtxtCelular.Text = rd["celular"].ToString();
                    MtxtEmail.Text = rd["email"].ToString();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void CRUDCliente_Load(object sender, EventArgs e)
        {
            CarregaDGVCliente();
        }

        private void metroGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                DataGridViewRow row = this.metroGrid1.Rows[e.RowIndex];
                MtxtId.Text = row.Cells[0].Value.ToString();
                MtxtNome.Text = row.Cells[1].Value.ToString();
                MtxtCelular.Text = row.Cells[2].Value.ToString();
                MtxtEmail.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
