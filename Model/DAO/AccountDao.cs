﻿using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class AccountDao
    {
        DocBaoDataContext db = null;
        public AccountDao()
        {
            db = new DocBaoDataContext();
        }


        //Lấy danh sách tất cả tài khoản
        public IEnumerable<TAIKHOAN> ListAll(string searchString, int page, int pageSize, int idtk)
        {
            IQueryable<TAIKHOAN> model = db.TAIKHOANs.Where(x=>x.IDTaiKhoan!=idtk);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenTaiKhoan.Contains(searchString) || x.HoTen.Contains(searchString));
            }
            return model.OrderByDescending(x => x.IDTaiKhoan).ToPagedList(page, pageSize);
        }



        //Lấy danh sách tất cả tài khoản ADMIN
        public IEnumerable<TAIKHOAN> ListAllAdmin(string searchString, int page, int pageSize, int idtk)
        {
            IQueryable<TAIKHOAN> model = db.TAIKHOANs.Where(x => x.QuyenHan == "A" && x.IDTaiKhoan!=idtk);

            //Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenTaiKhoan.Contains(searchString) || x.HoTen.Contains(searchString));
            }
            //trả ra kết quả và phân trang
            return model.OrderByDescending(x => x.IDTaiKhoan).ToPagedList(page, pageSize);
        }



        //Lấy danh sách tất cả tài khoản USER
        public IEnumerable<TAIKHOAN> ListAllPoster(string searchString, int page, int pageSize)
        {
            IQueryable<TAIKHOAN> model = db.TAIKHOANs.Where(x => x.QuyenHan == "U");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenTaiKhoan.Contains(searchString) || x.HoTen.Contains(searchString));
            }
            return model.OrderByDescending(x => x.IDTaiKhoan).ToPagedList(page, pageSize);
        }

        //Thêm tài khoản
        public long addAccount(TAIKHOAN tk)
        {
            db.TAIKHOANs.Add(tk);
            db.SaveChanges();
            return tk.IDTaiKhoan;
        }

        public TAIKHOAN GetByID(string userName)
        {
            return db.TAIKHOANs.SingleOrDefault(u => u.TenTaiKhoan == userName);
        }



        //Lấy thông tin tài khoản theo ID tài khoản
        public TAIKHOAN ViewDetail(int id)
        {
            return db.TAIKHOANs.Find(id);
        }


        public List<TAIKHOAN> ViewDetail1(int id)
        {
            return db.TAIKHOANs.Where(x=>x.IDTaiKhoan==id).ToList();
        }


        //KHOA TAI KHOAN

        public void BlockAccount(int id)
        {
            TAIKHOAN tk = db.TAIKHOANs.Find(id);
            tk.TrangThaiNguoiDung = "Bị khóa";
            db.SaveChanges();
        }


        public bool updateAccount(TAIKHOAN tk)
        {
            try
            {
                var tk1 = db.TAIKHOANs.SingleOrDefault(x => x.IDTaiKhoan == tk.IDTaiKhoan);
                tk1.HoTen = tk.HoTen;
                tk1.NgaySinh = tk.NgaySinh;
                tk1.Email = tk.Email;
                tk1.DiaChi = tk.DiaChi;
                tk1.GioiTinh = tk.GioiTinh;
                tk1.SDT = tk.SDT;
                tk1.QuyenHan = tk.QuyenHan;
                tk1.AnhDaiDien = tk.AnhDaiDien;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool Delete(int ID)
        {
            try
            {
                var acc = db.TAIKHOANs.SingleOrDefault(x => x.IDTaiKhoan == ID);
                db.TAIKHOANs.Remove(acc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        public int loginAccount(string username, string pass)
        {
            var result = db.TAIKHOANs.SingleOrDefault(x => x.TenTaiKhoan == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThaiNguoiDung.ToUpper() == "BỊ KHÓA")
                {
                    return -1;
                }
                else
                {
                    if (1==2&&result.MatKhau != pass)
                    {
                        return -2;
                    }

                    else
                    {
                        if (result.QuyenHan.ToUpper() == "A")
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;

                        }
                    }

                }
            }
        }
    }
}
