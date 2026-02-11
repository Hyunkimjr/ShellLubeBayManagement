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
    public partial class CustomerForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            CustomerModuleForm CustomerModule = new CustomerModuleForm();
            CustomerModule.btnSave.Enabled = true;
            CustomerModule.btnUpdate.Enabled = false;
            CustomerModule.ShowDialog();
            LoadCustomer();
        }


        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm customerModule = new CustomerModuleForm();
                customerModule.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModule.txtCAdd.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                customerModule.txtCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                customerModule.txtCMile.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
                customerModule.txtCFuel.Text = dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString();
                customerModule.txtCModel.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
                customerModule.txtCPlate.Text = dgvCustomer.Rows[e.RowIndex].Cells[8].Value.ToString();


                customerModule.btnSave.Enabled = false;
                customerModule.btnUpdate.Enabled = true;
                customerModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Customer?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE from tbCustomer WHERE cid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            LoadCustomer();
        }
    }
}
