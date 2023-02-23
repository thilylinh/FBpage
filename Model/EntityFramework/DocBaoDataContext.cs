namespace Model.EntityFramework
{
    using System.Data.Entity;

    public partial class DocBaoDataContext : DbContext
    {
        public DocBaoDataContext()
            : base("name=DocBaoDataContext")
        {
        }

        public virtual DbSet<BAIDANG> BAIDANGs { get; set; }
        public virtual DbSet<CHUYENMUC> CHUYENMUCs { get; set; }
        public virtual DbSet<LUOTXEM> LUOTXEMs { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public virtual DbSet<THELOAI> THELOAIs { get; set; }
        public virtual DbSet<PageFacebook> PageFacebooks { get; set; }
        public virtual DbSet<TuCam> TuCams { get; set; }

        public virtual DbSet<The> Thes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BAIDANG>()
                .HasMany(e => e.LUOTXEMs)
                .WithRequired(e => e.BAIDANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.QuyenHan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.BAIDANGs)
                .WithRequired(e => e.TAIKHOAN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<THELOAI>()
                .HasMany(e => e.BAIDANGs)
                .WithRequired(e => e.THELOAI)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<PageFacebook>();
        }
    }
}
