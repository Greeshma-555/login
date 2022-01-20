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

namespace login
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.JewelleryDbCon);
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtpass.Text == "" || txtadmin.Text == "")
            {
                MessageBox.Show("Enter UserName and Password");


            }
            else if (txtadmin.Text == "Admin" && txtpass.Text == "Password")
            {
                Items Obj = new Items();
                Obj.Show();
                this.Hide();
            }

            
        }

       

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }
    }
}
