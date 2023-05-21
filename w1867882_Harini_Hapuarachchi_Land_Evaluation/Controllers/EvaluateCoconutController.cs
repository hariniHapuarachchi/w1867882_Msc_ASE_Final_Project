using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Python.Runtime;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Views.VidewModel;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
	public class EvaluateCoconutController : Controller
	{
        private readonly ApplicationContext _context;
        public static string UsersName = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.MenuCropController.UsersName;
        public static string[] myCoconutArray = { };
        public IActionResult Index()
        {
            return View();
        }

        public EvaluateCoconutController(ApplicationContext db)
        {
            _context = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoconutLandView obj, int evaluation)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Users.Add(obj);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(obj);
            //var TeaCrop = PredictTea(obj);

            obj.CoconutLandModel.Evaluation = evaluation;
            obj.LandModel.LandId = GenerateKey();
            obj.LandModel.UserId = GetUserId(UsersName);
            _context.Lands.Add(obj.LandModel);
            _context.SaveChanges();
            obj.CoconutLandModel.LandId = obj.LandModel.LandId;
            _context.CoconutLands.Add(obj.CoconutLandModel);
            _context.SaveChanges();
            WriteDataToExcel(obj);
            string classofLand = RunPythonTeaCodeAndReturn();
            _context.Evaluations.Add(new Evaluation { LandId = obj.LandModel.LandId, Prediction = classofLand });
            _context.SaveChanges();

            List<string> myList = new List<string> { obj.LandModel.LandId.ToString(), obj.LandModel.UserId.ToString(),UsersName,
                                             obj.LandModel.Location, obj.LandModel.Days.ToString(),obj.LandModel.MeanAnualRF.ToString(),
                                             obj.LandModel.SoilDepth.ToString(),obj.LandModel.SoilDrainageClass,obj.LandModel.SoilPH.ToString(),
                                             obj.LandModel.RockOutcrops.ToString(), obj.CoconutLandModel.CoconutId.ToString(),obj.CoconutLandModel.Evaluation.ToString(),
                                             obj.CoconutLandModel.MeanAnualTemp.ToString(),obj.CoconutLandModel.TotalSunshine.ToString(),obj.CoconutLandModel.MinimumHumidity.ToString(),
                                             obj.CoconutLandModel.SoilTexture.ToString(),obj.CoconutLandModel.WaterDepth.ToString(),obj.CoconutLandModel.Ec.ToString(),
                                             obj.CoconutLandModel.SlopeAngle.ToString(), classofLand, "Coconut"
                                           };
            myCoconutArray = myList.ToArray();
            return RedirectToAction("Index", "Report", new { TrcArr = myCoconutArray });
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
            string returnedVariableName = "coconut_output";
            object returnedVariable = "";
            Initialize();

            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = Py.CreateScope();
            scope.Set(returnedVariableName, returnedVariable);
            scope.Exec(System.IO.File.ReadAllText(@"C:\Users\harin\OneDrive\Documents\IIT\ResearchProject\w1867882_Harini_Hapuarachchi_Land_Evaluation\MLModel\Land Evaluation of Coconut.py"));

            var myclass = scope.Get("coconut_output");
            var predictedVal = myclass.GetItem(0).ToString();
            double classOfLand = Convert.ToDouble(predictedVal);

            if (classOfLand >= 0.00 && classOfLand < 2.00)
            {
                return "Higly Suitable (S1) - Coconut";
            }
            else if (classOfLand >= 2.00 && classOfLand < 3)
            {
                return "Moderately Suitable (S2) - Coconut";
            }
            else if (classOfLand >= 3 && classOfLand < 4)
            {
                return "Marginally Suitable (S3) - Coconut";
            }
            else
            {
                return "Not Suitable (S4) - Coconut";
            }

        }

        public static void Initialize()
        {
            string pythonDll = @"C:\Users\harin\AppData\Local\Programs\Python\Python311\python311.dll";
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDll);
            //string pythonCtype = @"C:\Users\harin\AppData\Local\Programs\Python\Python311\DLLs\_ctypes.pyd";
            //Environment.SetEnvironmentVariable("PYTHONNET_PYD", pythonCtype);
            //string pythonLightGbm = @"C:\Users\harin\.nuget\packages\lightgbm\3.3.5\";
            //Environment.SetEnvironmentVariable("PYTHONNET_LGB", pythonLightGbm);
            PythonEngine.Initialize();
        }

        public void WriteDataToExcel(CoconutLandView obj)
        {
            string[] dataArr = new string[25];

            dataArr[0] = obj.CoconutLandModel.Evaluation.ToString();
            dataArr[1] = obj.CoconutLandModel.MeanAnualTemp.ToString();
            dataArr[2] = obj.CoconutLandModel.TotalSunshine.ToString();
            dataArr[3] = obj.CoconutLandModel.MinimumHumidity.ToString();
            dataArr[4] = obj.LandModel.Days.ToString();
            dataArr[5] = obj.LandModel.MeanAnualRF.ToString();
            dataArr[6] = obj.LandModel.SoilDepth.ToString();

            if (obj.CoconutLandModel.SoilTexture == "C")
            {
                dataArr[7] = "1";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "CL")
            {
                dataArr[7] = "0";
                dataArr[8] = "1";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "L")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "1";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "SC")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "1";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "SCL")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "1";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "SL")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "1";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "ZC")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "1";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "ZCL")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "1";
                dataArr[15] = "0";
                dataArr[16] = "0";
            }
            else if (obj.CoconutLandModel.SoilTexture == "ZL")
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "1";
                dataArr[16] = "0";
            }
            else
            {
                dataArr[7] = "0";
                dataArr[8] = "0";
                dataArr[9] = "0";
                dataArr[10] = "0";
                dataArr[11] = "0";
                dataArr[12] = "0";
                dataArr[13] = "0";
                dataArr[14] = "0";
                dataArr[15] = "0";
                dataArr[16] = "1";
            }

            if (obj.LandModel.SoilDrainageClass == "ID")
            {
                dataArr[17] = "1";
                dataArr[18] = "0";
                dataArr[19] = "0";

            }
            else if (obj.LandModel.SoilDrainageClass == "MWD")
            {
                dataArr[17] = "0";
                dataArr[18] = "1";
                dataArr[19] = "0";
            }
            else
            {
                dataArr[17] = "0";
                dataArr[18] = "0";
                dataArr[19] = "1";
            }

            dataArr[20] = obj.CoconutLandModel.WaterDepth.ToString();
            dataArr[21] = obj.LandModel.SoilPH.ToString();
            dataArr[22] = obj.CoconutLandModel.Ec.ToString();
            dataArr[23] = obj.CoconutLandModel.SlopeAngle.ToString();
            dataArr[24] = obj.LandModel.RockOutcrops.ToString();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage("C:\\Users\\harin\\OneDrive\\Documents\\IIT\\ResearchProject\\w1867882_Harini_Hapuarachchi_Land_Evaluation\\Dataset\\Land Eveluation DataCoconut Sample.xlsx"))
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
                package.Save();
            }
        }
    }
}
