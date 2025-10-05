#region Using

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

#endregion

#region Extra Using

using NLib;
using NLib.Logs;


using LuckyTex.Configs;
using LuckyTex.Pages;
using LuckyTex.Services;
#endregion

namespace LuckyTex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region OnStartup
        
        /// OnStartup.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            #region Check Current Domain and Thread's CurrentContext
            
            Console.WriteLine("OnStartUp");
            if (null != AppDomain.CurrentDomain)
            {
                if (null != System.Threading.Thread.CurrentContext)
                {
                    Console.WriteLine("Thread CurrentContext is not null.");
                }

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            }

            #endregion

            #region Create Application Environment Options (For update version, last update)

            EnvironmentOptions option = new EnvironmentOptions()
            {
                /* Setup Application Information */
                AppInfo = new NAppInformation()
                {
                    /*  This property is required */
                    CompanyName = "LuckyTex",
                    /*  This property is required */
                    ProductName = "Lab Transfer Data",
                    /* For Application Version */
                    Version = "1.0",
                    Minor = "0",
                    Build = "042",
                    LastUpdate = new DateTime(2024, 01, 26,02,00, 0)
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

            WpfAppContoller.Instance.Setup(option);

            if (option.Behaviors.IsSingleAppInstance &&
                WpfAppContoller.Instance.HasMoreInstance)
            {
                return;
            }

            #endregion

            #region Init Preload classes

            ApplicationManager.Instance.Preload(() =>
            {
            });

            #endregion

            #region Init and Show Main UI
            
            Window window = null;
            window = new MainWindow();

            // Start log manager
            LogManager.Instance.Start();

            if (null != window)
            {
                WpfAppContoller.Instance.Run(window);
            }

            #endregion
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (null != e.ExceptionObject)
                MessageBox.Show(e.ExceptionObject.ToString());
            else MessageBox.Show("Detected Unhandled Exeption but no Exception OBject."); 
        }

        #endregion

        #region OnExit
        
        /// <summary>
        /// OnExit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Wpf shutdown process required exit code.
            WpfAppContoller.Instance.Shutdown(true, e.ApplicationExitCode);
            
            // Shutdown log manager
            LogManager.Instance.Shutdown();

            base.OnExit(e);
        }

        #endregion
    }
}
