using System.ComponentModel.DataAnnotations;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Models
{
    public class RubberLand
    {
        [Key]
        public int RubberId { get; set; }
        public float Evaluation { get; set; }
        public float MeanAnualTemp { get; set; }
        public int LandId { get; set; }
        public Land Lands { get; set; }
    }
}
