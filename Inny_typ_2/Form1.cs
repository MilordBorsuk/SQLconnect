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
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Data.SqlServerCe;

namespace Inny_typ_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HasłoTXT.PasswordChar = '*';
            HasłoTXT.MaxLength = 25;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID ={2}; Password={3};", comboBox1.Text, BaseName.Text, LoginTXT.Text, HasłoTXT.Text);
            try
            {
                PomocSQl helper = new PomocSQl(connectionString);
                if (helper.IsConnection)
                {
                    MessageBox.Show("Baza danych została połaczona.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dataGridView1.DataSource = BaseName.Text;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void LoginTXT_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            comboBox1.Items.Add(".");
            comboBox1.Items.Add("(local)");
            comboBox1.Items.Add(@".\SQLEXPRESS");
            comboBox1.Items.Add(@".\SQLEX");
            comboBox1.Items.Add(string.Format(@"{0}\SQLEX", Environment.MachineName));
            comboBox1.SelectedIndex = 3;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            BaseName.Clear();
            LoginTXT.Clear();
            HasłoTXT.Clear();
            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            this.Validate();
            SqlCeEngine myEngine = new SqlCeEngine();
            myEngine.LocalConnectionString = comboBox1.Text;
            myEngine.CreateDatabase();
            //Przcisk do tworzenia bazy danych wpisanej w oknie SQL,
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*string query = textBox4.Text;
            string comonnn = BaseName.Text;
            SqlCeConnection con = new SqlCeConnection("Data Source = comboBox1.Text");
            SqlCeDataAdapter da = new SqlCeDataAdapter();
            DataTable dt = new DataTable();
            SqlCeCommand cmd = new SqlCeCommand();
            DataSet ds = new DataSet();

            con.ConnectionString = comonnn;
            cmd.Connection = con;
            cmd.CommandText = query;
            con.Open();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            con.Close();
            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }else
            {
                MessageBox.Show("Błąd połączenia, spróbuj jeszcze raz", "Wiadomość", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            SqlCeCommand cmd;
            string ZapytanieSQl = string.Empty;
            SqlCeConnection con = new SqlCeConnection(string.Format("Data Source = comboBox1.sdf"));
              
            
            try
            {
                using (SqlCeEngine ce = new SqlCeEngine(con.ConnectionString))
                {
                    if (!ce.Verify())
                    {
                        ce.CreateDatabase();
                            if (con.State == ConnectionState.Closed)
                            {
                            con.Open();
                            con.Close();
                            }

                    }
                }
            }

            catch (Exception ex)
            {
                string byk = string.Format("Problem w czasie tworzenia bazy danych: \n{0}", ex.Message);
                MessageBox.Show(byk, "Błąd Kuuurwa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         try { 
                if (con == null)
                    return;
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem w czasie tworzenia tabel: \n {0}", ex.Message);
                MessageBox.Show(byk, "Wiadomo, raczej to nie jest pozytywny komunikat :D", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            


        }
    }
}
