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
    public partial class DeliveryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public DeliveryForm()
        {
            InitializeComponent();
            LoadDeliveries();
        }

        private void LoadDeliveries()
        {
            int i = 0;
            dgvDeliveries.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbDelivery WHERE CONCAT(did, ddate, pid, pname, pqty, pprice, pltr, pdesc, pctgry) LIKE '%" + txtSearchDeliveries.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvDeliveries.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
            }
            dr.Close();
            con.Close();
        }

        

        private void txtSearchDeliveries_TextChanged_1(object sender, EventArgs e)
        {
            LoadDeliveries();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DeliveryModuleForm DeliveryModule = new DeliveryModuleForm();
            DeliveryModule.ShowDialog();
            LoadDeliveries();
        }

        private void dgvDeliveries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvDeliveries.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Delivery?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE from tbDelivery WHERE did LIKE '" + dgvDeliveries.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Delivery has been successfully deleted");
                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty-@pqty) WHERE pid LIKE '" + dgvDeliveries.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvDeliveries.Rows[e.RowIndex].Cells[5].Value));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    
                    LoadDeliveries();
                }

                LoadDeliveries();
            }
        }
    }
}
