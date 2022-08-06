6/20/2013

- create App_Data/makedb.sql

- recovery modle of a database: http://msdn.microsoft.com/en-us/library/ms189272.aspx
A recovery model is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available. Three recovery models exist: simple, full, and bulk-logged. Typically, a database uses the full recovery model or simple recovery model. 

- Close connection to a database (so you can drop it).
  http://stackoverflow.com/questions/11620/how-do-you-kill-all-current-connections-to-a-sql-server-2005-database

- ErrorLevel: 
  http://www.robvanderwoude.com/errorlevel.php
  IF ERRORLEVEL construction has one strange feature, that can be used to our advantage: it returns TRUE if the return code was equal to or higher than the specified errorlevel.

- Raiserror:
  http://msdn.microsoft.com/en-us/library/ms178592.aspx

6/13/2013

- Use master page:
  http://www.mini.pw.edu.pl/~mossakow/materials/presentations/aspnet.3.5/master_pages_site_navigation/
- header/footer:
  1) use master page. This is preferred over using user control for less redundant code. -> Default.aspx
  2) use user control, like in CasJobs. -> Default3.aspx
  3) use include file like in ASP, PHP.
  4) hard code.
- For public page that does not appear in menu, use a different Site.public.master, 
  in this master file, do not check valid session and redirect (like the private pages do). 
  
- Use of built in sitemap:
  -> Default2.aspx
- Login control:
  http://www.c-sharpcorner.com/uploadfile/raj1979/login-control-in-Asp-Net-3-5/
- To logout with no cache (back button of browser does not work), use this in master page Page_Load():
    Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Page.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
    Page.Response.Cache.SetNoStore();
  See: http://stackoverflow.com/questions/10542558/no-caching-and-logout-in-c-sharp-asp-net-3-5

- Align div to bottom of page and screen - whichever comes first
  http://stackoverflow.com/questions/6447793/html-css-align-div-to-bottom-of-page-and-screen-whichever-comes-first

- hr: noshade, size
  See: http://www.w3schools.com/tags/att_hr_noshade.asp


6/12/2013

- .NET hosting:
winhost.com. $4.95/month.

- header/footer:
  method 1: master page.
  method 2: web control: 
  - at top of page: <%@ Register TagPrefix="uc2" TagName="Header1" Src="top.ascx" %>
  - after <body> tag: <uc2:Header1 ID="Header1" runat="server" page_title="About Us" />
   Here page_titile is declared as a property in class top.ascx.cs:
     private string _page_title;
     public string page_title { get { return _page_title; } set { _page_title = value; } }


- A site framework should at least include:
  - header/footer (done)
  - login/logout  (done)
  - new user reigster
  - retrieve lost password
  - user management (add/remove/update/disable/enable)

  - use captcha
  - use flash logo

  Other things to consider:
  - about page.
  - e-commerce page.
  - info management.
  - bbs
  - blog
  - upload
  - search
  - calendar
  - export to word/excel/access
  - login from facebook etc.
  - follow with facebook, twitter, linked etc.
  - use ajax/jQuery features
  - sort table
