using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teltonika.Covid.Api.Entities
{
    public class Case
    {
        [Column("object_id")]
        public int Id { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("age_bracket")]
        public AgeBracket AgeBracket { get; set; }

        [Column("municipality")]
        public Municipality Municipality { get; set; }

        [Column("confirmation_date")]
        public DateTime ConfirmationDate { get; set; }

        [Column("case_code")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(64)]
        public string CaseCode { get; set; }

        [Column("Y")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string Y { get; set; }

        [Column("X")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string X { get; set; }
    }
}
