using De02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De02.BUS
{
    public class LoaiSPService
    {
        public List<LoaiSP> getAll()
        {
            SanPhamContext context = new SanPhamContext();
            return context.LoaiSPs.ToList();
        }
    }
}
