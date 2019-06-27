using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ShopMasterPOS
{
    public partial class Form1 : Form
    {
        string constring = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ToString();
        public Form1()
        {
            InitializeComponent();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string sql = "select username,password from loginTable where username = @username and password = @password ";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql,con))
                {
                    cmd.Parameters.AddWithValue("@username",userID.Text);
                    cmd.Parameters.AddWithValue("@password", passWord.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    try
                    {
                        string DateTime = System.DateTime.Now.ToString();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (userID.Text == "admin")
                            {
                                AdminArea admin = new AdminArea();
                                admin.Show();
                                this.Hide();
                            }
                            else if (userID.Text != "admin")
                            {
                                SalesUserEnd userEnd = new SalesUserEnd(userID.Text);
                                SaveLoginReports();
                                MessageBox.Show("Welcome " + userID.Text + "\n You are Logged on at " + DateTime, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                userEnd.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or Password!","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
            }
        }

        private void SaveLoginReports()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string saveQ = "insert into LoginReportData (data,date) values (@data,@date)";
                using (SqlCommand cmd = new SqlCommand(saveQ, con))
                {
                    try
                    {
                        string date = System.DateTime.Now.ToString();
                        string data = "User " + userID.Text + " Logged in at " + date + " On ShopMasterPOS";

                        cmd.Parameters.AddWithValue("@data", data);
                        cmd.Parameters.AddWithValue("@date", date);

                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
            }
        }
    }
}
