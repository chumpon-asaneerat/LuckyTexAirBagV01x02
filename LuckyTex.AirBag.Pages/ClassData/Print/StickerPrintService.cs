#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using NLib;
using NLib.Xml;

using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Printing;

#endregion

namespace LuckyTex.Services
{
    #region Configs classes
    
    /// <summary>
    /// The Sticker Printer class.
    /// </summary>
    [Serializable]
    public class StickerPrinter
    {
        #region Construcor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public StickerPrinter() : base() 
        {
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets printer name.
        /// </summary>
        [XmlAttribute]
        public string PrinterName { get; set; }

        #endregion
    }

    /// <summary>
    /// Sticker Printer Config class.
    /// </summary>
    [Serializable]
    public class StickerPrinterConfig
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public StickerPrinterConfig() : base()
        {
            this.Printer1 = new StickerPrinter();
            this.Printer2 = new StickerPrinter();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets printer 1.
        /// </summary>
        public StickerPrinter Printer1 { get; set; }
        /// <summary>
        /// Gets or sets printer 2.
        /// </summary>
        public StickerPrinter Printer2 { get; set; }

        #endregion
    }

    #endregion

    #region LocalReportPageSettings

    /// <summary>
    /// LocalReport Page Settings class.
    /// </summary>
    public class LocalReportPageSettings
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalReportPageSettings()
            : base()
        {
            // Letter size.
            this.PageWidth = 8.5;
            this.PageHeight = 11;

            this.MarginLeft = 0;
            this.MarginTop = 0;
            this.MarginRight = 0;
            this.MarginBottom = 0;
            this.PageLandscape = false;

            //เพิ่มใหม่
            //--------------------------------------------//
            
            this.RawKind = 9;
            this.PaperName = "A4";
            this.Width = 827;
            this.Height = 1169;

            //--------------------------------------------//
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets Page Width in inch.
        /// </summary>
        public double PageWidth { get; set; }
        /// <summary>
        /// Gets or sets Page Height in inch.
        /// </summary>
        public double PageHeight { get; set; }

        /// <summary>
        /// Gets or sets Page Margin Left in inch.
        /// </summary>
        public double MarginLeft { get; set; }
        /// <summary>
        /// Gets or sets Page Margin Top in inch.
        /// </summary>
        public double MarginTop { get; set; }
        /// <summary>
        /// Gets or sets Page Margin Right in inch.
        /// </summary>
        public double MarginRight { get; set; }
        /// <summary>
        /// Gets or sets Page Margin Bottom in inch.
        /// </summary>
        public double MarginBottom { get; set; }

        public bool PageLandscape { get; set; }

        //เพิ่มใหม่
        //--------------------------------------------//
        public int RawKind { get; set; }
        public string PaperName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //--------------------------------------------//

        #endregion
    }

    #endregion

    #region LocalReportRenderer

    /// <summary>
    /// LocalReport Renderer class. 
    /// See https://msdn.microsoft.com/en-us/library/ms252091(v=VS.80).aspx
    /// for more information.
    /// </summary>
    public class LocalReportRenderer : IDisposable
    {
        #region Internal Variables
        
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Routine to provide to the report renderer, in order to save an image for each page of the report.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileNameExtension"></param>
        /// <param name="encoding"></param>
        /// <param name="mimeType"></param>
        /// <param name="willSeek"></param>
        /// <returns></returns>
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            //string pathName = @"..\..\";
            string pathName = Path.GetTempPath();
            string fileName = name + "." + fileNameExtension;
            string printFileName = Path.GetFullPath(Path.Combine(pathName, fileName));
            Stream stream = new FileStream(printFileName, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        /// <summary>
        /// Free all print image (EMF) streams.
        /// </summary>
        private void FreeStreams()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        /// <summary>
        /// Export the given report as an EMF (Enhanced Metafile) file.
        /// </summary>
        /// <param name="report">The local report instance.</param>
        /// <param name="pageSettings">The page setting.</param>
        /// <returns>Returns true if success.</returns>
        private bool Export(LocalReport report, LocalReportPageSettings pageSettings)
        {
            string deviceInfo = string.Empty;
            LocalReportPageSettings pgSettings = pageSettings;
            if (null == pgSettings)
            {
                pgSettings = new LocalReportPageSettings();
            }
            string deviceInfofmt = 
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>{0}in</PageWidth>" +
              "  <PageHeight>{1}in</PageHeight>" +
              "  <MarginTop>{2}in</MarginTop>" +
              "  <MarginLeft>{3}in</MarginLeft>" +
              "  <MarginRight>{4}in</MarginRight>" +
              "  <MarginBottom>{5}in</MarginBottom>" +
              "</DeviceInfo>";
            deviceInfo = string.Format(deviceInfofmt, 
                pgSettings.PageWidth,
                pgSettings.PageHeight,
                pgSettings.MarginTop,
                pgSettings.MarginLeft,
                pgSettings.MarginRight,
                pgSettings.MarginBottom
            );
            try
            {
                FreeStreams(); // Free exist image streams.
                Warning[] warnings;
                m_streams = new List<Stream>();
                report.Render("Image", deviceInfo, CreateStream, out warnings);
                foreach (Stream stream in m_streams)
                    stream.Position = 0;
            }
            catch (Exception ex)
            {
                ex.Err();
                FreeStreams();
            }

            return true;
        }

        /// <summary>
        /// Handler for PrintPageEvents.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        void printDoc_EndPrint(object sender, PrintEventArgs e)
        {
            FreeStreams();
        }

        /// <summary>
        /// Print.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public bool Print(LocalReport report, string printerName,
            LocalReportPageSettings pageSettings)
        {
            bool result = false;
            if (!Export(report, pageSettings))
            {
                return result;
            }
            
            string sPrinterName = printerName;
            //const string printerName = "Microsoft Office Document Image Writer";
            if (m_streams == null || m_streams.Count == 0)
                return result;

            m_currentPageIndex = 0; // Reset print index.
            PrintDocument printDoc = new PrintDocument();
            if (!string.IsNullOrWhiteSpace(sPrinterName))
            {
                printDoc.PrinterSettings.PrinterName = sPrinterName;
            }
            else 
            {
                // It's should used default printer.
            }

            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format(
                   "Can't find printer \"{0}\".", printerName);
                msg.Err();
                return result;
            }

