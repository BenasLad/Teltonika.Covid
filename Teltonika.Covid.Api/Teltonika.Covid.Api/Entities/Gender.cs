using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teltonika.Covid.Api.Entities
{
    public class Gender : CovidEntity
    {
        [Column("id")]
        public new int Id { get; set; }

        [Column("name")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
