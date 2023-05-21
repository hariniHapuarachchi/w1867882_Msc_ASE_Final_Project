using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using Python.Runtime;
using System.Dynamic;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Views.VidewModel;
using static IronPython.Runtime.Profiler;
using System.Text.Json;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
    public class EvaluateController : Controller
    {
        private readonly ApplicationContext _context;
        public static string UsersName = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.MenuCropController.UsersName;
        public static string[] myTeaArray = { };
        public IActionResult Index()
        {
            UsersName = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.MenuCropController.UsersName;
            return View();
        }

        public EvaluateController(ApplicationContext db)
        {
            _context = db;
        }

        //public EvaluateController() { }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeaLandView obj, int evaluation)
        {
            if (obj.LandModel.Location == "Up")
            {
                obj.TeaLandModel.EvaluationUp = evaluation;
            }
            else if (obj.LandModel.Location == "Mis")
            {
                obj.TeaLandModel.EvaluationMis = evaluation;
            }
            else
            {
                obj.TeaLandModel.EvaluationLow = evaluation;
            }

            obj.LandModel.LandId = GenerateKey();
            obj.LandModel.UserId = GetUserId(UsersName);
            _context.Lands.Add(obj.LandModel);
            _context.SaveChanges();
            obj.TeaLandModel.LandId = obj.LandModel.LandId;
            _context.TeaLands.Add(obj.TeaLandModel);
            _context.SaveChanges();
            WriteDataToExcel(obj);
            string classofLand = RunPythonTeaCodeAndReturn();
            _context.Evaluations.Add(new Evaluation { LandId = obj.LandModel.LandId, Prediction = classofLand });
            _context.SaveChanges();

            List<string> myList = new List<string> { obj.LandModel.LandId.ToString(), obj.LandModel.UserId.ToString(),UsersName,
                                             obj.LandModel.Location, obj.LandModel.Days.ToString(),obj.LandModel.MeanAnualRF.ToString(),
                                             obj.LandModel.SoilDepth.ToString(),obj.LandModel.SoilDrainageClass,obj.LandModel.SoilPH.ToString(),
                                             obj.LandModel.RockOutcrops.ToString(), obj.TeaLandModel.TeaId.ToString(),obj.TeaLandModel.EvaluationUp.ToString(),
                                             obj.TeaLandModel.EvaluationMis.ToString(),obj.TeaLandModel.EvaluationLow.ToString(),obj.TeaLandModel.SoilTexture.ToString(),obj.TeaLandModel.StonesAndGrovels.ToString(),
                                             obj.TeaLandModel.SlopeAngle.ToString(), obj.TeaLandModel.PastErosion, classofLand, "Tea"
                                           };
            myTeaArray = myList.ToArray();
            return RedirectToAction("Index", "Report", new {TrcArr = myTeaArray });
        }

        public int GenerateKey()
        {
            int Id = 1;
            IEnumerable<Land> objLandList = _context.Lands;
            foreach (Land objLand in objLandList)
            {
                if (objLand.LandId == Id)
                {
                    Id = Id + 1;
                }
            }
            return Id;
        }

        public int GetUserId(string userName)
        {
            int userId = 0;
            IEnumerable<User> objUsersList = _context.Users;
            foreach (User objUser in objUsersList)
            {
                if (objUser.UserName == userName)
                {
                    userId = objUser.UserId;
                }
            }
            return userId;
        }

        public string RunPythonTeaCodeAndReturn()
        {
            string returnedVariableName = "tea_output";
            object returnedVariable = "";
            Initialize();

           // var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = Py.CreateScope();
            scope.Set(returnedVariableName, returnedVariable);
            scope.Exec(System.IO.File.ReadAllText(@"C:\Users\harin\OneDrive\Documents\IIT\ResearchProject\w1867882_Harini_Hapuarachchi_Land_Evaluation\MLModel\Land Evaluation with lightgbm.py"));

            var myclass = scope.Get("tea_output");
            var predictedVal = myclass.GetItem(0).ToString();
            double classOfLand = Convert.ToDouble(predictedVal);

            if (classOfLand >= 0.00 && classOfLand < 2.00)
            {
                return "Higly Suitable (S1) - Tea";
            }
            else if (classOfLand >= 2.00 && classOfLand < 3)
            {
                return "Moderately Suitable (S2) - Tea";
            }
            else if (classOfLand >= 3 && classOfLand < 4)
            {
                return "Marginally Suitable (S3) - Tea";
            }
            else
            {
                return "Not Suitable (S4) - Tea";
            }

        }

        public static void Initialize()
        {
            string pythonDll = @"C:\Users\harin\AppData\Local\Programs\Python\Python311\python311.dll";
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDll);
            PythonEngine.Initialize();
        }

        public void WriteDataToExcel(TeaLandView obj)
        {
            string[] dataArr = new string[26];
            if (obj.TeaLandModel.EvaluationUp != 0)
            {
                dataArr[0] = obj.TeaLandModel.EvaluationUp.ToString();
                dataArr[1] = "0";
                dataArr[2] = "0";
            }
            else if (obj.TeaLandModel.EvaluationMis != 0)
            {
                dataArr[0] = "0";
                dataArr[1] = obj.TeaLandModel.EvaluationUp.ToString();
                dataArr[2] = "0";
            }
            else if (obj.TeaLandModel.EvaluationLow != 0)
            {
                dataArr[0] = "0";
                dataArr[1] = "0";
                dataArr[2] = obj.TeaLandModel.EvaluationUp.ToString();
            }

            dataArr[3] = obj.LandModel.Days.ToString();
            dataArr[4] = obj.LandModel.MeanAnualRF.ToString();
            dataArr[5] = obj.LandModel.SoilDepth.ToString();

            if (obj.TeaLandModel.SoilTexture == "L")
            {
                dataArr[6] = "1";
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
            }
            else if (obj.TeaLandModel.SoilTexture == "S")
            {
                dataArr[6] = "0";
                dataArr[7] = "1";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
            }
            else if (obj.TeaLandModel.SoilTexture == "C")
            {
                dataArr[6] = "0";
                dataArr[7] = "0";
                dataArr[8] = "1";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
            }
            else if (obj.TeaLandModel.SoilTexture == "SL")
            {
                dataArr[6] = "0";
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "1";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
            }
            else if (obj.TeaLandModel.SoilTexture == "LC")
            {
                dataArr[6] = "0";
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "1";
                dataArr[11] = "0";
                dataArr[12] = "0";
            }
            else if (obj.TeaLandModel.SoilTexture == "SLC")
            {
                dataArr[6] = "0";
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "1";
                dataArr[12] = "0";
            }
            else
            {
                dataArr[6] = "0";
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "1";
            }

            dataArr[13] = obj.TeaLandModel.StonesAndGrovels.ToString();

            if (obj.LandModel.SoilDrainageClass == "WD")
            {
                dataArr[14] = "1";
                dataArr[15] = "0";
                dataArr[16] = "0";
                dataArr[17] = "0";
                dataArr[18] = "0";

            }
            else if (obj.LandModel.SoilDrainageClass == "MWD")
            {
                dataArr[14] = "0";
                dataArr[15] = "1";
                dataArr[16] = "0";
                dataArr[17] = "0";
                dataArr[18] = "0";
            }
            else if (obj.LandModel.SoilDrainageClass == "ED")
            {
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "1";
                dataArr[17] = "0";
                dataArr[18] = "0";
            }
            else if (obj.LandModel.SoilDrainageClass == "ID")
            {
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
                dataArr[17] = "1";
                dataArr[18] = "0";
            }
            else
            {
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
                dataArr[17] = "0";
                dataArr[18] = "1";
            }
            dataArr[19] = obj.LandModel.SoilPH.ToString();
            dataArr[20] = obj.TeaLandModel.SlopeAngle.ToString();

            if (obj.TeaLandModel.PastErosion == "nil")
            {
                dataArr[21] = "1";
                dataArr[22] = "0";
                dataArr[23] = "0";
                dataArr[24] = "0";
            }
            else if (obj.TeaLandModel.PastErosion == "moderate")
            {
                dataArr[21] = "0";
                dataArr[22] = "1";
                dataArr[23] = "0";
                dataArr[24] = "0";
            }
            else if (obj.TeaLandModel.PastErosion == "severe")
            {
                dataArr[21] = "0";
                dataArr[22] = "0";
                dataArr[23] = "1";
                dataArr[24] = "0";
            }
            else
            {
                dataArr[21] = "0";
                dataArr[22] = "0";
                dataArr[23] = "0";
                dataArr[24] = "1";
            }

            dataArr[25] = obj.LandModel.RockOutcrops.ToString();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage("C:\\Users\\harin\\OneDrive\\Documents\\IIT\\ResearchProject\\w1867882_Harini_Hapuarachchi_Land_Evaluation\\Dataset\\Land Eveluation DataTea Sample.xlsx"))
            {
                var sheet = package.Workbook.Worksheets.Last();
                sheet.Cells["A2"].Value = dataArr[0];
                sheet.Cells["B2"].Value = dataArr[1];
                sheet.Cells["C2"].Value = dataArr[2];
                sheet.Cells["D2"].Value = dataArr[3];
                sheet.Cells["E2"].Value = dataArr[4];
                sheet.Cells["F2"].Value = dataArr[5];
                sheet.Cells["G2"].Value = dataArr[6];
                sheet.Cells["H2"].Value = dataArr[7];
                sheet.Cells["I2"].Value = dataArr[8];
                sheet.Cells["J2"].Value = dataArr[9];
                sheet.Cells["K2"].Value = dataArr[10];
                sheet.Cells["L2"].Value = dataArr[11];
                sheet.Cells["M2"].Value = dataArr[12];
                sheet.Cells["N2"].Value = dataArr[13];
                sheet.Cells["O2"].Value = dataArr[14];
                sheet.Cells["P2"].Value = dataArr[15];
                sheet.Cells["Q2"].Value = dataArr[16];
                sheet.Cells["R2"].Value = dataArr[17];
                sheet.Cells["S2"].Value = dataArr[18];
                sheet.Cells["T2"].Value = dataArr[19];
                sheet.Cells["U2"].Value = dataArr[20];
                sheet.Cells["V2"].Value = dataArr[21];
                sheet.Cells["W2"].Value = dataArr[22];
                sheet.Cells["X2"].Value = dataArr[23];
                sheet.Cells["Y2"].Value = dataArr[24];
                sheet.Cells["Z2"].Value = dataArr[25];
                package.Save();
            }
        }
    }
}
