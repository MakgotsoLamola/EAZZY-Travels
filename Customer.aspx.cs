using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eazzy_Travelss
{
    public partial class HomePage : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\makgo\Documents\Eazzy Travelss\Eazzy Travelss\App_Data\Customer.mdf"";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMessage.Text = "Please complete all required fields.";
                return;
            }
            conn.Open();

            cmd = new SqlCommand("INSERT INTO Customer(Name, Age, ContactInfo) " +"VALUES(@Name, @Age, @Contact)", conn);

            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Age", txtAge.Text);
            cmd.Parameters.AddWithValue("@Contact", txtContact.Text);

            cmd.ExecuteNonQuery();

            // Clear the form
            txtName.Text = "";
            txtAge.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            
        }
    
    }
}