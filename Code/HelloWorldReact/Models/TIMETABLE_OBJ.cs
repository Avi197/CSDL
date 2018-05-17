using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS.uni
{
    public class TIMETABLE_OBJ
    {
        [Display(Name = "Tên môn học")]
        public string Tenmonhoc { set; get; }
        [Display(Name = "STC")]
        public int STC { set; get; }
        [Display(Name = "Lớp môn học")]
        public string Lopmonhoc { set; get; }
        [Display(Name = "Tên giảng viên")]
        public string TenGV { set; get; }
        [Display(Name = "Thứ")]
        public string Thu { set; get; }
        [Display(Name = "Tiết")]
        public string Tiet { set; get; }
        [Display(Name = "Giảng đường")]
        public string room { set; get; }
        [Display(Name = "Số sinh viên")]
        public int Sosv { set; get; }
        [Display(Name = "Số SVĐK")]
        public int Sosvdk { set; get; }
        public string ID { set; get; }
    }
}

