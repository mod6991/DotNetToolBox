using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Text;

namespace DotNetToolBox.Reporting
{
    public class Rdlc14Manager
    {
        private ReportViewer _report;
        private List<ReportParameter> _parameters;
        private List<ReportDataSource> _dataSources;
        private string _printerName;
        private bool _isLandscape;
        private PageSettings _pageSettings;
        private List<Stream> _printStreams;
        private int _currentPageIndex;

        #region Constructor

        public Rdlc14Manager(string reportPath)
        {
            _report = new ReportViewer();
            _report.LocalReport.ReportPath = reportPath;
            _parameters = new List<ReportParameter>();
            _dataSources = new List<ReportDataSource>();
            _pageSettings = new PageSettings();
            _printStreams = new List<Stream>();
        }

        #endregion

        #region Properties

        public ReportViewer Report
        {
            get { return _report; }
        }

        public List<ReportParameter> Parameters
        {
            get { return _parameters; }
        }

        public List<ReportDataSource> DataSources
        {
            get { return _dataSources; }
        }

        public string PrinterName
        {
            get { return _printerName; }
            set { _printerName = value; }
        }

        public bool IsLandscape
        {
            get { return _isLandscape; }
            set { _isLandscape = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the parameters and data sources before printing/saving the report
        /// </summary>
        private void SetParametersAndDataSources()
        {
            if (_parameters.Count > 0)
                Report.LocalReport.SetParameters(_parameters);

            Report.LocalReport.DataSources.Clear();

            foreach (ReportDataSource source in _dataSources)
                Report.LocalReport.DataSources.Add(source);
        }

        /// <summary>
        /// Add a new parameter to the report
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public void AddParameter(string name, string value)
        {
            _parameters.Add(new ReportParameter(name, value));
        }

        /// <summary>
        /// Save the report as a PDF file
        /// </summary>
        /// <param name="file">PDF output file</param>
        public void SavePdfReport(string file)
        {
            SetParametersAndDataSources();

            byte[] pdfData = Report.LocalReport.Render("PDF", null);
            File.WriteAllBytes(file, pdfData);
        }

        /// <summary>
        /// Print the report
        /// </summary>
        /// <param name="copies"></param>
        public Warning[] PrintReport(short copies)
        {
            PrintDocument printDoc;
            Warning[] warnings;
            _currentPageIndex = 0;
            _printStreams.Clear();

            //Sets the parameters and data sources
            SetParametersAndDataSources();

            using (printDoc = new PrintDocument())
            {
                //Sets the printer to use
                if (!string.IsNullOrWhiteSpace(PrinterName))
                {
                    _pageSettings.PrinterSettings.PrinterName = _printerName;
                    printDoc.DefaultPageSettings.PrinterSettings.PrinterName = _printerName;
                }

                //Gets the report settings
                GetReportPrintSettings(ref printDoc);
                //Create a new deviceInfo
                string deviceInfo = CreateEMFDeviceInfo();
                //Render the report
                Report.LocalReport.Render("Image", deviceInfo, CreateStream, out warnings);

                if (_printStreams.Count > 0)
                    foreach (Stream s in _printStreams)
                        s.Position = 0;

                printDoc.PrinterSettings.Copies = copies;
                printDoc.DefaultPageSettings.Landscape = _isLandscape;
                printDoc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.Landscape = _isLandscape;

                printDoc.PrintPage += new PrintPageEventHandler(PrintDoc_PrintPage);

                printDoc.Print();
            }

            return warnings;
        }

        /// <summary>
        /// Get the report settings
        /// </summary>
        /// <param name="printDoc">PrintDocument object</param>
        private void GetReportPrintSettings(ref PrintDocument printDoc)
        {
            ReportPageSettings rps = Report.LocalReport.GetDefaultPageSettings();

            _pageSettings.PaperSize = rps.PaperSize;
            _pageSettings.Margins = rps.Margins;

            printDoc.DefaultPageSettings.Margins = rps.Margins;
            printDoc.DefaultPageSettings.PaperSize = rps.PaperSize;
        }

        /// <summary>
        /// Generate the EMF device info from page settings
        /// </summary>
        private string CreateEMFDeviceInfo()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "<DeviceInfo><OutputFormat>emf</OutputFormat><StartPage>0</StartPage><EndPage>0</EndPage><MarginTop>{0}</MarginTop><MarginLeft>{1}</MarginLeft><MarginRight>{2}</MarginRight><MarginBottom>{3}</MarginBottom><PageHeight>{4}</PageHeight><PageWidth>{5}</PageWidth></DeviceInfo>",
                ToInches(_pageSettings.Margins.Top),
                ToInches(_pageSettings.Margins.Left),
                ToInches(_pageSettings.Margins.Right),
                ToInches(_pageSettings.Margins.Bottom),
                ToInches(_pageSettings.PaperSize.Height),
                ToInches(_pageSettings.PaperSize.Width));
        }

        private static string ToInches(int hundrethsOfInch)
        {
            double inches = hundrethsOfInch / 100.0;
            return inches.ToString(CultureInfo.InvariantCulture) + "in";
        }

        /// <summary>
        /// Callback used by the image render
        /// </summary>
        private Stream CreateStream(string name, string extension, Encoding encoding, string mimeType, bool willSeek)
        {
            MemoryStream stream = new MemoryStream();
            _printStreams.Add(stream);
            return stream;
        }

        /// <summary>
        /// PrintPage event handler
        /// </summary>
        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Stream pageToPrint = _printStreams[_currentPageIndex];
            pageToPrint.Position = 0;

            //Load each page into a Metafile to draw it
            using (Metafile pageMetaFile = new Metafile(pageToPrint))
            {
                Rectangle adjustedRect = new Rectangle(
                        ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                        ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                        ev.PageBounds.Width,
                        ev.PageBounds.Height);

                //Draw a white background for the report
                ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

                //Draw the report content
                ev.Graphics.DrawImage(pageMetaFile, adjustedRect);

                //Prepare for next page.  Make sure we haven't hit the end.
                _currentPageIndex++;
                ev.HasMorePages = _currentPageIndex < _printStreams.Count;
            }
        }

        #endregion
    }
}
