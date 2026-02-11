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
    public partial class TransModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        //initialize qty 
        int qty = 0;
        SqlDataReader dr;
        public TransModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
            numericupdownQty.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT(cid, cname) LIKE '%"+txtSearchCustomer.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
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

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtCID.Clear();
            txtCName.Clear();
            txtPID.Clear();
            txtPName.Clear();
            txtTlabor.Clear();
            txtPdesc.Clear();
            txtPLiters.Clear();
            txtPrice.Clear();
            numericupdownQty.Value = 0;
            
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCID.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPID.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtPdesc.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPLiters.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
            originalLiters.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
        }


            decimal labor = 0;
        private void txtTlabor_TextChanged(object sender, EventArgs e)
        {
            
            decimal.TryParse(txtTlabor.Text, out labor);
            if (string.IsNullOrEmpty(txtTlabor.Text))
            {  
                numericupdownQty.Enabled = false;
            }
            else
            {
                numericupdownQty.Enabled = true;
            }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            


        if(Convert.ToInt16(numericupdownQty.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericupdownQty.Value = numericupdownQty.Value - 1;
                return;
            }

            // If price box is empty, just clear total and skip
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                txtTotal.Text = "0.00";
                return;

            }
               
            decimal price;

            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Invalid price format!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            decimal total = (price * numericupdownQty.Value) + labor;
            txtTotal.Text = total.ToString("0.00");


            int liters = 0;
            int.TryParse(txtPLiters.Text, out liters);

            
            liters = Convert.ToInt16(originalLiters.Text) * Convert.ToInt16(numericupdownQty.Value);
            txtPLiters.Text = liters.ToString();


        }


        private void btnInTrans_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to insert this Transaction?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT into tbTransaction(tdate, cid, cname, tdesc, pid, pname, qty, price, liter, labor, total)VALUES(@tdate, @cid, @cname, @tdesc, @pid, @pname, @qty, @price, @liter, @labor, @total)", con);
                    cm.Parameters.AddWithValue("@tdate", dtTrans.Value);
                    cm.Parameters.AddWithValue("@cid", txtCID.Text);
                    cm.Parameters.AddWithValue("@cname", txtCName.Text);
                    cm.Parameters.AddWithValue("@tdesc", txtPdesc.Text);
                    cm.Parameters.AddWithValue("@pid", txtPID.Text);
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt16(numericupdownQty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                    cm.Parameters.AddWithValue("@liter", Convert.ToInt16(txtPLiters.Text));
                    cm.Parameters.AddWithValue("@labor", Convert.ToDecimal(txtTlabor.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToDecimal(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Transaction has been successfully Saved");
                    

                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty-@pqty) WHERE pid LIKE '" + txtPID.Text + "' ", con);
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



        //Gets the value of the product then stores the value to the variable qty.
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

    }
}
