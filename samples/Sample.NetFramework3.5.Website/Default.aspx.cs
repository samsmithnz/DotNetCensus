using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null && Session["username"].ToString() != "") {
            btnLogin.Visible = false;
        }
    }

    protected void btnLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (this.doLogin(btnLogin.UserName.ToString().Trim(), btnLogin.Password.ToString().Trim())) {
            e.Authenticated = true;
            btnLogin.Visible = false;
            Label1.Text = "Successfully Logged In.";
            Label1.ForeColor = System.Drawing.Color.Green;
            Response.Redirect("home.aspx");
        }
        else {
            e.Authenticated = false;
            btnLogin.FailureText = ""; // Hide default message display (inside the login box, looks not nice).

            Label1.Text = "<p>Your login attemp was not successful. Please try again.</p>";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }

    /// <summary>
    /// Use data reader, read the first row. Don't check whether there are extra rows.
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    private bool doLogin(string UserName, string Password)
    {
        string strQuery = "SELECT login, gid, email FROM [User] WHERE login='" + UserName +
            "' AND passwd=HASHBYTES('MD5', '" + Password + "')";

        string strConn = ClsUtil.Instance().strConn(); // ConfigurationManager.ConnectionStrings["connCI35"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);

        if (ClsUtil.Instance().DEBUG()) { Response.Write("query: " + strQuery); }

        try
        {
            conn.Open();
            using (SqlDataReader sdr = comm.ExecuteReader()) {
                if (sdr.Read())
                {
                    Session["username"] = sdr[0].ToString();
                    Session["role"] = getRole(sdr[1].ToString());
                    Session["email"] = sdr[2].ToString();
                    return true;
                }            
            }
        }
        catch (Exception ex)
        {
            if (ClsUtil.Instance().DEBUG())
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        finally
        {
            conn.Close();
        }

        return false;
    }

    /// <summary>
    /// Use data table to guarantee there is only one returned row.
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    private bool doLogin_v1(string UserName, string Password) {
        string strQuery = "SELECT login, gid, email FROM [User] WHERE login='" + UserName +
            "' AND passwd=HASHBYTES('MD5', '" + Password + "')";

        string strConn = ClsUtil.Instance().strConn(); // ConfigurationManager.ConnectionStrings["connCI35"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        if (ClsUtil.Instance().DEBUG()) { Response.Write("query: " + strQuery ); }

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();

            if (sdr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                if (dt.Rows.Count == 1)
                {
                    Session["username"] = dt.Rows[0][0].ToString();
                    Session["role"] = getRole(dt.Rows[0][1].ToString());
                    Session["email"] = dt.Rows[0][2].ToString();
                    return true;
                }
            }
        }
        catch (Exception ex) {
            if (ClsUtil.Instance().DEBUG())
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        finally {
            if (sdr != null) sdr.Close();
            conn.Close(); 
        }

        return false;
    }

    // For initial test only. Static username/password.
    private bool doLogin_v0(string UserName, string Password)
    {
        if (UserName.ToLower() == "admin" && Password.ToLower() == "password")
        {
            Session["username"] = UserName;
            Session["role"] = "admin";
            return true;
        }
        else
        {
            return false;
        }
    }

    private string getRole(string roleID) {
        return ClsUtil.Instance().getScalar("SELECT [name] FROM UserGroup WHERE ID = '" + roleID + "'");
    }
}
