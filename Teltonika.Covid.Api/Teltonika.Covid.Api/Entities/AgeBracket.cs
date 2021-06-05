using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teltonika.Covid.Api.Entities
{
    public class AgeBracket
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
