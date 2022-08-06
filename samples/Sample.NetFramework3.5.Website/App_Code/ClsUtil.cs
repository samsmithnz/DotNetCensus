using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ClsUtil
/// </summary>
public class ClsUtil
{
    private bool _DEBUG = false;
    private string _strConn;
    private static ClsUtil _SiteUtil = null;

    private ClsUtil()
    {
        _DEBUG = StrToBool(ConfigurationSettings.AppSettings["DEBUG"]);
        _strConn = ConfigurationManager.ConnectionStrings["connCI35"].ConnectionString;
    }
    
    /// <summary>
    /// Singleton. Return only one instance of ClsUtil for the site.
    /// </summary>
    public static ClsUtil Instance() {
        if (_SiteUtil == null) {
            _SiteUtil = new ClsUtil();
        }
        return _SiteUtil;
    }

    /// <summary>
    /// Site wide variables, initialized once at start up.
    /// </summary>

    public bool DEBUG() {
        return _DEBUG;
    }

    public string strConn() {
        return _strConn;
    }

    public string getScalar(string strQuery) {
        string ret = "";
        string strConn = this.strConn(); 
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();
            if (sdr.Read()) {
                ret = sdr[0].ToString();
            }
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
        finally {
            if (sdr != null) sdr.Close();
            conn.Close();
        }

        return ret;
    }

    /// <summary>
    /// Static utility functions.
    /// </summary>

    public static bool StrToBool(string v) {
        return v.ToLower() == "true";
    }
}
