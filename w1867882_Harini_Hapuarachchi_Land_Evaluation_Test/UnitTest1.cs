using System;
using w1867882_Harini_Hapuarachchi_Land_Evaluation;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Views.VidewModel;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_TeaPrediction()
        {
            EvaluateController evaluateController = new EvaluateController();
            Land land = new Land();
            TeaLand teaLand = new TeaLand();
            TeaLandView teaLandView = new TeaLandView();
            land.Days = 350;
            land.MeanAnualRF = 2550;
            land.SoilDepth = 72;
            land.SoilPH = 5;
            land.SoilDrainageClass = "WD";
            land.Location = "Up";
            land.RockOutcrops = 0;
            teaLand.EvaluationLow = 0;
            teaLand.EvaluationMis = 0;
            teaLand.EvaluationUp = 1790;
            teaLand.PastErosion = "slight";
            teaLand.SoilTexture = "L";
            teaLand.StonesAndGrovels = 5;
            teaLand.SlopeAngle = 23;

            teaLandView.LandModel = land;
            teaLandView.TeaLandModel = teaLand;

            evaluateController.WriteDataToExcel( teaLandView );
            string classofLand = evaluateController.RunPythonTeaCodeAndReturn();
            Assert.AreEqual("Moderately Suitable (S2) - Tea", classofLand);

        }
    }
}