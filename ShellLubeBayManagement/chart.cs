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
using System.Windows.Forms.DataVisualization.Charting;

namespace ShellLubeBayManagement
{
    public partial class chart : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hyun\Documents\dbSDMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();

        public chart()
        {
            InitializeComponent();
            Timer t = new Timer();
            t.Interval = 5000; // 5 seconds
            t.Tick += (s, e) => LoadMonthlySalesChart();
            t.Start();
        }

        public void LoadMonthlySalesChart()
        {

            string sql = @"
            SELECT
             pname,
             MONTH(tdate) AS mn,
             SUM(qty) AS total_qty
             FROM tbTransaction
             GROUP BY pname, MONTH(tdate)
             ORDER BY pname, mn;
                                ";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = 12;

            foreach (DataRow row in dt.Rows)
            {
                int[] monthlyTotals = new int[12];
                string product = row["pname"].ToString();
                int month = Convert.ToInt32(row["mn"]);
                int qty = Convert.ToInt32(row["total_qty"]);

                monthlyTotals[month-1] = qty;

                if (!chart1.Series.IsUniqueName(product))
                {
                    chart1.Series[product].Points.AddXY(month, qty);
                }
                else
                {
                    Series s = new Series(product)
                    {
                        ChartType = SeriesChartType.Line,
                        BorderWidth = 2,
                        MarkerStyle = MarkerStyle.Circle,
                    };
                    for (int m = 1; m <= 12; m++)
                    {
                        s.Points.AddXY(m, monthlyTotals[month-1]);
                    }

                    chart1.Series.Add(s);
                }

                
                
            }

        }
    }
}
