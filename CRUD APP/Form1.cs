using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Data.OleDb;

namespace CRUD_APP
{
    public partial class Form1 : Form
    {

        OleDbConnection cn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\jeffe\OneDrive\Documents\School.accdb");

        public Form1()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {

                cn.Open();
                string query = "SELECT * FROM student";
                OleDbCommand cm = new OleDbCommand(query, cn);
                cm.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter dp = new OleDbDataAdapter(cm);
                dp.Fill(dt);
                dataGridView1.DataSource = dt;
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearInput()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtEDP.Text = "";
            txtCourse.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(@"C:\Users\jeffe\OneDrive\Documents\inter font\inter\inter-Regular.ttf");
            foreach (Control c in this.Controls)
            {
                c.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            }

            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                cn.Open();
                string query = "INSERT INTO student(StudentName, StudentEDP, StudentCourse) VALUES(@name, @edp, @course)";
                OleDbCommand cm = new OleDbCommand(query, cn);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.Parameters.AddWithValue("@edp", txtEDP.Text);
                cm.Parameters.AddWithValue("@course", txtCourse.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Student added successfully", "CRUD APP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                cn.Open();
                string query = "UPDATE student SET StudentName = @name, StudentEDP = @edp, StudentCourse = @course WHERE ID = @id";
                OleDbCommand cm = new OleDbCommand(query, cn);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.Parameters.AddWithValue("@edp", txtEDP.Text);
                cm.Parameters.AddWithValue("@course", txtCourse.Text);
                cm.Parameters.AddWithValue("@id", txtID.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Student updated!", "CRUD APP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInput();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                cn.Open();
                string query = "DELETE * FROM student WHERE ID=@id";
                OleDbCommand cm = new OleDbCommand(query, cn);
                cm.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Student deleted!", "CRUD APP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {  
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtEDP.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtCourse.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();   
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
