namespace Model.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PageFacebook")]
    public partial class PageFacebook
    {
        [Key]
        public int Id { get; set; }

        public string Link { get; set; }
    }
}
