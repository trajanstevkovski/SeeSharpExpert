﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSE.Lotery.Data.Model
{
    [Table("dbo.UserCodeAwards")]
    public class UserCodeAward : IEntity
    {
        [Key]
        [Column("UserCodeAwardID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("UserCodeID")]
        public int UserCodeId { get; set; }
        public UserCode UserCode { get; set; }
        [Column("AwardID")]
        public int AwardId { get; set; }
        public Award Award { get; set; }
        public DateTime WonAt { get; set; }

    }
}
