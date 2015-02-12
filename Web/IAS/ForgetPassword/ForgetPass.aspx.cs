using System;
using System.Collections.Generic;
using System.Linq;


namespace IAS.ForgetPassword
{
    public partial class ForgetPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtConfirmEmail.Text))
            {
                BLL.PersonBiz biz = new BLL.PersonBiz();

                var res = biz.ForgetPasswordRequest(txtUserName.Text, txtConfirmEmail.Text);
                if (res.IsError)
                {
                    UCModalPopupError.ShowMessageError = res.ErrorMsg;

                    UCModalPopupError.ShowModalError();
                    UpdatePanelForgetPassword.Update();
                    return;
                }
                else
                {
                    txtConfirmEmail.Text = "";
                    txtUserName.Text = "";
                    UCModalPopupSuccess.ShowMessageSuccess = SysMessage.ConfirmChangePasswordMail;
                    UCModalPopupSuccess.ShowModalSuccess();
                    UpdatePanelForgetPassword.Update();
                    return;
                }
                
            }
            else
            {
                UCModalPopupError.ShowMessageError = SysMessage.PleaseInputFill;
                UCModalPopupError.ShowModalError();
                UpdatePanelForgetPassword.Update();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }

        //private void SendEmail()
        //{
        //    Email es = new Email();
        //    string lstRecipients = ConfigurationManager.AppSettings["RECIPIENTS_MAIL"];
        //    string[] recipients = lstRecipients.Split(new char[] { ',' });

        //    //HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(ConfigurationManager.AppSettings["EMAIL_CONTENTFILE_CONTACTUS_ADMIN"].ToString());
        //    //myWebRequest.Method = "GET";
        //    //// make request for web page
        //    //HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
        //    //StreamReader sr = new StreamReader(myWebResponse.GetResponseStream());
        //    //string mailContent = sr.ReadToEnd();

        //    //mailContent = mailContent.Replace("[name]", "Fuse");
        //    ////mailContent = mailContent.Replace("[country]", countryDropDownList.SelectedItem.Text);
        //    ////mailContent = mailContent.Replace("[email]", txtemail.Text.Trim());
        //    ////mailContent = mailContent.Replace("[message]", txtmessage.Text.Trim());
        //    ////mailContent = mailContent.Replace("[phone]", txtphone.Text.Trim());
        //    //mailContent = mailContent.Replace("[sendername]", txtConfirmEmail.Text.Trim());
        //    //mailContent = mailContent.Replace("[time]", DateTime.Now.ToString());


        //    // Send emails
        //    if (false == es.SendIndividualEmailsPerRecipient(recipients, ConfigurationManager.AppSettings["EMAIL_SUBJECT_CONTACTUS"].ToString(), "test"))
        //    {
        //        Console.WriteLine("Err:" + es.LastError);
        //        return;
        //    }
        //}

        //private bool SendMail(string toList, string from, string ccList, string subject, string body)
        //{
        //    bool isSendComplete = false;
        //    //get config
        //    string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
        //    string smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
        //    string smtpUserName = ConfigurationManager.AppSettings["UserNameMail"].ToString();
        //    string smtpPassword = ConfigurationManager.AppSettings["PasswordMail"].ToString();
        //    bool smtpEnable = true;

        //    if (smtpEnable)
        //    {
        //        MailMessage message = new MailMessage();
        //        SmtpClient smtpClient = new SmtpClient();
        //        try
        //        {
        //            MailAddress fromAddress = new MailAddress(from);
        //            message.SubjectEncoding = Encoding.UTF8;
        //            message.From = fromAddress;
        //            message.To.Add(toList);
        //            if (ccList != null && ccList != string.Empty)
        //                message.CC.Add(ccList);

        //            message.Subject = subject;
        //            message.IsBodyHtml = true;
        //            message.Body = body;
        //            smtpClient.Host = smtpServer;
        //            smtpClient.Port = Convert.ToInt32(smtpPort);
        //            smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SMTPEnableSSL"].ToString());
        //            smtpClient.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["SMTPUseDefaultCredentials"].ToString()); ;
        //            smtpClient.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
        //            smtpClient.Send(message);
        //            isSendComplete = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }

        //    return isSendComplete;
        //}
    }
}