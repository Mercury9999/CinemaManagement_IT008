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
using CinemaManagement.CustomControls;
using ClosedXML.Excel;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for QuanLyKhachHang.xaml
    /// </summary>
    public partial class QuanLyKhachHang : Page
    {
        public QuanLyKhachHang()
        {
            InitializeComponent();
        }

        // Hàm để xuất dữ liệu ra file Excel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ DataGrid (dsKH)
            var customers = (this.DataContext as ViewModel.AdminVM.QuanLyKhachHangVM)?.dsKH;

            if (customers == null || !customers.Any())
            {
                MessageBox.Show("Không có dữ liệu để xuất!");
                return;
            }

            // Tạo workbook mới
            using (var workbook = new XLWorkbook())
            {
                // Tạo worksheet
                var worksheet = workbook.AddWorksheet("KhachHang");

                // Thêm tiêu đề cột
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Tên khách hàng";
                worksheet.Cell(1, 3).Value = "Số điện thoại";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Ngày đăng ký";
                worksheet.Cell(1, 6).Value = "Chi tiêu";

                // Thêm dữ liệu khách hàng
                int row = 2;
                foreach (var customer in customers)
                {
                    worksheet.Cell(row, 1).Value = customer.MaKHStr;
                    worksheet.Cell(row, 2).Value = customer.TenKH;
                    worksheet.Cell(row, 3).Value = customer.SDT_KH;
                    worksheet.Cell(row, 4).Value = customer.email_KH;
                    worksheet.Cell(row, 5).Value = customer.NgayDK;
                    worksheet.Cell(row, 6).Value = customer.HDTichLuyStr;
                    row++;
                }

                // Lưu file Excel
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = "KhachHang.xlsx"
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
