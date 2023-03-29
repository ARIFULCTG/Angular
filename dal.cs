using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NG6_R51
{
    public class trainer
    {
        [Key]
        public string trainerid { get; set; }
        public string trainername { get; set; }
        public string location { get; set; }
        public IList<player> player { get; set; }
    }
    public partial class player
    {
        [Key]
        public string playercode { get; set; }
        public string playername { get; set; }
        [ForeignKey("trainer")]
        public string trainerid { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Nullable<decimal> traincost { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Nullable<decimal> earned { get; set; }
        public DateTime matchdate { get; set; }
        public string picture { get; set; }
        public trainer trainer { get; set; }
    }
    public class TrainerPlayerVm
    {
        public TrainerPlayerVm()
        {
            this.trainer = new trainer();
            this.player = new List<player>();
            /* do nothing */
        }
        public trainer trainer { get; set; }
        public List<player> player { get; set; }
    }

}
