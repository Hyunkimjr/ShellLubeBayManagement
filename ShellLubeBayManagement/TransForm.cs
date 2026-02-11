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
    public partial class TransForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public TransForm()
        {
            InitializeComponent();
            LoadTransaction();
        }


        public void LoadTransaction()
        {
            int i = 0;
            dgvTrans.Rows.Clear();
            cm = new SqlCommand("SELECT transid, tdate, T.cid, C.cname, tdesc, T.pid, P.pname, qty, price, liter, labor, total FROM tbTransaction AS T JOIN tbCustomer as C ON T.cid=C.cid JOIN tbProduct AS P ON T.pid=P.pid  WHERE CONCAT(transid, tdate, T.cid, C.cname, tdesc, T.pid, P.pname, qty, price, liter, labor, total) LIKE '%" + txtSearchTransaction.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvTrans.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TransModuleForm frm = new TransModuleForm();
            frm.ShowDialog();
            LoadTransaction();
        }

        private void dgvTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvTrans.Columns[e.ColumnIndex].Name;
             if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Transaction?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE from tbTransaction WHERE transid LIKE '" + dgvTrans.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();    
                    MessageBox.Show("Record has been successfully deleted");

                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty+@pqty) WHERE pname LIKE '" + dgvTrans.Rows[e.RowIndex].Cells[5].Value.ToString() + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvTrans.Rows[e.RowIndex].Cells[6].Value.ToString()));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadTransaction();
            
        }

    
        private void txtSearchTransaction_TextChanged_1(object sender, EventArgs e)
        {
            LoadTransaction();
        }
    }
}
