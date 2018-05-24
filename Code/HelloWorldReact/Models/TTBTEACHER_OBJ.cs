using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS.uni
{
    public class TTBTEACHER_OBJ
    {
        [Display(Name = "Tên giảng viên")]
        public string tengiangvien { set; get; }
        [Display(Name = "Tên môn học")]
        public string monhoc { set; get; }
        [Display(Name = "Lớp môn học")]
        public string lopmonhoc { set; get; }
        [Display(Name = "Thứ")]
        public string thu { set; get; }
        [Display(Name = "Tiết")]
        public string tiet { set; get; }
        [Display(Name = "Giảng đường")]
        public string giangduong { set; get; }
        public string ID { set; get; }
    }
}

