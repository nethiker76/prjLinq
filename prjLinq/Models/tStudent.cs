using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjLinq.Models
{
    public partial class tStudent
    {
        [Required(ErrorMessage = "學號不可以空白!")]
        public string fStuId { get; set; }

        [Required(ErrorMessage = "姓名不可以空白!")]
        public string fName { get; set; }

        [Required(ErrorMessage = "信箱不可以空白!")]
        [EmailAddress(ErrorMessage = "E-Mail格式錯誤!")]
        public string fEmail { get; set; }

        [Required(ErrorMessage = "分數不可以空白!")]
        [Range(0,100,ErrorMessage = "分數必須是0-100")]
        public Nullable<int> fScore { get; set; }
    }
}