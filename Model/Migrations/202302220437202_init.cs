namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BAIDANG",
                c => new
                    {
                        IDBaiDang = c.Int(nullable: false, identity: true),
                        TenBaiDang = c.String(),
                        TieuDe = c.String(),
                        UrlRequire = c.String(maxLength: 200),
                        AnhDaiDien = c.String(maxLength: 500),
                        NoiDung = c.String(),
                        IDTaiKhoan = c.Int(nullable: false),
                        NgayDang = c.DateTime(storeType: "date"),
                        TrangThaiBaiDang = c.String(maxLength: 50),
                        IDTheLoai = c.Int(nullable: false),
                        IdThe = c.Int(),
                    })
                .PrimaryKey(t => t.IDBaiDang)
                .ForeignKey("dbo.TAIKHOAN", t => t.IDTaiKhoan)
                .ForeignKey("dbo.The", t => t.IdThe)
                .ForeignKey("dbo.THELOAI", t => t.IDTheLoai)
                .Index(t => t.IDTaiKhoan)
                .Index(t => t.IdThe)
                .Index(t => t.IDTheLoai);
            
            CreateTable(
                "dbo.LUOTXEM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NgayThang = c.DateTime(nullable: false, storeType: "date"),
                        IDBaiDang = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.NgayThang })
                .ForeignKey("dbo.BAIDANG", t => t.IDBaiDang)
                .Index(t => t.IDBaiDang);
            
            CreateTable(
                "dbo.TAIKHOAN",
                c => new
                    {
                        IDTaiKhoan = c.Int(nullable: false, identity: true),
                        TenTaiKhoan = c.String(nullable: false, maxLength: 30),
                        MatKhau = c.String(nullable: false),
                        HoTen = c.String(nullable: false, maxLength: 50),
                        NgaySinh = c.DateTime(nullable: false, storeType: "date"),
                        GioiTinh = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        DiaChi = c.String(nullable: false, maxLength: 200),
                        SDT = c.String(nullable: false, maxLength: 20),
                        AnhDaiDien = c.String(nullable: false, maxLength: 100),
                        TrangThaiNguoiDung = c.String(maxLength: 50),
                        QuyenHan = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.IDTaiKhoan);
            
            CreateTable(
                "dbo.The",
                c => new
                    {
                        IdThe = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdThe);
            
            CreateTable(
                "dbo.THELOAI",
                c => new
                    {
                        IDTheLoai = c.Int(nullable: false, identity: true),
                        TenTheLoai = c.String(maxLength: 50),
                        UrlRequire = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IDTheLoai);
            
            CreateTable(
                "dbo.CHUYENMUC",
                c => new
                    {
                        IDCM = c.Int(nullable: false, identity: true),
                        IDCMCha = c.Int(),
                        TenCM = c.String(maxLength: 50),
                        Link = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.IDCM);
            
            CreateTable(
                "dbo.PageFacebook",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TuCam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tu = c.String(),
                        ThayThe = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BAIDANG", "IDTheLoai", "dbo.THELOAI");
            DropForeignKey("dbo.BAIDANG", "IdThe", "dbo.The");
            DropForeignKey("dbo.BAIDANG", "IDTaiKhoan", "dbo.TAIKHOAN");
            DropForeignKey("dbo.LUOTXEM", "IDBaiDang", "dbo.BAIDANG");
            DropIndex("dbo.BAIDANG", new[] { "IDTheLoai" });
            DropIndex("dbo.BAIDANG", new[] { "IdThe" });
            DropIndex("dbo.BAIDANG", new[] { "IDTaiKhoan" });
            DropIndex("dbo.LUOTXEM", new[] { "IDBaiDang" });
            DropTable("dbo.TuCam");
            DropTable("dbo.PageFacebook");
            DropTable("dbo.CHUYENMUC");
            DropTable("dbo.THELOAI");
            DropTable("dbo.The");
            DropTable("dbo.TAIKHOAN");
            DropTable("dbo.LUOTXEM");
            DropTable("dbo.BAIDANG");
        }
    }
}
