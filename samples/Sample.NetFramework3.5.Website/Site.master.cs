using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Session.Clear();
            if (Page.Title != "Home" && Page.Title != "About Us")
            {
                Response.Redirect("./");
            }
        }

        //
        // Use this, so the Back button of browser does not return to a logged in page.
        //
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Page.Response.Cache.SetNoStore();
    }

    public string writeMenu()
    {
        string s = "";
        string page_title = Page.Header.Title;

        string current = ((page_title == "Home") ? " class='current'" : "");
        s += "<li" + current + "><a href='Default.aspx'>Home</a></li>\r\n";

        current = ((page_title == "About Us") ? " class='current'" : "");
        s += "<li" + current + "><a href='About.aspx'>About Us</a></li>\r\n";

        //if (isset($_SESSION['role']) && $_SESSION['role'] == "admin") {
        if (Session["role"] != null && Session["role"].ToString() == "admin")
        {
            if (page_title == "Site Admin")
            {
                s += "<li class='current'><a href='#'>Site Admin</a>\r\n";
            }
            else
            {
                current = "";
                if (page_title.StartsWith("Site Admin")) current = " class='current'";
                s += "<li" + current + "><a href='adminhome.aspx'>Site Admin</a>\r\n";
            }
            s += "<ul>\r\n";
            s += "<li><a href='admin_users.aspx'>Manage Users</a></li>\r\n";
            //$s .= "<li><a href='admin_images.php'>Manage Images</a></li>";
            s += "<li><a href='#' onclick='javascript: open_file(\"admin_images.php\");'>Manage Images</a></li>\r\n";
            s += "<li><a href='admin_create_schema.aspx'>Create Schema For Tables</a></li>\r\n";
            s += "<li><a href='admin_dump_table.aspx'>Dump Contents Of Tables</a></li>\r\n";
            s += "<li><a href='admin_backup_db.aspx'>Backup Database</a></li>\r\n";
            s += "</ul>\r\n";
            s += "</li>\r\n";
        }

        if (Session["username"] != null && Session["username"].ToString() != "")
        {
            if (page_title == "Member Home")
            {
                s += "<li class='current'><a href='home.aspx'>Member Home</a></li>\r\n";
            }
            else
            {
                s += "<li><a href='home.aspx'>Member Home</a></li>\r\n";
            }

            if (page_title == "My Profile")
            {
                s += "<li class='current'><a href='profile.aspx'>My Profile</a></li>\r\n";
            }
            else
            {
                s += "<li><a href='profile.aspx'>My Profile</a></li>\r\n";
            }

            string t = "<a href='logout.aspx'>Log out</a>";
            s += "<li>" + t + "</li>\r\n";
        }

        s = "<ul id='nav'>\r\n" + s + "</ul>\r\n";
        return s;
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
}
