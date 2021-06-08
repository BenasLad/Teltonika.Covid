using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teltonika.Covid.Api.Entities
{
    public class Municipality : CovidEntity
    {
        [Column("id")]
        public new int Id { get; set; }

        [Column("name")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Column("code")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(2)]
        public string Code { get; set; }
    }
}
