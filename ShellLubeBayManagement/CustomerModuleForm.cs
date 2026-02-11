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
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {        
                if (MessageBox.Show("Are you sure you want to save this Customer?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT into tbCustomer(cname,caddress,cphone,cmileage,cfuel,cmodel,cplate)VALUES(@cname,@caddress,@cphone,@cmileage,@cfuel,@cmodel,@cplate)", con);
                    cm.Parameters.AddWithValue("@cname", txtCName.Text);
                    cm.Parameters.AddWithValue("@caddress", txtCAdd.Text);
                    cm.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    cm.Parameters.AddWithValue("@cmileage", txtCMile.Text);
                    cm.Parameters.AddWithValue("@cfuel", txtCFuel.Text);
                    cm.Parameters.AddWithValue("@cmodel", txtCModel.Text);
                    cm.Parameters.AddWithValue("@cplate", txtCPlate.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been successfully Saved");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtCName.Clear();
            txtCPhone.Clear();
            txtCAdd.Clear();
            txtCMile.Clear();
            txtCFuel.Clear();
            txtCModel.Clear();
            txtCPlate.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Customer?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbCustomer SET cname=@cname, caddress=@caddress, cphone=@cphone, cmileage=@cmileage, cfuel=@cfuel, cmodel=@cmodel, cplate=@cplate WHERE cid LIKE '" + lblCid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@cname", txtCName.Text);
                    cm.Parameters.AddWithValue("@caddress", txtCAdd.Text);
                    cm.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    cm.Parameters.AddWithValue("@cmileage", txtCMile.Text);
                    cm.Parameters.AddWithValue("@cfuel", txtCFuel.Text);
                    cm.Parameters.AddWithValue("@cmodel", txtCModel.Text);
                    cm.Parameters.AddWithValue("@cplate", txtCPlate.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been successfully Updated");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxClose_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
