using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Models
{

    public class Land
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LandId { get; set; }
        public string Location { get; set; }
        public int Days { get; set; }
        public float MeanAnualRF { get; set; }
        public float SoilDepth { get; set; }
        //public string SoilTexture { get; set; }
        //public float StonesAndGrovels { get; set; }
        public string SoilDrainageClass { get; set; }
        public float SoilPH { get; set; }
        //public float SlopeAngle { get; set; }
        //public string PastErosion { get; set; }
        public float RockOutcrops { get; set; }
        //public int ClassOfLandUnit { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Evaluation> Evaluations { get; }
        public ICollection<TeaLand> TeaLands { get; }
        public ICollection<CoconutLand> CoconutLands { get; }
        public ICollection<RubberLand> RubberLands { get; }

}
}
