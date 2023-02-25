namespace Model.EntityFramework
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("THELOAI")]
    public partial class THELOAI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THELOAI()
        {
            BAIDANGs = new HashSet<BAIDANG>();
        }

        [Key]
        public int IDTheLoai { get; set; }

        [StringLength(50)]
        public string TenTheLoai { get; set; }
        [StringLength(50)]
        public string UrlRequire { get; set; }

        public string Domain { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAIDANG> BAIDANGs { get; set; }
    }
}
