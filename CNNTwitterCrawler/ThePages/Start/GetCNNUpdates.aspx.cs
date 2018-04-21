using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNNTwitterCrawler.ThePages.Start
{
    public partial class GetCNNUpdates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TextBoxNameSenderEmail.Value = string.Empty;
                //TextBoxNameRecipientEmail.Value = string.Empty;
                //TextBoxNameMailSubject.Value = string.Empty;
                //TextAreaMailBody.Value = string.Empty;
                //TextBoxNameSenderPassword.Value = string.Empty;
            }
        }

        protected void searchsubmit_OnServerClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (string.IsNullOrWhiteSpace(TextBoxNameSenderEmail.Value))
            //        throw new Exception("First Name field is required");
            //    if (string.IsNullOrWhiteSpace(TextBoxNameRecipientEmail.Value))
            //        throw new Exception("Last Name field is required");
            //    if (string.IsNullOrWhiteSpace(TextBoxNameMailSubject.Value))
            //        throw new Exception("Email field is required");
            //    if (string.IsNullOrWhiteSpace(TextAreaMailBody.Value))
            //        throw new Exception("User Name field is required");
            //    if (string.IsNullOrWhiteSpace(TextBoxNameSenderPassword.Value))
            //        throw new Exception("User Name field is required");

            //    string strFromEmail = TextBoxNameSenderEmail.Value.Trim();
            //    string strToEmail = TextBoxNameRecipientEmail.Value.Trim();
            //    string strSubject = TextBoxNameMailSubject.Value.Trim();
            //    string senderPassword = TextBoxNameSenderPassword.Value.Trim();
            //    string url = HttpContext.Current.Request.Url.AbsoluteUri;

            //    MailMessage MailMsg = new MailMessage();
            //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            //    MailMsg.From = new MailAddress(strFromEmail);
            //    MailMsg.Subject = strSubject;

            //    MailMsg.To.Add(strToEmail);
                
            //    MailMsg.Body = TextAreaMailBody.Value.Trim();
            //    MailMsg.Body += Environment.NewLine;
            //    MailMsg.Body +=  string.Format("url : {0}", url);


            //    smtp.Port = 587;
            //    smtp.Credentials = new System.Net.NetworkCredential(strFromEmail, senderPassword);
            //    smtp.EnableSsl = true;
            //    smtp.Send(MailMsg);

            //    TextBoxNameSenderEmail.Value = string.Empty;
            //    TextBoxNameRecipientEmail.Value = string.Empty;
            //    TextBoxNameMailSubject.Value = string.Empty;
            //    TextAreaMailBody.Value = string.Empty;
            //    TextBoxNameSenderPassword.Value = string.Empty;

            //    if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "message"))
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "<script type='text/javascript'>alertify.alert('Message', '" + "Email Sent Successfully" + "', function(){location = '/ThePages/Start/GetCNNUpdates.aspx';});</script>", false);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            //    if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "message"))
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "message", @"<script type='text/javascript'>alertify.alert('Message', """ + errorMessage.Replace("\n", "").Replace("\r", "") + @""", function(){});</script>", false);
            //    }
            //}
        }
    }
}