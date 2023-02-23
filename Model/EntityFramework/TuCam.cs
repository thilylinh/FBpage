using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EntityFramework
{
    [Table("TuCam")]
    public partial class TuCam
    {
        [Key]
        public int Id { get; set; }

        public string Tu { get; set; }
        public string ThayThe { get; set; }
    }
}
