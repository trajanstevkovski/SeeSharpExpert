using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSE.Lottery.RaffleData.Model
{
    [Table("Awards")]
    public class Award : IEntity
    {
        [Key]
        [Column("AwardID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AwardName { get; set; }
        public string AwardDescription { get; set; }
        public int Quantaty { get; set; }
        public byte RuffledType { get; set; } // ENUM Values: Immediate/PerDay/Final
    }
}
