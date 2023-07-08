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
using System.CodeDom;

namespace examRevision
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        string strConn = @"Data Source=localhost;Initial Catalog=dbcshap2023;Integrated Security=True";
        string gender;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void showData() 
        {
            dt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM tbEmployee", conn);
            da.Fill(dt);
            dataGridView.DataSource = dt;

            dataGridView.Columns[0].HeaderText = "ລະຫັດພະນັກງານ";
            dataGridView.Columns[1].HeaderText = "ຊື່";
            dataGridView.Columns[2].HeaderText = "ນາມສະກຸນ";
            dataGridView.Columns[3].HeaderText = "ເພດ";

            conn.Close();
        }
        private void clearData()
        {
            txtID.Clear();
            txtName.Clear();
            txtSurname.Clear();
            if (rdBtnFemale.Checked)
            {
                rdBtnFemale.Checked = false;
            } else
            {
                rdBtnMale.Checked = false;
            }
        }

        private string getGender()
        {
            if (rdBtnFemale.Checked)
            {
                return "ຍິງ";
            } else
            {
                return "ຊາຍ";
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            MessageBox.Show("Connection Succeeded!");
            showData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string insertSql;
            conn.Open();

            gender = getGender();

            insertSql = $"INSERT INTO tbEmployee VALUES ({Convert.ToInt32(txtID.Text)}, N'{txtName.Text}', " +
                $"N'{txtSurname.Text}', N'{gender}')";
            cmd = new SqlCommand(insertSql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ການເພີ່ມຂໍ້ມູນສຳເລັດ");
            showData();
            clearData();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string updateSql;
            conn.Open();

            gender = getGender();

            updateSql = $"UPDATE tbEmployee SET ename=N'{txtName.Text}', " +
                $"esurname = N'{txtSurname.Text}', gender=N'{gender}' WHERE eid LIKE {txtID.Text}";
            cmd = new SqlCommand(updateSql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ການແກ້ໄຂຂໍ້ມູນສຳເລັດ");
            showData();
            clearData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deleteSql;
            conn.Open();

            deleteSql = $"DELETE FROM tbEmployee WHERE eid LIKE {txtID.Text}";
            cmd = new SqlCommand(deleteSql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ການລົບຂໍ້ມູນສຳເລັດ");
            showData();
            clearData();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[0].Value.ToString();
            txtName.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[1].Value.ToString();
            txtSurname.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[2].Value.ToString();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string querySql;
            conn.Open();
            querySql = $"SELECT * FROM tbEmployee WHERE eid LIKE {txtSearch.Text}";
            cmd = new SqlCommand(querySql, conn);
            cmd.ExecuteNonQuery();

            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            dataGridView.DataSource = dt;

            txtID.Text = dt.ToString();
            txtName.Text = dt.ToString();
            txtSurname.Text = dt.ToString();

            conn.Close();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            conn.Open();
            showData();
        }
    }
}
