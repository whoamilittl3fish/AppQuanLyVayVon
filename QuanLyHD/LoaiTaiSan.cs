using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVayVon.Models
{
    public class LoaiTaiSanItem
    {
        public int ID { get; set; }
        public string Ten { get; set; } = string.Empty; // Initialize with a default value

        public override string ToString()
        {
            return Ten;
        }
    }
}

