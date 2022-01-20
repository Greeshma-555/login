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
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void FilterByCat()
        {
            Con.Open();
            string query = "select * from ItemTbl where ItCat='"+FilterCat.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FilterByType()
        {
            Con.Open();
            string query = "select * from ItemTbl where ItType='" + FilterType.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ItName.Text == "" || PriceTb.Text == "" || QtyTb.Text == "" || CatCb.SelectedIndex == -1 || TypeCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into ItemTbl values('" + ItName.Text + "','" + CatCb.SelectedItem.ToString()+"','"+TypeCb.SelectedItem.ToString() + "',"+ PriceTb.Text + "," + QtyTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Saved Successfully");
                    Con.Close();
                    populate();
                    Reset();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItName.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.SelectedItem = ItemDGV.SelectedRows[0].Cells[2].Value.ToString();
            TypeCb.SelectedItem = ItemDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ItemDGV.SelectedRows[0].Cells[4].Value.ToString();
            QtyTb.Text = ItemDGV.SelectedRows[0].Cells[5].Value.ToString();
            if(ItName.Text == "")
            {
                key = 0;
            }else
            {
                key = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
            ItName.Text = "";
            CatCb.SelectedIndex = -1;
            TypeCb.SelectedIndex = -1;
            PriceTb.Text = "";
            QtyTb.Text = "";
            key = 0;
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from ItemTbl where ItId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Deleted Successfully");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (ItName.Text == "" || PriceTb.Text == "" || QtyTb.Text == "" || CatCb.SelectedIndex == -1 || TypeCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update ItemTbl set ItName='" + ItName.Text + "',ItCat='" + CatCb.SelectedItem.ToString() + "',ItType='" + TypeCb.SelectedItem.ToString() + "',ItPrice='" + PriceTb.Text + "',ItQty='" + QtyTb.Text + "'where ItId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Updated Successfully");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void ItName_TextChanged(object sender, EventArgs e)
        {

        }

        private void FilterCat_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void FilterType_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void FilterCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCat();
        }

        private void FilterType_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            FilterByType();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoginForm Obj = new LoginForm();
            Obj.Show();
            this.Hide();
        }
    }
}
