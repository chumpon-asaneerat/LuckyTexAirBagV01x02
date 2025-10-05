#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using NLib;
using NLib.Logs;

#endregion

namespace InspectionPLCSim
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
                    ProductName = "LuckyTex Inspection PLC Simulator",
                    /* For Application Version */
                    Version = "1.0",
                    Minor = "0",
                    Build = "99",
                    LastUpdate = new DateTime(2014, 12, 17, 09, 25, 0)
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
                    IsSingleAppInstance = true,
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
                //NLib.Data.ODPConfig.Prepare();
                //NLib.Data.FirebirdConfig.Prepare();
            });

            #endregion

            Form form = new Form1();

            // Start log manager
            LogManager.Instance.Start();

            if (null != form)
            {
                WinAppContoller.Instance.Run(form);
            }

            // Shutdown log manager
            LogManager.Instance.Shutdown();

            WinAppContoller.Instance.Shutdown(true);
        }
    }
}
