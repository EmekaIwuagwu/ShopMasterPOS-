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
    public partial class AdminArea : Form
    {
        string constring = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ToString();
        public AdminArea()
        {
            InitializeComponent();
        }

        private void AdminArea_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Connected";
        }
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                fullname.Enabled = true;
                telephone.Enabled = true;
                username.Enabled = true;
                password.Enabled = true;
                c_password.Enabled = true;
                access_type.Enabled = true;
                address.Enabled = true;
                state.Enabled = true;
            } else
            {
                fullname.Enabled = false;
                telephone.Enabled = false;
                username.Enabled = false;
                password.Enabled = false;
                c_password.Enabled = false;
                access_type.Enabled = false;
                address.Enabled = false;
                state.Enabled = false;
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            fullname.Text = " ";
            telephone.Text = " ";
            username.Text = " ";
            password.Text = " ";
            c_password.Text = " ";
            access_type.Text = " ";
            address.Text = " ";
            state.Text = " ";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sqlinsert = "insert into loginTable (fullname,telephone,username,password,access_type) values (@fullname,@telephone,@username,@password,@access_type)";
                using (SqlCommand cmd = new SqlCommand(sqlinsert, con))
                {
                    try
                    {
                        if (c_password.Text != password.Text)
                        {
                            MessageBox.Show("Password does not Match", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@fullname", fullname.Text);
                            cmd.Parameters.AddWithValue("@telephone", telephone.Text);
                            cmd.Parameters.AddWithValue("@username", username.Text);
                            cmd.Parameters.AddWithValue("@password", password.Text);
                            cmd.Parameters.AddWithValue("@access_type", access_type.Text);

                            DialogResult res = MessageBox.Show("Please Confirm?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                string confirmText = "User Created \n Username : " + username.Text + " \n Password : " + password.Text + "\n Please Save For Reference";
                                MessageBox.Show(confirmText, "User Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                fullname.Text = " ";
                                telephone.Text = " ";
                                username.Text = " ";
                                password.Text = " ";
                                c_password.Text = " ";
                                access_type.Text = " ";
                                address.Text = " ";
                                state.Text = " ";
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'LoginReports.LoginReportData' table. You can move, or remove it, as needed.
            this.LoginReportDataTableAdapter.Fill(this.LoginReports.LoginReportData,dateFrom.Text,dateTo.Text);
            this.reportViewer1.RefreshReport();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'TransactionDataSet.salesTransactionTable' table. You can move, or remove it, as needed.
            this.salesTransactionTableTableAdapter.Fill(this.TransactionDataSet.salesTransactionTable, dateFrom.Text, dateTo.Text);
            this.reportViewer2.RefreshReport();
        }
    }
}
