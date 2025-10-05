using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NLib;

namespace TestModbusSlave
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region Create Application Environment Options

            EnvironmentOptions option = new EnvironmentOptions()
            {
                /* Setup Application Information */
                AppInfo = new NAppInformation()
                {
                    /*  This property is required */
                    CompanyName = "LuckyTex",
                    /*  This property is required */
                    ProductName = "Modbus TCP Slave",
                    /* For Application Version */
                    Version = "2.0",
                    Minor = "1",
                    Build = "99",
                    LastUpdate = new DateTime(2015, 09, 09, 21, 48, 0)
                },
                /* Setup Storage */
                Storage = new NAppStorage()
                {
                    StorageType = NAppFolder.Custom,
                    /*  This property should set only when StorageType is Custom */
                    CustomFolder = @"C:\INC\AIRBAG\"
                },
                /* Setup Behaviors */
                Behaviors = new NAppBehaviors()
                {
                    /* Set to true for allow only one instance of application can execute an runtime */
                    IsSingleAppInstance = false,
                    /* Set to true for enable Debuggers this value should always be true */
                    EnableDebuggers = true
                }
            };

            #endregion

            #region Setup Option to Controller and check instance

            WinAppContoller.Instance.Setup(option);

            if (option.Behaviors.IsSingleAppInstance &&
                WinAppContoller.Instance.HasMoreInstance)
            {
                return;
            }

            #endregion

            #region Init Preload classes

            ApplicationManager.Instance.Preload(() =>
            {
            });

            #endregion

            Form form = null;
            //form = new Form1();
            form = new Form2();
            if (null != form)
            {
                WinAppContoller.Instance.Run(form);
            }

            WinAppContoller.Instance.Shutdown(true, 500);
        }
    }
}
