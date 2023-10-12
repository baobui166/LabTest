using De02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De02.BUS
{
    public class SanPhamService
    {
        public List<SanPham> getAll()
        {
            SanPhamContext context = new SanPhamContext();
            return context.SanPhams.ToList();
        }

        public SanPham findByID(string id)
        {
            SanPhamContext context = new SanPhamContext();
            return context.SanPhams.FirstOrDefault(p => p.MaLoai == id);
        }

        public void InsertUpdate(SanPham s)
        {
            SanPhamContext context = new SanPhamContext();
            context.SanPhams.AddOrUpdate(s);
            context.SaveChanges();
        }
    }
}
