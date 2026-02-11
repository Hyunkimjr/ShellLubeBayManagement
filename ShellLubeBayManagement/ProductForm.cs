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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
            HighlightLowStockRows();
        }

        public void LoadProduct()
        {
            int i = 0;
               dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pqty, preorderlevel, pprice, pltr, pdesc, pctgry) LIKE '%" + txtSearchProduct.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            con.Close();
        }


        private void HighlightLowStockRows()
        {
            foreach (DataGridViewRow row in dgvProduct.Rows)
            {
                if (row.IsNewRow) continue;

                int qty = Convert.ToInt32(row.Cells["Qty"].Value);
                int reorder = Convert.ToInt32(row.Cells["preorderlevel"].Value);

                if (qty <= reorder)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            ProductModuleForm ProductModule = new ProductModuleForm();
            ProductModule.btnSave.Enabled = true;
            ProductModule.btnUpdate.Enabled = false;
            ProductModule.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModule = new ProductModuleForm();
                productModule.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.txtPQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.txtPPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.txtPLtr.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.txtPDesc.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                productModule.comboPCtgry.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString();

                productModule.btnSave.Enabled = false;
                productModule.btnUpdate.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE from tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            LoadProduct();
            HighlightLowStockRows();
        }


        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        
    }
}
