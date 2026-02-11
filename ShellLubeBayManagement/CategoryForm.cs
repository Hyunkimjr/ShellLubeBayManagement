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
namespace ShellLubeBayManagement
{
    public partial class CategoryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCategory WHERE CONCAT(catid, catname) LIKE '%" + txtSearchCategory.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        } 

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModuleForm CategoryModule = new CategoryModuleForm();
            CategoryModule.btnSave.Enabled = true;
            CategoryModule.btnUpdate.Enabled = false;
            CategoryModule.ShowDialog();
            LoadCategory();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm CategoryModule = new CategoryModuleForm();
                CategoryModule.lblCatID.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                CategoryModule.txtCatName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();


                CategoryModule.btnSave.Enabled = false;
                CategoryModule.btnUpdate.Enabled = true;
                CategoryModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Category?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE from tbCategory WHERE cid LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            LoadCategory();
        }

        private void txtSearchCategory_TextChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }
    }
}
