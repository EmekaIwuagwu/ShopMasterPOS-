using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopMasterPOS
{
    public partial class SalesUserEnd : Form
    {
        decimal SumValue = 0;
        string invoiceNumber = generateInvoiceNumber();
        int updateVolume;
        string uAttendant;


        string constring = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ToString();
        public SalesUserEnd(string userAttendant)
        {
            InitializeComponent();
            uAttendant = userAttendant;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string query = "select * from productsmarket_Table where productname like '"+product_search.Text+"%' ";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string productName = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
                string price = dataGridView1.SelectedRows[0].Cells[3].Value + string.Empty;

                chprodName.Text = productName;
                price_2.Text = price;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (CheckForInStockGoods())
            {
                decimal netPrice = Convert.ToDecimal(prod_qty.Text) * Convert.ToDecimal(price_2.Text);
                string finalPrice = Convert.ToString(netPrice);
                string DateOfPurchase = System.DateTime.Now.ToString();

                string notify = "These items would be added to Tray, Please confirm?";
                DialogResult result = MessageBox.Show(notify, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    listView1.View = View.Details;
                    ListViewItem lvItem = listView1.Items.Add(chprodName.Text);
                    lvItem.SubItems.Add(price_2.Text);
                    lvItem.SubItems.Add(prod_qty.Text);
                    lvItem.SubItems.Add(finalPrice);
                    lvItem.SubItems.Add(DateOfPurchase);
                }
            }

        }

        private bool CheckForInStockGoods()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string checkQuery = "select * from productsmarket_Table where productname like '" + product_search.Text + "%' ";
                using (SqlCommand cmd = new SqlCommand(checkQuery, con))
                {
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        int availableQty = Convert.ToInt32((rd["productQty"].ToString()));
                        if (availableQty == 0)
                        {
                            MessageBox.Show("You cannot Place This Order\n You appear to be out of stock!", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        public static string generateInvoiceNumber()
        {
            string shopMasterPrologue = "#SHOPMAS1";
            Random rand = new Random();
            int randomLength = rand.Next(1, 6900);
            string FinalRandom = shopMasterPrologue + randomLength;
            return FinalRandom;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            decimal finalPrice = Convert.ToDecimal(excess.Text) - Convert.ToDecimal(totalPrice.Text);
            //decimal FinalBalance = Convert.ToDecimal(finalPrice); // Aww Snap, i dont need this sir!
            bal.Text = Convert.ToString(finalPrice);
            button3.Enabled = false;
            button3.Text = "Balance Calculated";
            string changeInfo = "Recieved by " + uAttendant + " For The Products: " + getAllboughtProds() + "With Excess Balace of : "+bal.Text + " And Excess Payment of "+ excess.Text + " On " + System.DateTime.Now;
            UpdateProductQuantity();
            SaveTransactionRecord(invoiceNumber, changeInfo);
        }

        private void SalesUserEnd_Load(object sender, EventArgs e)
        {
            label9.Text = " ";
            toolStripStatusLabel1.Text = "Connected";

            //make These Hidden at start!
            InvoiceNumberLabel.Text = "";
            priceLabel.Text = "";
            dateLabel.Text = "";
            salesAttendantLabel.Text = "";
            invoiceNumberViewLabel.Text = "";
            priceViewLabel.Text = "";
            salesAttendantViewLabel.Text = "";
            dateViewLabel.Text = "";
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            foreach (ListViewItem lstItem in listView1.Items)
            {
                SumValue += decimal.Parse(lstItem.SubItems[3].Text);
                label9.Text = "Total : NGN "+ SumValue;
                totalPrice.Text = Convert.ToString(SumValue);
                button5.Enabled = false;
                button5.Text = "Calculated";
                excess.Text = "0.00";
                bal.Text = "0.00";

                InvoiceNumberLabel.Text = "Invoice # :";
                priceLabel.Text = "Calculated Price:";
                dateLabel.Text = "Date :";
                salesAttendantLabel.Text = "Attendant : ";
                invoiceNumberViewLabel.Text = invoiceNumber;
                priceViewLabel.Text = totalPrice.Text;
                salesAttendantViewLabel.Text = uAttendant;
                dateViewLabel.Text = System.DateTime.Now.ToString();

                //MessageBox.Show(uAttendant);

            }

            DialogResult res = MessageBox.Show("Price as at checkout is : " + SumValue + " Please Select Yes to Confirm Order", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string info = "Recieved by " + uAttendant + " For The Products: "+ getAllboughtProds() + " On "+System.DateTime.Now;
                MessageBox.Show("Order Complete!");
                //Save for Transaction and generate 
                UpdateProductQuantity();
                SaveTransactionRecord(invoiceNumber,info);

            }
        }

        private void UpdateProductQuantity()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                //Starting calls.
                //Select all first to check for Quantity
                string updateQtyQuery = "select* from productsmarket_Table where productname like '"+product_search.Text+"%' ";
                using (SqlCommand cmd = new SqlCommand(updateQtyQuery, con))
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            int orginalQty = Convert.ToInt32((rd["productQty"].ToString()));
                            updateVolume = orginalQty - Convert.ToInt32(prod_qty.Text);
                        }
                    }

                    string updateSqlQuantity = "update productsmarket_Table set productQty = @productQty where productname= @productname";
                    using (SqlCommand cmd2 = new SqlCommand(updateSqlQuantity, con))
                    {
                        try
                        {
                            cmd2.Parameters.AddWithValue("@productQty", updateVolume);
                            cmd2.Parameters.AddWithValue("@productname", product_search.Text);
                            cmd2.ExecuteNonQuery();

                            //Works Good!
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        private void SaveTransactionRecord(string invoiceNumber,string data)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string saveQuery = "insert into salesTransactionTable (invoiceNumber,data,transactionDate) values (@invoiceNumber,@data,@transactionDate)";
                using (SqlCommand cmd = new SqlCommand(saveQuery,con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@invoiceNumber",invoiceNumber);
                        cmd.Parameters.AddWithValue("@data", data);
                        cmd.Parameters.AddWithValue("@transactionDate", System.DateTime.Now);
                        cmd.ExecuteNonQuery();
                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private string getAllboughtProds()
        {
            string allBoughtItems = null;
            foreach (ListViewItem lstItem in listView1.Items)
            {
                allBoughtItems += lstItem.SubItems[0].Text + ", ";
            }
            return allBoughtItems;
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 100, y = 100;
            /*Image newImage = Properties.Resources.logo;
            int width = 150, height = 70;
            int ix = x, iy = y; //image position
            e.Graphics.DrawImage(newImage, ix, iy, width, height);*/
            var header = new Font("Calibri", 21, FontStyle.Bold);
            int hdy = (int)header.GetHeight(e.Graphics); //30; //line height spacing

            var fnt = new Font("Courier New", 10, FontStyle.Bold);
            int dy = (int)fnt.GetHeight(e.Graphics); //20; //line height spacing

            e.Graphics.DrawString("Receipt", header, Brushes.Black, new PointF(x, y)); y += hdy;
            e.Graphics.DrawString("                            Date: " + System.DateTime.Now, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("Attendant Name: " + uAttendant, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("Invoice #: " + invoiceNumber, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("-----------------------------------------------------------", fnt, Brushes.Black, new PointF(x, y)); y += dy;

            e.Graphics.DrawString(" Product Name  "+"   |  "+" Price " + "   |  "+" Qty" +"   |  "+ " Net Price " , fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("-----------------------------------------------------------", fnt, Brushes.Black, new PointF(x, y)); y += dy;

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                // Draw the row details for ? receipt 
                e.Graphics.DrawString(" " + listView1.Items[i].SubItems[0].Text +"              |  " + listView1.Items[i].SubItems[1].Text +"        |   "+
                  listView1.Items[i].SubItems[2].Text + "          | "+ listView1.Items[i].SubItems[3].Text, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            }
            e.Graphics.DrawString("---------------------", fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("Total : " + totalPrice.Text, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("Excess : " + excess.Text, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("Balance : " + bal.Text, fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("------------------------------------------------------", fnt, Brushes.Black, new PointF(x, y)); y += dy;
            e.Graphics.DrawString("Thank you for your Patronage ", fnt, Brushes.Black, new PointF(x, y)); y += dy;
        }

        private void GroupBox6_Enter(object sender, EventArgs e)
        {

        }
    }
}

