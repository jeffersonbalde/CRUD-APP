using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace CRUD_APP
{
    public partial class Login : Form
    {
        OleDbConnection cn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\jeffe\OneDrive\Documents\School.accdb");
        SqlDataReader dr;

        bool found = false;
        string username = "";
        string password = "";
        public Login()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                cn.Open();


                string query = "SELECT * FROM Account WHERE username = @username AND password = @password";
                OleDbCommand cm = new OleDbCommand(query, cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                OleDbDataReader dr = cm.ExecuteReader();
                dr.Read();

                if (dr.HasRows) 
                {
                    found = true;
                    username = dr["username"].ToString();
                    password = dr["password"].ToString();

                    Form1 frm = new Form1();
                    frm.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username and Password");
                }

                cn.Close();
                dr.Close();

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
