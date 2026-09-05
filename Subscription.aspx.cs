using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eazzy_Travelss
{
    public partial class Subscription : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\makgo\Documents\Eazzy Travelss\Eazzy Travelss\App_Data\Customer.mdf"";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            int customerID = Convert.ToInt32(txtCustomerID.Text);
            string subscriptionType = ddlSubscriptionType.SelectedValue;
            DateTime startDate = Calendar1.SelectedDate;
            string length = ddlLength.SelectedValue;

            cmd = new SqlCommand("INSERT INTO SUBSCRIBER(CustomerID, SubscriptionType, SubscriptionDate, Length) " +
                "VALUES(@CustomerID, @SubscriptionType, @StartDate, @Length)", conn);

            cmd.Parameters.AddWithValue("@CustomerID", customerID);
            cmd.Parameters.AddWithValue("@SubscriptionType", subscriptionType);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@Length", length);

            cmd.ExecuteNonQuery();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            conn.Open();

            int customerID = Convert.ToInt32(txtCustomerID.Text);
            string subscriptionType = ddlSubscriptionType.SelectedValue;
            DateTime startDate = Calendar1.SelectedDate;
            string length = ddlLength.SelectedValue;

            cmd = new SqlCommand("UPDATE Subscriber SET " + "SubscriptionType = @SubscriptionType, " +"SubscriptionDate = @StartDate, " +"Length = @Length " + "WHERE CustomerID = @CustomerID", conn);

            cmd.Parameters.AddWithValue("@CustomerID", customerID);
            cmd.Parameters.AddWithValue("@SubscriptionType", subscriptionType);
            cmd.Parameters.AddWithValue("@SubscriptionDate", startDate);
            cmd.Parameters.AddWithValue("@Length", length);

            cmd.ExecuteNonQuery();
        }
    }
}