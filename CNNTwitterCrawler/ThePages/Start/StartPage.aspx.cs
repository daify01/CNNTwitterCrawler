using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNNTwitterCrawler.ThePages.Start
{
    public partial class StartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void getCNNUpdates_OnServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ThePages/Start/GetCNNUpdates.aspx");
        }

        protected void getTwitterUpdates_OnServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ThePages/Start/GetTwitterUpdates.aspx");
        }
    }
}