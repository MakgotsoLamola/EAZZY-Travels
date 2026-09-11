using System;
using MySql.Data.MySqlClient;

public partial class TransportProvider : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadEmployees();
            LoadTransportProviders();
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

    // Loads all transport provider records into the GridView
    private void LoadTransportProviders()
    {
        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand("SELECT * FROM TransportProvider ORDER BY TransportID", conn);
            conn.Open();
            gvTransportProviders.DataSource = cmd.ExecuteReader();
            gvTransportProviders.DataBind();
        }
    }

    // Checks whether a Location/TransportType/Insurance combination already exists
    private bool RecordExists(string location, string transportType, string insurance, int excludeId = 0)
    {
        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM TransportProvider WHERE Location=@loc AND TransportType=@type " +
                "AND Insurance=@insurance AND TransportID <> @excludeId", conn);
            cmd.Parameters.AddWithValue("@loc", location);
            cmd.Parameters.AddWithValue("@type", transportType);
            cmd.Parameters.AddWithValue("@insurance", insurance);
            cmd.Parameters.AddWithValue("@excludeId", excludeId);
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }

    // ADD new transport provider record
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        if (RecordExists(txtLocation.Text.Trim(), txtTransportType.Text.Trim(), txtInsurance.Text.Trim()))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Warning: A matching transport provider record already exists.";
            return;
        }

        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand(
                "INSERT INTO TransportProvider (Location, TransportType, Insurance, Service, EmployeeID) " +
                "VALUES (@loc, @type, @insurance, @service, @empId)", conn);
            cmd.Parameters.AddWithValue("@loc", txtLocation.Text.Trim());
            cmd.Parameters.AddWithValue("@type", txtTransportType.Text.Trim());
            cmd.Parameters.AddWithValue("@insurance", txtInsurance.Text.Trim());
            cmd.Parameters.AddWithValue("@service", txtService.Text.Trim());
            cmd.Parameters.AddWithValue("@empId", ddlEmployee.SelectedValue);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.ForeColor = System.Drawing.Color.Green;
        lblMessage.Text = "Transport provider record added successfully.";
        ClearForm();
        LoadTransportProviders();
    }

    // UPDATE existing transport provider record
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid || string.IsNullOrEmpty(hfTransportID.Value))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Select a record from the table before updating.";
            return;
        }

        int transportId = Convert.ToInt32(hfTransportID.Value);

        if (RecordExists(txtLocation.Text.Trim(), txtTransportType.Text.Trim(), txtInsurance.Text.Trim(), transportId))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Warning: A matching transport provider record already exists.";
            return;
        }

        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand(
                "UPDATE TransportProvider SET Location=@loc, TransportType=@type, Insurance=@insurance, " +
                "Service=@service, EmployeeID=@empId WHERE TransportID=@id", conn);
            cmd.Parameters.AddWithValue("@loc", txtLocation.Text.Trim());
            cmd.Parameters.AddWithValue("@type", txtTransportType.Text.Trim());
            cmd.Parameters.AddWithValue("@insurance", txtInsurance.Text.Trim());
            cmd.Parameters.AddWithValue("@service", txtService.Text.Trim());
            cmd.Parameters.AddWithValue("@empId", ddlEmployee.SelectedValue);
            cmd.Parameters.AddWithValue("@id", transportId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.ForeColor = System.Drawing.Color.Green;
        lblMessage.Text = "Transport provider record updated successfully.";
        ClearForm();
        LoadTransportProviders();
    }

    // DELETE transport provider record
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfTransportID.Value))
        {
            lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            lblMessage.Text = "Select a record from the table before deleting.";
            return;
        }

        using (var conn = DBConnection.GetConnection())
        {
            var cmd = new MySqlCommand("DELETE FROM TransportProvider WHERE TransportID=@id", conn);
            cmd.Parameters.AddWithValue("@id", hfTransportID.Value);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.ForeColor = System.Drawing.Color.Green;
        lblMessage.Text = "Transport provider record deleted successfully.";
        ClearForm();
        LoadTransportProviders();
    }

    // Loads the selected GridView row into the form for editing
    protected void gvTransportProviders_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvTransportProviders.SelectedRow;
        hfTransportID.Value = gvTransportProviders.DataKeys[row.RowIndex].Value.ToString();
        txtLocation.Text = row.Cells[2].Text;
        txtTransportType.Text = row.Cells[3].Text;
        txtInsurance.Text = row.Cells[4].Text;
        txtService.Text = row.Cells[5].Text;
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
        hfTransportID.Value = "";
        txtLocation.Text = "";
        txtTransportType.Text = "";
        txtInsurance.Text = "";
        txtService.Text = "";
    }
}
