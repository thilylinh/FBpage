namespace Model.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LUOTXEM")]
    public partial class LUOTXEM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime NgayThang { get; set; }

        public int IDBaiDang { get; set; }

        public virtual BAIDANG BAIDANG { get; set; }
    }
}
