using System;
using MySql.Data.MySqlClient;

public partial class PlaneProvider : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadEmployees();
            LoadPlaneProviders();
        }
    }

    // Fills the Employee dropdown list
    private void LoadEmployees()
    {
        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand("SELECT EmployeeID, FirstName FROM Employee", conn);
            conn.Open();
            ddlEmployee.DataSource = cmd.ExecuteReader();
            ddlEmployee.DataBind();
        }
    }

    // Loads all plane provider records into the GridView
    private void LoadPlaneProviders()
    {
        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand("SELECT * FROM PlaneProvider ORDER BY PlaneID", conn);
            conn.Open();
            gvPlaneProviders.DataSource = cmd.ExecuteReader();
            gvPlaneProviders.DataBind();
        }
    }

    // Checks whether a Location/PlaneType/Tickets combination already exists
    private bool RecordExists(string location, string planeType, string tickets, int excludeId = 0)
    {
        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM PlaneProvider WHERE Location=@loc AND PlaneType=@type " +
                "AND Tickets=@tickets AND PlaneID <> @excludeId", conn);
            cmd.Parameters.AddWithValue("@loc", location);
            cmd.Parameters.AddWithValue("@type", planeType);
            cmd.Parameters.AddWithValue("@tickets", tickets);
            cmd.Parameters.AddWithValue("@excludeId", excludeId);
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }

    // ADD new plane provider record
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        if (RecordExists(txtLocation.Text.Trim(), txtPlaneType.Text.Trim(), txtTickets.Text.Trim()))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Warning: A matching plane provider record already exists.";
            return;
        }

        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand(
                "INSERT INTO PlaneProvider (Location, PlaneType, Tickets, Services, EmployeeID) " +
                "VALUES (@loc, @type, @tickets, @services, @empId)", conn);
            cmd.Parameters.AddWithValue("@loc", txtLocation.Text.Trim());
            cmd.Parameters.AddWithValue("@type", txtPlaneType.Text.Trim());
            cmd.Parameters.AddWithValue("@tickets", txtTickets.Text.Trim());
            cmd.Parameters.AddWithValue("@services", txtServices.Text.Trim());
            cmd.Parameters.AddWithValue("@empId", ddlEmployee.SelectedValue);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.ForeColor = System.Drawing.Color.Green;
        lblMessage.Text = "Plane provider record added successfully.";
        ClearForm();
        LoadPlaneProviders();
    }

    // UPDATE existing plane provider record
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid || string.IsNullOrEmpty(hfPlaneID.Value))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Select a record from the table before updating.";
            return;
        }

        int planeId = Convert.ToInt32(hfPlaneID.Value);

        if (RecordExists(txtLocation.Text.Trim(), txtPlaneType.Text.Trim(), txtTickets.Text.Trim(), planeId))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Warning: A matching plane provider record already exists.";
            return;
        }

        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand(
                "UPDATE PlaneProvider SET Location=@loc, PlaneType=@type, Tickets=@tickets, " +
                "Services=@services, EmployeeID=@empId WHERE PlaneID=@id", conn);
            cmd.Parameters.AddWithValue("@loc", txtLocation.Text.Trim());
            cmd.Parameters.AddWithValue("@type", txtPlaneType.Text.Trim());
            cmd.Parameters.AddWithValue("@tickets", txtTickets.Text.Trim());
            cmd.Parameters.AddWithValue("@services", txtServices.Text.Trim());
            cmd.Parameters.AddWithValue("@empId", ddlEmployee.SelectedValue);
            cmd.Parameters.AddWithValue("@id", planeId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.ForeColor = System.Drawing.Color.Green;
        lblMessage.Text = "Plane provider record updated successfully.";
        ClearForm();
        LoadPlaneProviders();
    }

    // DELETE plane provider record
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfPlaneID.Value))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Select a record from the table before deleting.";
            return;
        }

        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand("DELETE FROM PlaneProvider WHERE PlaneID=@id", conn);
            cmd.Parameters.AddWithValue("@id", hfPlaneID.Value);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.ForeColor = System.Drawing.Color.Green;
        lblMessage.Text = "Plane provider record deleted successfully.";
        ClearForm();
        LoadPlaneProviders();
    }

    // Loads the selected GridView row into the form for editing
    protected void gvPlaneProviders_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvPlaneProviders.SelectedRow;
        hfPlaneID.Value = gvPlaneProviders.DataKeys[row.RowIndex].Value.ToString();
        txtLocation.Text = row.Cells[2].Text;
        txtPlaneType.Text = row.Cells[3].Text;
        txtTickets.Text = row.Cells[4].Text;
        txtServices.Text = row.Cells[5].Text;
        ddlEmployee.SelectedValue = row.Cells[6].Text;
        lblMessage.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
        lblMessage.Text = "";
    }

    private void ClearForm()
    {
        hfPlaneID.Value = "";
        txtLocation.Text = "";
        txtPlaneType.Text = "";
        txtTickets.Text = "";
        txtServices.Text = "";
    }
}
