using System.ComponentModel.DataAnnotations;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Models
{
    public class TeaLand
    {
        [Key]
        public int TeaId { get; set; }
        public float EvaluationUp { get; set; }
        public float EvaluationMis { get; set; }
        public float EvaluationLow { get; set; }
        public string SoilTexture { get; set; }
        public float StonesAndGrovels { get; set; }
        public float SlopeAngle { get; set; }
        public string PastErosion { get; set; }
        public int LandId { get; set; }
        public Land Lands { get; set; }

    }
}
