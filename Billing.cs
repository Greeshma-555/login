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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
            displayCust();
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
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void displayCust()
        {
            Con.Open();
            string query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int key = 0, stock = 0;
        int CustKey = 0;
        private void Update()
        {
            try
            {
                int newStock = stock - Convert.ToInt32(QtyTb.Text);
                Con.Open();
                string query = "update ItemTbl set ItQty='" + newStock + "'where ItId=" + key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Item Updated Successfully");
                Con.Close();
                populate();
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int n = 0, GridTotal=0;
        private void AddtoBillBtn_Click(object sender, EventArgs e)
        {
            if(QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text)>stock)
            {
                MessageBox.Show("No Stock");
            }else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdNameTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                
                GridTotal = GridTotal + total;
                Totallbl.Text = "Rs" +GridTotal;
                n++;
                Update();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into BillTbl values('" + CustKey + "','" + CustnameTb.Text + "',"+GridTotal+")";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved Successfully");
                Con.Close();
                populate();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        int proid, prodqty, prodprice, total, pos = 60;

        private void btnlogin_Click(object sender, EventArgs e)
        {
            ViewBills Obj = new ViewBills();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoginForm Obj = new LoginForm();
            Obj.Show();
            this.Hide();
        }

        private void Reset()
        {
            ProdNameTb.Text = "";
            PriceTb.Text = "";
            CustnameTb.Text = "";
            QtyTb.Text = "";
            key = 0;
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Jewellery Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT   PRICE    QUANTITY  TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in BillDGV.Rows)
            {
                proid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                total = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString(""+proid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total: RS" + GridTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("********Jewellery Shop********", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(11, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GridTotal = 0;
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                CustnameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
                

                if (CustnameTb.Text == "")
                {
                    CustKey = 0;
                }
                else
                {
                    CustKey = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
        }

        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTb.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();
            PriceTb.Text = ItemDGV.SelectedRows[0].Cells[4].Value.ToString();
            
            if (ProdNameTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[5].Value.ToString());
            }
        }
    }
}
