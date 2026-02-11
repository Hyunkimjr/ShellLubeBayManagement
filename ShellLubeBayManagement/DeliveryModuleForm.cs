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
    public partial class DeliveryModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public DeliveryModuleForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPID.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtPdesc.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPLiters.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtCtgry.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pltr, pdesc, pctgry) LIKE '%" + txtSearchProduct.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            con.Close();
        }


        private void txtSearchProduct_TextChanged_1(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnInDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to insert this Delivery?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT into tbDelivery (ddate, pid, pname, pqty, pprice, pltr, pdesc, pctgry)VALUES(@ddate, @pid, @pname, @pqty, @pprice, @pltr, @pdesc, @pctgry)", con);
                    cm.Parameters.AddWithValue("@ddate", dtDel.Value);
                    cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                    cm.Parameters.AddWithValue("@pid", txtPID.Text);
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(numericupdownQty.Value));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToDecimal(txtPrice.Text));
                    cm.Parameters.AddWithValue("@pltr", Convert.ToInt16(txtPLiters.Text));
                    cm.Parameters.AddWithValue("@pctgry", txtCtgry.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Delivery has been successfully Saved");


                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty+@pqty) WHERE pid LIKE '" + txtPID.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(numericupdownQty.Value));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Clear()
        { 
            txtPID.Clear();
            txtPName.Clear();
            txtPdesc.Clear();
            txtPLiters.Clear();
            txtPrice.Clear();
            numericupdownQty.Value = 0;
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid ='" + txtPID.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}
