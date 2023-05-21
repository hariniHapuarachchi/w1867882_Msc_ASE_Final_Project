using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Python.Runtime;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Views.VidewModel;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
	public class EvaluateRubberController : Controller
	{
        private readonly ApplicationContext _context;
        public static string UsersName = w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.MenuCropController.UsersName;
        public static string[] myRubberArray = { };
        public IActionResult Index()
		{
			return View();
		}

        public EvaluateRubberController(ApplicationContext db)
        {
            _context = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RubberLandView obj, int evaluation)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Users.Add(obj);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(obj);
            //var TeaCrop = PredictTea(obj);
            obj.RubberLandModel.Evaluation = evaluation;
            obj.LandModel.LandId = GenerateKey();
            obj.LandModel.UserId = GetUserId(UsersName);
            _context.Lands.Add(obj.LandModel);
            _context.SaveChanges();
            obj.RubberLandModel.LandId = obj.LandModel.LandId;
            _context.RubberLands.Add(obj.RubberLandModel);
            _context.SaveChanges();
            WriteDataToExcel(obj);
            string classofLand = RunPythonTeaCodeAndReturn();
            _context.Evaluations.Add(new Evaluation { LandId = obj.LandModel.LandId, Prediction = classofLand });
            _context.SaveChanges();

            List<string> myList = new List<string> { obj.LandModel.LandId.ToString(), obj.LandModel.UserId.ToString(),UsersName,
                                             obj.LandModel.Location, obj.LandModel.Days.ToString(),obj.LandModel.MeanAnualRF.ToString(),
                                             obj.LandModel.SoilDepth.ToString(),obj.LandModel.SoilDrainageClass,obj.LandModel.SoilPH.ToString(),
                                             obj.LandModel.RockOutcrops.ToString(), obj.RubberLandModel.RubberId.ToString(),obj.RubberLandModel.Evaluation.ToString(),
                                             obj.RubberLandModel.MeanAnualTemp.ToString(),classofLand, "Rubber"
                                           };
            myRubberArray = myList.ToArray();

            return RedirectToAction("Index", "Report", new { TrcArr = myRubberArray });
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
            string returnedVariableName = "rubber_output";
            object returnedVariable = "";
            Initialize();

            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = Py.CreateScope();
            scope.Set(returnedVariableName, returnedVariable);
            scope.Exec(System.IO.File.ReadAllText(@"C:\Users\harin\OneDrive\Documents\IIT\ResearchProject\w1867882_Harini_Hapuarachchi_Land_Evaluation\MLModel\Land Evaluation of Rubber.py"));

            var myclass = scope.Get("rubber_output");
            var predictedVal = myclass.GetItem(0).ToString();
            double classOfLand = Convert.ToDouble(predictedVal);

            if (classOfLand >= 0.00 && classOfLand < 2.00)
            {
                return "Higly Suitable (S1) - Rubber";
            }
            else if (classOfLand >= 2.00 && classOfLand < 3)
            {
                return "Moderately Suitable (S2) - Rubber";
            }
            else if (classOfLand >= 3 && classOfLand < 4)
            {
                return "Marginally Suitable (S3) - Rubber";
            }
            else
            {
                return "Not Suitable (S4) - Rubber";
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

        public void WriteDataToExcel(RubberLandView obj)
        {
            string[] dataArr = new string[10];

            dataArr[0] = obj.RubberLandModel.Evaluation.ToString();
            dataArr[1] = obj.RubberLandModel.MeanAnualTemp.ToString();
            dataArr[2] = obj.LandModel.Days.ToString();
            dataArr[3] = obj.LandModel.MeanAnualRF.ToString();
            dataArr[4] = obj.LandModel.SoilDepth.ToString();

            if (obj.LandModel.SoilDrainageClass == "EXD")
            {
                dataArr[5] = "1";
                dataArr[6] = "0";
                dataArr[7] = "0";

            }
            else if (obj.LandModel.SoilDrainageClass == "MWD")
            {
                dataArr[5] = "0";
                dataArr[6] = "1";
                dataArr[7] = "0";
            }
            else
            {
                dataArr[5] = "0";
                dataArr[6] = "0";
                dataArr[7] = "1";
            }
           
            dataArr[8] = obj.LandModel.SoilPH.ToString();
            dataArr[9] = obj.LandModel.RockOutcrops.ToString();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage("C:\\Users\\harin\\OneDrive\\Documents\\IIT\\ResearchProject\\w1867882_Harini_Hapuarachchi_Land_Evaluation\\Dataset\\Land Eveluation DataRubber Sample.xlsx"))
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
                package.Save();
            }
        }
    }
}
