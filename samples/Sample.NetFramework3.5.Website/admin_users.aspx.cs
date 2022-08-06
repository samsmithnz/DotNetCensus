using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        init();
    }

    private void init() {
        string sql = "SELECT * FROM [User]";
        SqlConnection conn = new SqlConnection( ClsUtil.Instance().strConn() );
        SqlCommand comm = new SqlCommand(sql, conn);

        try
        {
            conn.Open();
            using (SqlDataReader sdr = comm.ExecuteReader())
            {
                this.GridView1.DataSource = sdr.HasRows ? sdr : null;
                this.GridView1.DataBind();
            }
            conn.Close();
        }
        catch (Exception ex) {
            Response.Write("Error: " + ex.Message);
        }
    }
}
