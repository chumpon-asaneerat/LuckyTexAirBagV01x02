#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using NLib;
using NLib.Data;
using NLib.Components;
using Newtonsoft.Json;

#endregion

namespace LuckyTex.ClassData
{
    using System.Net;
    using System.Net.Mail;

    #region Configs

    #region SendMailConfig

    /// <summary>
    /// The SMTPServerConfig class.
    /// </summary>
    public class SMTPServerConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SMTPServerConfig()
            : base()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets SMTP Host Name or IP.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets SMTP Port No.
        /// </summary>
        public int PortNo { get; set; }

        /// <summary>
        /// Gets or set enable credential;
        /// </summary>
        public bool EnableCredential { get; set; }
        /// <summary>
        /// Gets or sets SMTP user name (i.e. email address).
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or set SMTP password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets Use SSL.
        /// </summary>
        public bool UseSSL { get; set; }

        #endregion
    }

    #endregion

    #region MailAddressConfig

    /// <summary>
    /// The MailAddressConfig class.
    /// </summary>
    public class MailAddressConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MailAddressConfig()
            : base()
        {
            Recipients = new List<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets mail sender;
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Gets or sets list of recipient.
        /// </summary>
        public List<string> Recipients { get; set; }

        #endregion
    }

    #endregion

    #region SendMailConfig

    /// <summary>
    /// The SendMailConfig class.
    /// </summary>
    public class SendMailConfig
    {
        #region Constructor

        public SendMailConfig()
            : base()
        {
            Server = new SMTPServerConfig();
            Address = new MailAddressConfig();
        }

        #endregion

        #region Public Properties

        public SMTPServerConfig Server { get; set; }
        public MailAddressConfig Address { get; set; }

        #endregion
    }

    #endregion

    #endregion

    #region ConfigSendMail
    public class ConfigSendMail
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConfigSendMail()
            : base()
        {
        }

        #endregion

        #region ConfigInfo
        public class ConfigInfo
        {
            public string HostName { get; set; }
            public int PortNo { get; set; }
            public bool EnableCredential { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool UseSSL { get; set; }
            public string Sender { get; set; }
            public string To { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }
        #endregion

        #region SaveConfig
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HostName"></param>
        /// <param name="PortNo"></param>
        /// <param name="EnableCredential"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="UseSSL"></param>
        /// <param name="Sender"></param>
        /// <param name="To"></param>
        /// <param name="Title"></param>
        /// <param name="Body"></param>
        /// <returns></returns>
        public static bool SaveConfig()
        {

            try
            {
                string hostName = @"10.251.208.31";
                int portNo = 25;
                bool enableCredential = false;
                string userName = string.Empty;
                string password = string.Empty;
                bool useSSL = false;
                string sender = "QAAirbagM3@mail.toray";
                string to = string.Empty;
                string title = string.Empty;
                string body = string.Empty;

                ConfigInfo config = new ConfigInfo
                {
                    HostName = @hostName,
                    PortNo = portNo,
                    EnableCredential = enableCredential,
                    UserName = @userName,
                    Password = @password,
                    UseSSL = useSSL,
                    Sender = @sender,
                    To = @to,
                    Title = @title,
                    Body = @body
                };

                try
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\\configSendMail.json"))
                    {
                        FileInfo fileCheck = new FileInfo(Directory.GetCurrentDirectory() + @"\\configSendMail.json");
                        fileCheck.Delete();
                    }
                }
                catch (Exception ex)
                {
                    ex.Err();
                }

                string json = JsonConvert.SerializeObject(config, Formatting.Indented);

                string path = Directory.GetCurrentDirectory() + @"\\configSendMail.json";
                //export data to json file. 
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(json);
                };

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region LoadConfig
        public static void LoadConfig()
        {
            try
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\\configSendMail.json"))
                {
                    SaveConfig();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region SendSMTPMail
        public static bool SendSMTPMail()
        {
            try
            {
                try
                {
                    if (!File.Exists(Directory.GetCurrentDirectory() + @"\\configSendMail.json"))
                    {
                        SaveConfig();
                    }
                }
                catch (Exception ex)
                {
                    ex.Err();
                    return false;
                }

                if (File.Exists(Directory.GetCurrentDirectory() + @"\\configSendMail.json"))
                {
                    using (StreamReader file = File.OpenText(Directory.GetCurrentDirectory() + @"\\configSendMail.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        ConfigInfo config = (ConfigInfo)serializer.Deserialize(file, typeof(ConfigInfo));


                        SendMailConfig cfg = new SendMailConfig();
                        // SMTP Server
                        try
                        {
                            cfg.Server.HostName = config.HostName.Trim();
                            cfg.Server.PortNo = config.PortNo;
                            cfg.Server.EnableCredential = config.EnableCredential;
                            cfg.Server.UserName = config.UserName;
                            cfg.Server.Password = config.Password;
                            cfg.Server.UseSSL = config.UseSSL;
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString().Err();
                            return false;
                        }

                        // Mail Adddress
                        try
                        {
                            cfg.Address.Sender = config.Sender.Trim();
                            string[] targets = config.To.Split(new char[] { ',' },
                                StringSplitOptions.RemoveEmptyEntries);
                            cfg.Address.Recipients.AddRange(targets);
                            //cfg.Address.Recipients.Add("vorakanok@yahoo.com");
                            //string recipients = txtRecipients.Text;
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString().Err();
                            return false;
                        }

                        // Mail Message
                        string title = config.Title;
                        string body = config.Body;

                        try
                        {
                            SMTPMailSender.Send(cfg, title, body);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString().Err();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion
    }
    #endregion

    #region SMTPMailSender
    public class SMTPMailSender
    {
        public static void Send(SendMailConfig cfg, string title, string body)
        {
            if (null == cfg)
                return;

            // Set protocol.            
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            //System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)0;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3 | (SecurityProtocolType)0 | SecurityProtocolType.Tls | (SecurityProtocolType)768
                | (SecurityProtocolType)3072 | (SecurityProtocolType)12288;
            //Ssl3	48	
            //SystemDefault	0	
            //Tls	192	
            //Tls11	768	
            //Tls12	3072	
            //Tls13	12288	

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(cfg.Address.Sender);
                foreach (string target in cfg.Address.Recipients)
                {
                    message.To.Add(new MailAddress(target));
                }

                message.IsBodyHtml = true; // to make message body as html

                message.SubjectEncoding = Encoding.UTF8;
                message.Subject = title;

                message.BodyEncoding = Encoding.UTF8;
                message.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = cfg.Server.HostName;

                smtp.Port = cfg.Server.PortNo;
                smtp.EnableSsl = cfg.Server.UseSSL;

                if (cfg.Server.EnableCredential)
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(cfg.Server.UserName, cfg.Server.Password);
                }
                else
                {
                    smtp.UseDefaultCredentials = true;
                }
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                smtp.Dispose();
                smtp = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    #endregion
}
