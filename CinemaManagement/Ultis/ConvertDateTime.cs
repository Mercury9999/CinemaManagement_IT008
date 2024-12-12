using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Ultis
{
    public static class ConvertDateTime
    {
        public static string Clock(DateTime? dateTime)
        {
            if (dateTime.HasValue) return dateTime.Value.ToString("HH:mm");
            else return "Không có";
        }
        public static string Day(DateTime? dateTime)
        {
            if (dateTime.HasValue) return dateTime.Value.ToString("dd/MM/yyyy");
            else return "Không có";
        }
        public static string Full(DateTime? dateTime)
        {
            if (dateTime.HasValue) return dateTime.Value.ToString("HH:mm:ss dd/MM/yyyy");
            else return "Không có";
        }
    }
}
