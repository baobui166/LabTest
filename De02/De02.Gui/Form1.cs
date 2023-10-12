using De02.BUS;
using De02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De02.Gui
{
    public partial class Form1 : Form
    {

        private readonly SanPhamService SanPhamService = new SanPhamService();
        private readonly LoaiSPService LoaiSPService = new LoaiSPService();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SanPhamContext context = new SanPhamContext();
            SanPham db = context.SanPhams.FirstOrDefault(p => p.MaSP == txtMaSP.Text);
            SanPham s = new SanPham()
            {
                MaSP = txtMaSP.Text,
                TenSp = txtTenSp.Text,
                NgayNhap = dateTimePicker1.Value,
                MaLoai = cmbSP.SelectedValue.ToString(),
            };


            context.SanPhams.Add(s);
            context.SaveChanges();

            Form1_Load(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgvSanPham);
                var listFacultys = LoaiSPService.getAll();
                var listStudents = SanPhamService.getAll();
                FillFaculty(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtFind.Text = "";
            txtMaSP.Text = "";
            txtTenSp.Text = "";
        }

        private void FillFaculty(List<LoaiSP> listFaculty)
        {
            listFaculty.Insert(0, new LoaiSP());
            this.cmbSP.DataSource = listFaculty;
            this.cmbSP.DisplayMember = "TenLoai";
            this.cmbSP.ValueMember = "MaLoai";
        }

        private void BindGrid(List<SanPham> listStudent)
        {
            dgvSanPham.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvSanPham.Rows.Add();
                dgvSanPham.Rows[index].Cells[0].Value = item.MaSP;
                dgvSanPham.Rows[index].Cells[1].Value = item.TenSp;
                dgvSanPham.Rows[index].Cells[2].Value = item.NgayNhap;
                dgvSanPham.Rows[index].Cells[3].Value = item.LoaiSP.TenLoai + " ";

            }
        }

        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionForeColor = Color.DarkGoldenrod;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SanPhamContext context = new SanPhamContext();
            SanPham dbDelete = context.SanPhams.FirstOrDefault(p => p.MaSP == txtMaSP.Text);

            if (dbDelete != null)
            {
                context.SanPhams.Remove(dbDelete);
                context.SaveChanges();
            }

            Form1_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SanPhamContext context = new SanPhamContext();
            SanPham dbUpdate = context.SanPhams.FirstOrDefault(p => p.MaLoai == txtMaSP.Text);

            if (dbUpdate != null)
            {
                dbUpdate.TenSp = txtTenSp.Text;
                dbUpdate.MaSP = txtMaSP.Text;
                dbUpdate.NgayNhap = dateTimePicker1.Value;
                dbUpdate.MaLoai = cmbSP.SelectedValue.ToString();
                context.SaveChanges();
            }

           Form1_Load(sender, e);   

        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
            txtMaSP.Text = row.Cells[0].Value.ToString();
            txtTenSp.Text = row.Cells[1].Value.ToString();
            dateTimePicker1.Text = row.Cells[2].Value.ToString();
            cmbSP.Text = row.Cells[3].Value.ToString();
        }

        private string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        

        private void btnFind_Click(object sender, EventArgs e)
        {
            //List<Sinhvien> sinhviens = new List<Sinhvien>();
            string findName = txtFind.Text;
            findName = RemoveDiacritics(findName);
            for (int i = 0; i < dgvSanPham.Rows.Count; i++)
            {
                if (dgvSanPham.Rows[i].Cells[1].Value != null)
                {
                    string name = dgvSanPham.Rows[i].Cells[1].Value.ToString();


                    name = RemoveDiacritics(name);


                    bool contains = name.IndexOf(findName, StringComparison.OrdinalIgnoreCase) >= 0;
                    if (contains)
                    {
                        dgvSanPham.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgvSanPham.Rows[i].Visible = false;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

