using System.ComponentModel.DataAnnotations;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Models
{
    public class CoconutLand
    {
        [Key]
        public int CoconutId { get; set; }
        public float Evaluation { get; set; }
        public float MeanAnualTemp { get; set; }
        public float TotalSunshine { get; set; }
        public float MinimumHumidity { get; set; }
        public string SoilTexture { get; set; }
        public float WaterDepth { get; set; }
        public float Ec { get; set; }
        public float SlopeAngle { get; set; }
        public int LandId { get; set; }
        public Land Lands { get; set; }

    }
}
