using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    using System;
    using System.Collections.Generic;
    
    public class PhongChieuDTO
    {
        public PhongChieuDTO()
        {
            this.Ghe = new List<GheDTO>();
        }
    
        public int SoPhong { get; set; }
        public int SLGhe { get; set; }
        public List<GheDTO> Ghe { get; set; }
        public string SoPhongStr
        {
            get
            {
                return $"P{SoPhong}";
            }
        }
       
    }
}
