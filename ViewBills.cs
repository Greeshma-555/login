using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class ViewBills : Form
    {
        public ViewBills()
        {
            InitializeComponent();
            populate();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        SqlConnection Con = new SqlConnection(@"Data Source=GRLAP01;Initial Catalog=JewelleryDb;Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ViewBills_Load(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }
    }
}
