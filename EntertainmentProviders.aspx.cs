using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace EazzyTravels
{
    public partial class EntertainmentProviders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffId"] == null) Session["StaffId"] = "EMP-001";
            if (Session["Role"] == null) Session["Role"] = "Employee";

            if (!IsPostBack)
            {
                txtStaffId.Text = Session["StaffId"].ToString();
                ddlRole.SelectedValue = Session["Role"].ToString();
                PopulateActivityFilter();
                BindGrid();
            }
        }

        protected void btnSetSession_Click(object sender, EventArgs e)
        {
            Session["StaffId"] = string.IsNullOrWhiteSpace(txtStaffId.Text) ? "EMP-001" : txtStaffId.Text.Trim();
            Session["Role"] = ddlRole.SelectedValue;
            BindGrid();
        }

        // ---------- Grid ----------

        private void PopulateActivityFilter()
        {
            ddlActivityFilter.Items.Clear();
            ddlActivityFilter.Items.Add(new System.Web.UI.WebControls.ListItem("All activities", ""));
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand("SELECT DISTINCT Activity FROM EntertainmentProvider ORDER BY Activity", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        ddlActivityFilter.Items.Add(new System.Web.UI.WebControls.ListItem(reader.GetString(0), reader.GetString(0)));
                }
            }
        }

        private void BindGrid()
        {
            string search = txtSearch.Text.Trim();
            string activityFilter = ddlActivityFilter.SelectedValue;

            var sql = new StringBuilder("SELECT EntertainmentID, Activity, Food, Location, EmployeeID FROM EntertainmentProvider WHERE 1=1");
            if (!string.IsNullOrEmpty(activityFilter)) sql.Append(" AND Activity = @activity");
            if (!string.IsNullOrEmpty(search)) sql.Append(" AND (EntertainmentID LIKE @search OR Activity LIKE @search OR Location LIKE @search)");
            sql.Append(" ORDER BY EntertainmentID");

            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand(sql.ToString(), conn))
            {
                if (!string.IsNullOrEmpty(activityFilter)) cmd.Parameters.AddWithValue("@activity", activityFilter);
                if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                conn.Open();
                var da = new MySqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                gvEntertainment.DataSource = dt;
                gvEntertainment.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlActivityFilter.SelectedValue = "";
            BindGrid();
        }

        protected void gvEntertainment_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lnkDelete = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("lnkDelete");
                bool isAdmin = Session["Role"] != null && Session["Role"].ToString() == "Administrator";
                lnkDelete.Enabled = isAdmin;
                lnkDelete.ToolTip = isAdmin ? "" : "Only administrators can delete records";
                if (!isAdmin) lnkDelete.CssClass += " disabled";
            }
        }

        protected void gvEntertainment_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string entertainmentId = e.CommandArgument.ToString();

            if (e.CommandName == "EditRecord")
            {
                LoadRecordIntoForm(entertainmentId);
            }
            else if (e.CommandName == "DeleteRecord")
            {
                if (Session["Role"] == null || Session["Role"].ToString() != "Administrator")
                {
                    ShowAlert("Only administrators can delete records.");
                    return;
                }

                using (var conn = DbHelper.GetConnection())
                using (var cmd = new MySqlCommand("DELETE FROM EntertainmentProvider WHERE EntertainmentID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", entertainmentId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ShowSuccess($"Record {entertainmentId} deleted successfully.");
                PopulateActivityFilter();
                BindGrid();
            }
        }

        // ---------- Add / Edit form ----------

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            litFormTitle.Text = "New Entertainment Provider";
            hfEntertainmentId.Value = "";
            txtEntertainmentIdDisplay.Text = GenerateNextId();
            txtEmployeeIdDisplay.Text = Session["StaffId"].ToString();
            pnlForm.Visible = true;
        }

        private void LoadRecordIntoForm(string entertainmentId)
        {
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand("SELECT EntertainmentID, Activity, Food, Location, EmployeeID FROM EntertainmentProvider WHERE EntertainmentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", entertainmentId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hfEntertainmentId.Value = reader["EntertainmentID"].ToString();
                        txtEntertainmentIdDisplay.Text = reader["EntertainmentID"].ToString();
                        txtActivity.Text = reader["Activity"].ToString();
                        txtFood.Text = reader["Food"].ToString();
                        txtLocation.Text = reader["Location"].ToString();
                        txtEmployeeIdDisplay.Text = reader["EmployeeID"] == DBNull.Value ? "" : reader["EmployeeID"].ToString();
                    }
                }
            }
            litFormTitle.Text = "Edit Entertainment Provider";
            pnlForm.Visible = true;
        }

        private string GenerateNextId()
        {
            int next = 1;
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand("SELECT EntertainmentID FROM EntertainmentProvider ORDER BY EntertainmentID DESC LIMIT 1", conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastId = result.ToString().Replace("ENT-", "");
                    if (int.TryParse(lastId, out int lastNum)) next = lastNum + 1;
                }
            }
            return "ENT-" + next.ToString("D3");
        }

        protected void btnCancelForm_Click(object sender, EventArgs e)
        {
            pnlForm.Visible = false;
            ClearForm();
        }

        private void ClearForm()
        {
            txtActivity.Text = "";
            txtFood.Text = "";
            txtLocation.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Required-field validation
            var missing = new System.Collections.Generic.List<string>();
            if (string.IsNullOrWhiteSpace(txtActivity.Text)) missing.Add("Activity");
            if (string.IsNullOrWhiteSpace(txtFood.Text)) missing.Add("Types of food");
            if (string.IsNullOrWhiteSpace(txtLocation.Text)) missing.Add("Location");

            if (missing.Count > 0)
            {
                ShowAlert("Please complete all fields before saving: " + string.Join(", ", missing) + ".");
                pnlForm.Visible = true;
                return;
            }

            bool isEdit = !string.IsNullOrEmpty(hfEntertainmentId.Value);
            string entertainmentId = isEdit ? hfEntertainmentId.Value : txtEntertainmentIdDisplay.Text;
            string activity = txtActivity.Text.Trim();
            string food = txtFood.Text.Trim();
            string location = txtLocation.Text.Trim();
            string employeeId = txtEmployeeIdDisplay.Text;

            // Duplicate detection: same activity + location on a different record
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM EntertainmentProvider WHERE Activity = @activity AND Location = @location AND EntertainmentID <> @id", conn))
            {
                cmd.Parameters.AddWithValue("@activity", activity);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@id", entertainmentId);
                conn.Open();
                long count = (long)cmd.ExecuteScalar();
                if (count > 0)
                {
                    ShowAlert("An entertainment provider with the same activity and location already exists. Please check before saving.");
                    pnlForm.Visible = true;
                    return;
                }
            }

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd;
                if (isEdit)
                {
                    cmd = new MySqlCommand(
                        "UPDATE EntertainmentProvider SET Activity = @activity, Food = @food, Location = @location WHERE EntertainmentID = @id", conn);
                }
                else
                {
                    cmd = new MySqlCommand(
                        "INSERT INTO EntertainmentProvider (EntertainmentID, Activity, Food, Location, EmployeeID) VALUES (@id, @activity, @food, @location, @empId)", conn);
                    cmd.Parameters.AddWithValue("@empId", employeeId);
                }
                cmd.Parameters.AddWithValue("@id", entertainmentId);
                cmd.Parameters.AddWithValue("@activity", activity);
                cmd.Parameters.AddWithValue("@food", food);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            ShowSuccess(isEdit
                ? $"Record {entertainmentId} updated successfully."
                : $"Record {entertainmentId} added successfully.");

            pnlForm.Visible = false;
            ClearForm();
            PopulateActivityFilter();
            BindGrid();
        }

        private void ShowAlert(string message)
        {
            lblAlert.Text = message;
            pnlAlert.Visible = true;
            pnlSuccess.Visible = false;
        }

        private void ShowSuccess(string message)
        {
            lblSuccess.Text = message;
            pnlSuccess.Visible = true;
            pnlAlert.Visible = false;
        }
    }
}
