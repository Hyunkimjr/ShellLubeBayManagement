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
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboPCtgry.Items.Clear();
            cm = new SqlCommand("SELECT catname FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboPCtgry.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtPDesc.Clear();
            txtPName.Clear();
            txtPPrice.Clear();
            txtPLtr.Clear();
            txtPQty.Clear();
            comboPCtgry.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT into tbProduct(pname, pqty, pprice, pltr, pdesc, pctgry)VALUES(@pname, @pqty, @pprice, @pltr, @pdesc, @pctgry)", con);
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQty.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToDecimal(txtPPrice.Text));
                    cm.Parameters.AddWithValue("@pltr", Convert.ToInt16(txtPLtr.Text));
                    cm.Parameters.AddWithValue("@pdesc", txtPDesc.Text);
                    cm.Parameters.AddWithValue("@pctgry", comboPCtgry.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully Saved");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Product?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET pname=@pname,pqty=@pqty,pprice=@pprice,pltr=@pltr,pdesc=@pdesc,pctgry=@pctgry,preorderlevel=@preorderlevel WHERE pid LIKE '" + lblPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pname", txtPName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQty.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToDecimal(txtPPrice.Text));
                    cm.Parameters.AddWithValue("@pltr", Convert.ToInt16(txtPLtr.Text));
                    cm.Parameters.AddWithValue("@pdesc", txtPDesc.Text);
                    cm.Parameters.AddWithValue("@pctgry", comboPCtgry.Text);
                    cm.Parameters.AddWithValue("@preorderlevel", nudReorderLevel.Value);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully Updated");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
