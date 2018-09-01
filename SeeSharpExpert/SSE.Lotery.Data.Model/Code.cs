﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSE.Lotery.Data.Model
{
    [Table("dbo.Codes")]
    public class Code : IEntity
    {
        [Key]
        [Column("CodeID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CodeValue { get; set; }
        public bool IsWinning { get; set; }
        [DefaultValue(false)]
        public bool IsUsed { get; set; }
    }
}