            //printDoc.DefaultPageSettings.Landscape = pageSettings.PageLandscape;

            //เพิ่มใหม่
            //---------------------------------------------------------------------------------//

            printDoc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(pageSettings.PaperName, pageSettings.Width, pageSettings.Height);
            printDoc.DefaultPageSettings.PaperSize.RawKind = pageSettings.RawKind;
            printDoc.DefaultPageSettings.Landscape = pageSettings.PageLandscape;

            //---------------------------------------------------------------------------------//

            // Hook handlers
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.EndPrint += new PrintEventHandler(printDoc_EndPrint);
            try
            {
                printDoc.Print();
                // print OK.
                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            FreeStreams();
        }

        #endregion
    }

    #endregion

    #region StickerPrintService

    /// <summary>
    /// Sticker Print Service.
    /// </summary>
    public class StickerPrintService
    {
        #region Singelton

        private static StickerPrintService _instance = null;
        /// <summary>
        /// Singelton Access instance.
        /// </summary>
        public static StickerPrintService Instance
        {
            get 
            {
                if (null == _instance)
                {
                    lock (typeof(StickerPrintService))
                    {
                        _instance = new StickerPrintService();
                    }
                }
                return _instance;  
            }
        }

        #endregion

        #region Internal Variables

        private StickerPrinterConfig _config = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private StickerPrintService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~StickerPrintService()
        {

        }

        #endregion

        #region Private Methods

        private void InitConfig()
        {
            _config = new StickerPrinterConfig();
            // get default printer name.
            string defaultPrinterName = LocalPrintServer.GetDefaultPrintQueue().FullName;
            // set printer 1
            if (null == _config.Printer1)
                _config.Printer1 = new StickerPrinter();
            _config.Printer1.PrinterName = defaultPrinterName;
            // set printer 2
            if (null == _config.Printer2)
                _config.Printer2 = new StickerPrinter();
            _config.Printer2.PrinterName = defaultPrinterName;
        }

        private void LoadConfig()
        {
            if (!File.Exists(this.ConfigFileName))
            {
                // Auto save if not exists.
                if (null == _config)
                {
                    InitConfig(); // Create new if required.
                }
                // No file created so create new one
                SaveConfig();
            }
            StickerPrinterConfig cfg = 
                XmlManager.LoadFromFile<StickerPrinterConfig>(this.ConfigFileName);
            if (null != cfg)
            {
                _config = cfg;
            }
            else
            {
                if (null == _config)
                {
                    InitConfig(); // Create new if required.
                }
                // No need to create file here but if required un-comment below code.
                //SaveConfig();
            }
        }

        private void SaveConfig()
        {
            if (null == _config)
                return;
            bool result = XmlManager
                .SaveToFile<StickerPrinterConfig>(this.ConfigFileName, _config);
            if (!result)
            {
                "Cannot save sticker config.".Err();
            }
        }

        #endregion

        #region Public Methods

        public void Print(LocalReport value, string printerName, 
            LocalReportPageSettings pageSettings)
        {
            if (null == value)
            {
                "LocalReport is null.".Info();
                return;
            }
            LocalReportRenderer prt = new LocalReportRenderer();
            prt.Print(value, printerName, pageSettings);
            prt.Dispose();
            prt = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Config File Name.
        /// </summary>
        public string ConfigFileName
        {
            get
            {
                string configPath =
                    ApplicationManager.Instance.Environments.Company.Configs.FullName;
                return Path.Combine(configPath, "StickerPrinterConfig.xml");
            }
        }
        /// <summary>
        /// Gets Config.
        /// </summary>
        public StickerPrinterConfig Config
        {
            get
            {
                if (null == _config)
                {
                    LoadConfig();
                }
                return _config;
            }
        }

        #endregion
    }

    #endregion
}
