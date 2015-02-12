using System;
using System.Web;
using IAS.BLL;
using IAS.Common.Configuration;
using IAS.Common.Email;
using IAS.Common.Logging;
using IAS.Common.Validator;
using StructureMap;
using System.Threading;


namespace IAS
{
    public class Global : HttpApplication
    {


        PersonBiz biz = new PersonBiz();
        public DateTime OldDate=DateTime.Now;
        void Application_Start(object sender, EventArgs e)
        {
           // OfflineAll();
            
            BootStrapper.ConfigureDependencies();
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory
                    (ObjectFactory.GetInstance<IApplicationSettings>());
            LoggerFactory.SetCurrent(new Log4NetServiceLogFactory());
            EmailServiceFactory.InitializeEmailServiceFactory
                            (ObjectFactory.GetInstance<IEmailService>());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            LoggerFactory.CreateLog().LogInfo("Web Soria Started" + DateTime.Now.Minute);

            Thread thread = new Thread(new ThreadStart(ThreadFunc));
            thread.IsBackground = true;
            thread.Name = "ThreadFunc";
            thread.Start();
        }
        protected void ThreadFunc()
        {
           
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimerWorker);
            if (DateTime.Now.Hour == 23)// %3==0)//10000 = 1 s. // 600000 = 1 min. // 36000000 = 1 hr.
                t.Interval =  6000000;//10min.
            else
                t.Interval = 36000000;//1hr.
            t.Enabled = true;
            t.AutoReset = true;
            t.Start();
        }

        protected void TimerWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (true)
            {
                var biz = new BLL.PaymentBiz();
                DateTime GetDate=Convert.ToDateTime(biz.GetLastEndDate().DataResponse);
                if(GetDate.Date!=OldDate.Date)
                {
                    LoggerFactory.CreateLog().LogInfo("     start Global    " + DateTime.Now);
                    var res = biz.Auto_CancelAppNoPay(GetDate, DateTime.Now);
                    if (res.ResultMessage == true)
                    {
                        LoggerFactory.CreateLog().LogInfo("     end  Global  " + DateTime.Now );
                        OldDate = GetDate;
                    }
                    else
                        LoggerFactory.CreateLog().LogInfo("     error Global   " + DateTime.Now);
                }
            
            }
        }
                

        void Application_End(object sender, EventArgs e)
        {
           // OfflineAll();
        }

        void Application_Error(object sender, EventArgs e)
        {
           
        }

        protected void Session_Start(object src, EventArgs e)
        {
            
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //Offline();
        }
        private void OfflineAll()
        {
            try
            {
                biz.SetOffLineAllStatus("");
            }
            catch { }
        }
        private void Offline()
        {
            try
            {
                if (Session["LoginUser"] != null)
                {
                    string username = Session["LoginUser"].ToString();
                    if (username.Length > 0)
                    {
                        biz.SetOffLineStatus(username);
                    }
                }
            }
            catch { }
        }


    }
}
