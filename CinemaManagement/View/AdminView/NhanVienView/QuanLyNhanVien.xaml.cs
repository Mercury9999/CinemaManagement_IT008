using CinemaManagement.ViewModel.AdminVM;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaManagement.View.AdminView
{
    /// <summary>
    /// Interaction logic for QuanLyNhanVien.xaml
    /// </summary>
    public partial class QuanLyNhanVien : Page
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }
        // Hàm để xuất dữ liệu ra file Excel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ DataGrid (dsNV)
            var staffList = (this.DataContext as QuanLyNhanVienVM)?.dsNV;

            if (staffList == null || !staffList.Any())
            {
                MessageBox.Show("Không có dữ liệu để xuất!");
                return;
            }

            // Tạo workbook mới
            using (var workbook = new XLWorkbook())
            {
                // Tạo worksheet
                var worksheet = workbook.AddWorksheet("NhanVien");

                // Thêm tiêu đề cột
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Họ và tên";
                worksheet.Cell(1, 3).Value = "Số điện thoại";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Chức vụ";
                worksheet.Cell(1, 6).Value = "Ngày sinh";
                worksheet.Cell(1, 7).Value = "Ngày vào làm";

                // Thêm dữ liệu nhân viên
                int row = 2;
                foreach (var staff in staffList)
                {
                    worksheet.Cell(row, 1).Value = staff.MaNVStr;
                    worksheet.Cell(row, 2).Value = staff.TenNV;
                    worksheet.Cell(row, 3).Value = staff.SDT_NV;
                    worksheet.Cell(row, 4).Value = staff.email_NV;
                    worksheet.Cell(row, 5).Value = staff.ChucVu;
                    worksheet.Cell(row, 6).Value = staff.NgaySinhStr;
                    worksheet.Cell(row, 7).Value = staff.NgayVaoLamStr;
                    row++;
                }

                // Lưu file Excel
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = "NhanVien.xlsx"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    CustomControls.MyMessageBox.Show("Dữ liệu đã được xuất thành công!");
                }
            }
        }

       
    }
}
