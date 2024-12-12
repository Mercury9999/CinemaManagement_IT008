using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Ultis
{
    public static class MoneyFormat
    {
        public static string FormatToVND(decimal money)
        {
            CultureInfo viCulture = new CultureInfo("vi-VN");
            return money.ToString("C2", viCulture);
        }
    }
}
