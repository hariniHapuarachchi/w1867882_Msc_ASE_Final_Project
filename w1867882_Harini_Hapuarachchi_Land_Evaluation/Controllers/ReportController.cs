using GemBox.Document;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Views.VidewModel;
using Microsoft.AspNetCore.Hosting;
using System.Web;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        string[] teaArr = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.EvaluateController.myTeaArray;
        string[] rubberArr = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.EvaluateRubberController.myRubberArray;
        string[] coconutArr = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.EvaluateCoconutController.myCoconutArray;
        public ReportController(ApplicationContext db, IWebHostEnvironment env)
        {
            _context = db;
            _hostingEnv = env;
        }

        public IActionResult Index(string[] TrcArr)
        {
            ViewBag.FinalResult = TrcArr;
            return View();
        }

        public FileResult GenerateReport()
        {
            MemoryStream memoryStream = new MemoryStream();
            System.Text.StringBuilder buildingStatus = new System.Text.StringBuilder("");
            DateTime dateTime = DateTime.Now;
            //name of the file    
            string pdfFileName = string.Format("ReportPdf" + dateTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document document = new Document();
            document.SetMargins(0, 0, 0, 0);
            PdfPTable pdfTableLayout;
            //Create PDF Table with multiple columns based on crop details
            if (teaArr.Length != 0)
            {
                pdfTableLayout = new PdfPTable(5);
            }
            else if (rubberArr.Length != 0)
            {
                pdfTableLayout = new PdfPTable(5);
            }
            else
            {
                pdfTableLayout = new PdfPTable(5);
            }
            document.SetMargins(0, 0, 0, 0); 

            //file will download for this path
            var webRootPath = _hostingEnv.WebRootPath;
            string strAttachment = System.IO.Path.Combine(webRootPath, pdfFileName);


            PdfWriter.GetInstance(document, memoryStream).CloseStream = false;
            document.Open();

            // Add details to pdf based on crop
            if (teaArr.Length != 0) { 
                document.Add(Convert_Tea_To_PDF(pdfTableLayout));
            }else if (rubberArr.Length != 0)
            {
                document.Add(Convert_Rubber_To_PDF(pdfTableLayout));
            }
            else
            {
                document.Add(Convert_Coconut_To_PDF(pdfTableLayout));
            }
  
                document.Close();

            byte[] byteInfo = memoryStream.ToArray();
            memoryStream.Write(byteInfo, 0, byteInfo.Length);
            memoryStream.Position = 0;


            return File(memoryStream, "application/pdf", pdfFileName);

        }

        protected PdfPTable Convert_Tea_To_PDF(PdfPTable pdfTableLayout)
        {
            float[] headerWidth = { 50, 24, 45, 35, 50 };
            pdfTableLayout.SetWidths(headerWidth);
            pdfTableLayout.WidthPercentage = 100;
            pdfTableLayout.HeaderRows = 3;

            pdfTableLayout.AddCell(new PdfPCell(new Phrase("Generated Report of Land Suitability Evaluation", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
            });

            //Add header names to cells
            AddHeaderCell(pdfTableLayout, "Land Id");
            AddHeaderCell(pdfTableLayout, "User Name");
            AddHeaderCell(pdfTableLayout, "Location");
            AddHeaderCell(pdfTableLayout, "Growing Period");
            AddHeaderCell(pdfTableLayout, "Mean Annual RF");
            AddHeaderCell(pdfTableLayout, "Soil Depth");
            AddHeaderCell(pdfTableLayout, "Soil Drainage Class");
            AddHeaderCell(pdfTableLayout, "Soil Ph");
            AddHeaderCell(pdfTableLayout, "Rock Outcrops");
            AddHeaderCell(pdfTableLayout, "Elevation");
            AddHeaderCell(pdfTableLayout, "Soil Texture");
            AddHeaderCell(pdfTableLayout, "Stones And Grovels");
            AddHeaderCell(pdfTableLayout, "Slope Angle");
            AddHeaderCell(pdfTableLayout, "Past Erosion");
            AddHeaderCell(pdfTableLayout, "Class of Land Unit");

            AddBodyCell(pdfTableLayout, teaArr[0]);
            AddBodyCell(pdfTableLayout, teaArr[2]);
            AddBodyCell(pdfTableLayout, teaArr[3]);
            AddBodyCell(pdfTableLayout, teaArr[4]);
            AddBodyCell(pdfTableLayout, teaArr[5]);
            AddBodyCell(pdfTableLayout, teaArr[6]);
            AddBodyCell(pdfTableLayout, teaArr[7]);
            AddBodyCell(pdfTableLayout, teaArr[8]);
            AddBodyCell(pdfTableLayout, teaArr[9]);

            if (teaArr[11].ToString() != null)
            {
                AddBodyCell(pdfTableLayout, teaArr[11]);
            }else if (teaArr[12].ToString() != null)
            {
                AddBodyCell(pdfTableLayout, teaArr[12]);
            }
            else
            {
                AddBodyCell(pdfTableLayout, teaArr[13]);
            }
            AddBodyCell(pdfTableLayout, teaArr[14]);
            AddBodyCell(pdfTableLayout, teaArr[15]);
            AddBodyCell(pdfTableLayout, teaArr[16]);
            AddBodyCell(pdfTableLayout, teaArr[17]);
            AddBodyCell(pdfTableLayout, teaArr[teaArr.Length - 2]);

            return pdfTableLayout;
        }

        protected PdfPTable Convert_Rubber_To_PDF(PdfPTable pdfTableLayout)
        {

            float[] headerWidth = { 50, 24, 45, 35, 50 };
            pdfTableLayout.SetWidths(headerWidth);
            pdfTableLayout.WidthPercentage = 100;
            pdfTableLayout.HeaderRows = 3;

            pdfTableLayout.AddCell(new PdfPCell(new Phrase("Generated Report of Land Suitability Evaluation", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
            });

            //Add header names to cell
            AddHeaderCell(pdfTableLayout, "Land Id");
            AddHeaderCell(pdfTableLayout, "User Name");
            AddHeaderCell(pdfTableLayout, "Location");
            AddHeaderCell(pdfTableLayout, "Growing Period");
            AddHeaderCell(pdfTableLayout, "Mean Annual RF");
            AddHeaderCell(pdfTableLayout, "Soil Depth");
            AddHeaderCell(pdfTableLayout, "Soil Drainage Class");
            AddHeaderCell(pdfTableLayout, "Soil Ph");
            AddHeaderCell(pdfTableLayout, "Rock Outcrops");
            AddHeaderCell(pdfTableLayout, "Elevation");
            AddHeaderCell(pdfTableLayout, "Mean Anual Temperature");
            AddHeaderCell(pdfTableLayout, "Class of Land Unit");
            AddHeaderCell(pdfTableLayout, "Date");
            AddHeaderCell(pdfTableLayout, "-");
            AddHeaderCell(pdfTableLayout, "-");

            AddBodyCell(pdfTableLayout, rubberArr[0]);
            AddBodyCell(pdfTableLayout, rubberArr[2]);
            AddBodyCell(pdfTableLayout, rubberArr[3]);
            AddBodyCell(pdfTableLayout, rubberArr[4]);
            AddBodyCell(pdfTableLayout, rubberArr[5]);
            AddBodyCell(pdfTableLayout, rubberArr[6]);
            AddBodyCell(pdfTableLayout, rubberArr[7]);
            AddBodyCell(pdfTableLayout, rubberArr[8]);
            AddBodyCell(pdfTableLayout, rubberArr[9]);
            AddBodyCell(pdfTableLayout, rubberArr[11]);
            AddBodyCell(pdfTableLayout, rubberArr[12]);
            AddBodyCell(pdfTableLayout, rubberArr[rubberArr.Length - 2]);
            AddBodyCell(pdfTableLayout, DateTime.Now.ToString());
            AddBodyCell(pdfTableLayout, "-");
            AddBodyCell(pdfTableLayout, "-");
            return pdfTableLayout;
        }

        protected PdfPTable Convert_Coconut_To_PDF(PdfPTable pdfTableLayout)
        {

            float[] headerWidth = { 50, 24, 45, 35, 50 };
            pdfTableLayout.SetWidths(headerWidth);
            pdfTableLayout.WidthPercentage = 100;
            pdfTableLayout.HeaderRows = 4;

            pdfTableLayout.AddCell(new PdfPCell(new Phrase("Generated Report of Land Suitability Evaluation", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
            });

            //Add header names to cells 
            AddHeaderCell(pdfTableLayout, "Land Id");
            AddHeaderCell(pdfTableLayout, "User Name");
            AddHeaderCell(pdfTableLayout, "Location");
            AddHeaderCell(pdfTableLayout, "Growing Period");
            AddHeaderCell(pdfTableLayout, "Mean Annual RF");
            AddHeaderCell(pdfTableLayout, "Soil Depth");
            AddHeaderCell(pdfTableLayout, "Soil Drainage Class");
            AddHeaderCell(pdfTableLayout, "Soil Ph");
            AddHeaderCell(pdfTableLayout, "Rock Outcrops");
            AddHeaderCell(pdfTableLayout, "Elevation");
            AddHeaderCell(pdfTableLayout, "Mean Anual Temperature");
            AddHeaderCell(pdfTableLayout, "Total Sunshine");
            AddHeaderCell(pdfTableLayout, "Minimum Humidity");
            AddHeaderCell(pdfTableLayout, "Soil Texture");
            AddHeaderCell(pdfTableLayout, "Water Depth");
            AddHeaderCell(pdfTableLayout, "EC");
            AddHeaderCell(pdfTableLayout, "Slope Angle");
            AddHeaderCell(pdfTableLayout, "Class of Land Unit");
            AddHeaderCell(pdfTableLayout, "Date");
            AddHeaderCell(pdfTableLayout, "-");

            AddBodyCell(pdfTableLayout, coconutArr[0]);
            AddBodyCell(pdfTableLayout, coconutArr[2]);
            AddBodyCell(pdfTableLayout, coconutArr[3]);
            AddBodyCell(pdfTableLayout, coconutArr[4]);
            AddBodyCell(pdfTableLayout, coconutArr[5]);
            AddBodyCell(pdfTableLayout, coconutArr[6]);
            AddBodyCell(pdfTableLayout, coconutArr[7]);
            AddBodyCell(pdfTableLayout, coconutArr[8]);
            AddBodyCell(pdfTableLayout, coconutArr[9]);
            AddBodyCell(pdfTableLayout, coconutArr[11]);
 
            AddBodyCell(pdfTableLayout, coconutArr[12]);
            AddBodyCell(pdfTableLayout, coconutArr[13]);
            AddBodyCell(pdfTableLayout, coconutArr[14]);
            AddBodyCell(pdfTableLayout, coconutArr[15]);
            AddBodyCell(pdfTableLayout, coconutArr[16]);
            AddBodyCell(pdfTableLayout, coconutArr[17]);
            AddBodyCell(pdfTableLayout, coconutArr[18]);
            AddBodyCell(pdfTableLayout, coconutArr[coconutArr.Length - 2]);
            AddBodyCell(pdfTableLayout, DateTime.Now.ToString());
            AddBodyCell(pdfTableLayout, "-");

            return pdfTableLayout;
        }
        // Add each cell to head 
        private static void AddHeaderCell(PdfPTable pdfTableLayout, string cellName)
        {

            pdfTableLayout.AddCell(new PdfPCell(new Phrase(cellName, new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(255, 255, 0))))
            {
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
            });
        }

        // Add values of header cells to body  
        private static void AddBodyCell(PdfPTable pdfTableLayout, string cellName)
        {
            pdfTableLayout.AddCell(new PdfPCell(new Phrase(cellName, new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }
    }
}
