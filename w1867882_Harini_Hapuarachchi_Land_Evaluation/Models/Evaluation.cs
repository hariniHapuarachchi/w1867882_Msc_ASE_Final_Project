using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Models
{
    public class Evaluation
    {
        [Key]
        public int EvaluationId { get; set; }
        public string Prediction { get; set; }
        public int LandId { get; set; }
        public Land Land { get; set; }
    }
}
