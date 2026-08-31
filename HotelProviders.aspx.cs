using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace EazzyTravels
{
    public partial class HotelProviders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffId"] == null) Session["StaffId"] = "EMP-001";
            if (Session["Role"] == null) Session["Role"] = "Employee";

            if (!IsPostBack)
            {
                txtStaffId.Text = Session["StaffId"].ToString();
                ddlRole.SelectedValue = Session["Role"].ToString();
                PopulateTypeFilter();
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

        private void PopulateTypeFilter()
        {
            ddlTypeFilter.Items.Clear();
            ddlTypeFilter.Items.Add(new System.Web.UI.WebControls.ListItem("All types", ""));
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand("SELECT DISTINCT HotelType FROM HotelProvider ORDER BY HotelType", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        ddlTypeFilter.Items.Add(new System.Web.UI.WebControls.ListItem(reader.GetString(0), reader.GetString(0)));
                }
            }
        }

        private void BindGrid()
        {
            string search = txtSearch.Text.Trim();
            string typeFilter = ddlTypeFilter.SelectedValue;

            var sql = new StringBuilder("SELECT HotelID, HotelType, Location, Rating, EmployeeID FROM HotelProvider WHERE 1=1");
            if (!string.IsNullOrEmpty(typeFilter)) sql.Append(" AND HotelType = @type");
            if (!string.IsNullOrEmpty(search)) sql.Append(" AND (HotelID LIKE @search OR HotelType LIKE @search OR Location LIKE @search)");
            sql.Append(" ORDER BY HotelID");

            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand(sql.ToString(), conn))
            {
                if (!string.IsNullOrEmpty(typeFilter)) cmd.Parameters.AddWithValue("@type", typeFilter);
                if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                conn.Open();
                var da = new MySqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                gvHotels.DataSource = dt;
                gvHotels.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlTypeFilter.SelectedValue = "";
            BindGrid();
        }

        // Hide the Delete link for non-admins, per the "Admin can delete,
        // staff can only view/edit" non-functional requirement.
        protected void gvHotels_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

        protected void gvHotels_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string hotelId = e.CommandArgument.ToString();

            if (e.CommandName == "EditRecord")
            {
                LoadRecordIntoForm(hotelId);
            }
            else if (e.CommandName == "DeleteRecord")
            {
                if (Session["Role"] == null || Session["Role"].ToString() != "Administrator")
                {
                    ShowAlert("Only administrators can delete records.");
                    return;
                }

                using (var conn = DbHelper.GetConnection())
                using (var cmd = new MySqlCommand("DELETE FROM HotelProvider WHERE HotelID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", hotelId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ShowSuccess($"Record {hotelId} deleted successfully.");
                PopulateTypeFilter();
                BindGrid();
            }
        }

        // ---------- Add / Edit form ----------

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            litFormTitle.Text = "New Hotel Provider";
            hfHotelId.Value = "";
            txtHotelIdDisplay.Text = GenerateNextId();
            txtEmployeeIdDisplay.Text = Session["StaffId"].ToString();
            pnlForm.Visible = true;
        }

        private void LoadRecordIntoForm(string hotelId)
        {
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand("SELECT HotelID, HotelType, Location, Rating, EmployeeID FROM HotelProvider WHERE HotelID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", hotelId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hfHotelId.Value = reader["HotelID"].ToString();
                        txtHotelIdDisplay.Text = reader["HotelID"].ToString();
                        ddlHotelType.SelectedValue = reader["HotelType"].ToString();
                        txtLocation.Text = reader["Location"].ToString();
                        ddlRating.SelectedValue = reader["Rating"].ToString();
                        txtEmployeeIdDisplay.Text = reader["EmployeeID"] == DBNull.Value ? "" : reader["EmployeeID"].ToString();
                    }
                }
            }
            litFormTitle.Text = "Edit Hotel Provider";
            pnlForm.Visible = true;
        }

        private string GenerateNextId()
        {
            int next = 1;
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand("SELECT HotelID FROM HotelProvider ORDER BY HotelID DESC LIMIT 1", conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastId = result.ToString().Replace("HTL-", "");
                    if (int.TryParse(lastId, out int lastNum)) next = lastNum + 1;
                }
            }
            return "HTL-" + next.ToString("D3");
        }

        protected void btnCancelForm_Click(object sender, EventArgs e)
        {
            pnlForm.Visible = false;
            ClearForm();
        }

        private void ClearForm()
        {
            ddlHotelType.SelectedValue = "";
            txtLocation.Text = "";
            ddlRating.SelectedValue = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Required-field validation
            var missing = new System.Collections.Generic.List<string>();
            if (string.IsNullOrEmpty(ddlHotelType.SelectedValue)) missing.Add("Type of hotel");
            if (string.IsNullOrWhiteSpace(txtLocation.Text)) missing.Add("Location");
            if (string.IsNullOrEmpty(ddlRating.SelectedValue)) missing.Add("Rating");

            if (missing.Count > 0)
            {
                ShowAlert("Please complete all fields before saving: " + string.Join(", ", missing) + ".");
                pnlForm.Visible = true;
                return;
            }

            bool isEdit = !string.IsNullOrEmpty(hfHotelId.Value);
            string hotelId = isEdit ? hfHotelId.Value : txtHotelIdDisplay.Text;
            string hotelType = ddlHotelType.SelectedValue;
            string location = txtLocation.Text.Trim();
            int rating = int.Parse(ddlRating.SelectedValue);
            string employeeId = txtEmployeeIdDisplay.Text;

            // Duplicate detection: same type + location on a different record
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM HotelProvider WHERE HotelType = @type AND Location = @location AND HotelID <> @id", conn))
            {
                cmd.Parameters.AddWithValue("@type", hotelType);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@id", hotelId);
                conn.Open();
                long count = (long)cmd.ExecuteScalar();
                if (count > 0)
                {
                    ShowAlert("A hotel provider with the same type and location already exists. Please check before saving.");
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
                        "UPDATE HotelProvider SET HotelType = @type, Location = @location, Rating = @rating WHERE HotelID = @id", conn);
                }
                else
                {
                    cmd = new MySqlCommand(
                        "INSERT INTO HotelProvider (HotelID, HotelType, Location, Rating, EmployeeID) VALUES (@id, @type, @location, @rating, @empId)", conn);
                    cmd.Parameters.AddWithValue("@empId", employeeId);
                }
                cmd.Parameters.AddWithValue("@id", hotelId);
                cmd.Parameters.AddWithValue("@type", hotelType);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            ShowSuccess(isEdit
                ? $"Record {hotelId} updated successfully."
                : $"Record {hotelId} added successfully.");

            pnlForm.Visible = false;
            ClearForm();
            PopulateTypeFilter();
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
