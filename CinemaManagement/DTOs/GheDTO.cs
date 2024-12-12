using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public class GheDTO
    {
        public GheDTO() { }
        public int MaGhe { get; set; }
        public int SoGhe { get; set; }
        public int SoPhong { get; set; }
        public string SoGheStr
        {
            get
            {
                if (SoGhe < 1) return string.Empty; 
                int charIndex = (SoGhe - 1) / 10;
                int numIndex = (SoGhe - 1) % 10 + 1;
                char letter = (char)('A' + charIndex);
                return $"{letter}{numIndex:000}";
            }
        }
    }
}
