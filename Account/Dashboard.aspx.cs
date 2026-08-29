using System;

public partial class Account_Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Account/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            litUsername.Text = ", " + Convert.ToString(Session["Username"]);
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Account/Login.aspx");
    }
}
